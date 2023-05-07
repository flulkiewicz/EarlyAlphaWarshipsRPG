using System.Security.Claims;
using WarshipsRPGAlpha.Dtos.MainGun;
using WarshipsRPGAlpha.Dtos.Ship;
using WarshipsRPGBeta.Data;

namespace WarshipsRPGAlpha.Services.MainGunService
{
    public class MainGunService : IMainGunService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public MainGunService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<GetShipResponseDto>> AddMainGun(AddMainGunDto newGun)
        {
            var response = new ServiceResponse<GetShipResponseDto>();
            try
            {
                var ship = await _context.Ships
                    .FirstOrDefaultAsync(s => s.Id == newGun.ShipId && 
                        s.User!.Id == int.Parse(_httpContextAccessor.HttpContext!.User
                            .FindFirstValue(ClaimTypes.NameIdentifier)!)); 

                if(ship is null)
                {
                    response.Success = false;
                    response.Message = "Ship not found.";
                    return response;
                }

                var mainGun = new MainGun()
                {
                    Name = newGun.Name,
                    Damage = newGun.Damage,
                    Ship = ship
                };

                _context.MainGuns.Add(mainGun);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetShipResponseDto>(ship);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message= ex.Message;
            }

            return response;
        }
    }
}
