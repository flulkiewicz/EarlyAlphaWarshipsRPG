using TestApiV2.Models;

namespace TestApiV2.Services.ShipService
{
    public class ShipService : IShipService
    {
        private static List<Ship> ships = new List<Ship>()
        {
            new Ship(),
            new Ship() {Id = 1, Name = "SKR-7", Class = ShipClass.Frigate, Faction = ShipFaction.Russia},
            new Ship() {Id = 2, Name = "Arleigh", Class =ShipClass.Destroyer, Faction = ShipFaction.America},
        };

        public async Task<ServiceResponse<List<Ship>>> AddShip(Ship newShip)
        {
            var serviceResponse = new ServiceResponse<List<Ship>>();
            serviceResponse.Data = ships;

            ships.Add(newShip);

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Ship>>> GetAllShips()
        {
            var serviceResponse = new ServiceResponse<List<Ship>>();
            serviceResponse.Data = ships;

            return serviceResponse;
        }

        public async Task<ServiceResponse<Ship>> GetShipById(int id)
        {
            var serviceResponse = new ServiceResponse<Ship>();

            var ship = ships.FirstOrDefault(b => b.Id == id);
            serviceResponse.Data = ship;

            return serviceResponse;
        }
    }
}
