using TestApiV2.Dtos.Ship;
using TestApiV2.Models;

namespace TestApiV2.Services.ShipService
{
    public interface IShipService
    {
        Task<ServiceResponse<List<GetShipResponseDto>>> GetAllShips();
        Task<ServiceResponse<GetShipResponseDto>> GetShipById(int id);
        Task<ServiceResponse<List<GetShipResponseDto>>> AddShip(AddShipRequestDto newShip);
        Task<ServiceResponse<GetShipResponseDto>> UpdateShip(UpdateShipRequestDto updatedShip);
        Task<ServiceResponse<List<GetShipResponseDto>>> DeleteShip(int id);
    }
}
