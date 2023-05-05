using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using WarshipsRPGAlpha.Dtos.Ship;
using WarshipsRPGAlpha.Dtos.SpecialWaepon;
using WarshipsRPGAlpha.Models;
using WarshipsRPGBeta.Data;

namespace WarshipsRPGAlpha.Services.ShipService
{
    public class ShipService : IShipService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShipService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
          
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);


        public async Task<ServiceResponse<List<GetShipResponseDto>>> AddShip(AddShipRequestDto newShip)
        {
            var serviceResponse = new ServiceResponse<List<GetShipResponseDto>>();

            var ship = _mapper.Map<Ship>(newShip);

            ship.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            _context.Ships.Add(ship);

            await _context.SaveChangesAsync();

            serviceResponse.Data = 
                await _context.Ships
                .Where(u => u.Id == GetUserId())
                .Select(s => _mapper.Map<GetShipResponseDto>(s)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetShipResponseDto>>> GetAllShips()
        {
            var serviceResponse = new ServiceResponse<List<GetShipResponseDto>>();
            var dbShips = await _context.Ships.Where(c => c.User!.Id == GetUserId())
                .Include(c => c.MainGun)
                .ToListAsync();
            serviceResponse.Data = dbShips.Select(s => _mapper.Map<GetShipResponseDto>(s)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetShipResponseDto>> GetShipById(int id)
        {
            var serviceResponse = new ServiceResponse<GetShipResponseDto>();
            var dbShip = await _context.Ships
                .Include(c => c.MainGun)
                .FirstOrDefaultAsync(s => s.Id == id && s.User!.Id == GetUserId());

            serviceResponse.Data = _mapper.Map<GetShipResponseDto>(dbShip);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetShipResponseDto>> UpdateShip(UpdateShipRequestDto updatedShip)
        {
            var serviceResponse = new ServiceResponse<GetShipResponseDto>();

            try
            {
                var ship = await _context.Ships
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(s => s.Id == updatedShip.Id);

                if (ship == null || ship.User!.Id != GetUserId()) 
                    throw new Exception($"Ship with given Id {updatedShip.Id} was not found.");

                _mapper.Map(updatedShip, ship);

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetShipResponseDto>(ship);
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetShipResponseDto>>> DeleteShip(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetShipResponseDto>>();

            try
            {
                var ship = await _context.Ships.FirstOrDefaultAsync(s => s.Id == id && s.User!.Id == GetUserId());

                if (ship == null) 
                    throw new Exception($"Ship with given Id {id} was not found.");

                _context.Ships.Remove(ship);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Ships
                    .Where(s => s.User!.Id == GetUserId())
                    .Select(s => _mapper.Map<GetShipResponseDto>(s)).ToListAsync();
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetShipResponseDto>> AddSpecialWaepon(AddSpecialWaeponDto newWaepon)
        {
            var response = new ServiceResponse<GetShipResponseDto>();
            try
            {
                var ship = await _context.Ships
                    .Include(s => s.MainGun)
                    .Include(s => s.SpecialWaepons)
                    .FirstOrDefaultAsync(s => s.Id == newWaepon.ShipId && s.User!.Id == GetUserId());

                if (ship == null)
                {
                    response.Success = false;
                    response.Message = "Ship not found.";
                    return response;
                }

                var specialWaepon = await _context.SpecialWaepons.FirstOrDefaultAsync(s => s.Id == newWaepon.SpecialWaeponId);

                if (specialWaepon == null)
                {
                    response.Success = false;
                    response.Message = "Skill not found.";
                    return response;
                }

                ship.SpecialWaepons!.Add(specialWaepon);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetShipResponseDto>(ship);

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
        }
    }
}
