using WarshipsRPGAlpha.Dtos.Ship;
using WarshipsRPGAlpha.Dtos.SpecialWaepon;
using WarshipsRPGAlpha.Models;

namespace WarshipsRPGAlpha.Services.ShipService
{
    public interface IShipService
    {
        Task<ServiceResponse<List<GetShipResponseDto>>> GetAllShips();
        Task<ServiceResponse<GetShipResponseDto>> GetShipById(int id);
        Task<ServiceResponse<List<GetShipResponseDto>>> AddShip(AddShipRequestDto newShip);
        Task<ServiceResponse<GetShipResponseDto>> UpdateShip(UpdateShipRequestDto updatedShip);
        Task<ServiceResponse<List<GetShipResponseDto>>> DeleteShip(int id);
        Task<ServiceResponse<GetShipResponseDto>> AddSpecialWaepon(AddSpecialWaeponDto newWaepon);
    }
}
