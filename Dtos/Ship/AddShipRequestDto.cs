namespace TestApiV2.Dtos.Ship
{
    public class AddShipRequestDto
    {
        public string Name { get; set; } = "LS3";
        public int HitPoints { get; set; } = 180;
        public int FirePower { get; set; } = 10;
        public int Armor { get; set; } = 20;
        public int Crew { get; set; } = 3;
        public ShipClass Class { get; set; } = ShipClass.Boat;
        public ShipFaction Faction { get; set; } = ShipFaction.Germany;
    }
}
