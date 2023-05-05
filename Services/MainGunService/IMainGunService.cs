using WarshipsRPGAlpha.Dtos.MainGun;

namespace WarshipsRPGAlpha.Services.MainGunService
{
    public interface IMainGunService
    {
        Task<ServiceResponse<GetShipResponseDto>> AddMainGun(AddMainGunDto addMainGunDto);
    }
}
