using TestApiV2.Models;

namespace TestApiV2.Services.ShipService
{
    public interface IShipService
    {
        List<Ship> GetAllShips();
        Ship GetShipById(int id);
        List<Ship> AddShip(Ship newShip);
    }
}
