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

        public List<Ship> AddShip(Ship newShip)
        {
            ships.Add(newShip);

            return ships;
        }

        public List<Ship> GetAllShips()
        {
            return ships;
        }

        public Ship GetShipById(int id)
        {
            var ship = ships.FirstOrDefault(b => b.Id == id);

            if(ship != null) return ship;

            throw new Exception("Character not found");
        }
    }
}
