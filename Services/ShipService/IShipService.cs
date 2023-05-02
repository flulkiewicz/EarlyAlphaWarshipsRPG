using TestApiV2.Models;

namespace TestApiV2.Services.ShipService
{
    public interface IShipService
    {
        Task<ServiceResponse<List<Ship>>> GetAllShips();
        Task<ServiceResponse<Ship>> GetShipById(int id);
        Task<ServiceResponse<List<Ship>>> AddShip(Ship newShip);
    }
}
