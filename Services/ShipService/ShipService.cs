using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.CompilerServices;
using WarshipsRPGAlpha.Dtos.Ship;
using WarshipsRPGAlpha.Models;
using WarshipsRPGBeta.Data;

namespace WarshipsRPGAlpha.Services.ShipService
{
    public class ShipService : IShipService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ShipService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<ServiceResponse<List<GetShipResponseDto>>> AddShip(AddShipRequestDto newShip)
        {
            var serviceResponse = new ServiceResponse<List<GetShipResponseDto>>();

            var ship = _mapper.Map<Ship>(newShip);
            _context.Ships.Add(ship);

            await _context.SaveChangesAsync();

            serviceResponse.Data = 
                await _context.Ships.Select(s => _mapper.Map<GetShipResponseDto>(s)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetShipResponseDto>>> GetAllShips()
        {
            var serviceResponse = new ServiceResponse<List<GetShipResponseDto>>();
            var dbShips = await _context.Ships.ToListAsync();
            serviceResponse.Data = dbShips.Select(s => _mapper.Map<GetShipResponseDto>(s)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetShipResponseDto>> GetShipById(int id)
        {
            var serviceResponse = new ServiceResponse<GetShipResponseDto>();
            var dbShip = await _context.Ships.FirstOrDefaultAsync(s => s.Id == id);
            serviceResponse.Data = _mapper.Map<GetShipResponseDto>(dbShip);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetShipResponseDto>> UpdateShip(UpdateShipRequestDto updatedShip)
        {
            var serviceResponse = new ServiceResponse<GetShipResponseDto>();

            try
            {
                var ship = await _context.Ships.FirstOrDefaultAsync(s => s.Id == updatedShip.Id);

                if (ship == null) 
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
                var ship = await _context.Ships.FirstOrDefaultAsync(s => s.Id == id);

                if (ship == null) 
                    throw new Exception($"Ship with given Id {id} was not found.");

                _context.Ships.Remove(ship);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Ships.Select(s => _mapper.Map<GetShipResponseDto>(s)).ToListAsync();
            }
            catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
