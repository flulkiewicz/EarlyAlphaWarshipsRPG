using WarshipsRPGAlpha.Models;

namespace WarshipsRPGAlpha.Models
{
    public class Ship
    {
        public int Id { get; set; }
        public string Name { get; set; } = "LS3";
        public int HitPoints { get; set; } = 180;
        public int FirePower { get; set; } = 10;
        public int Armor { get; set; } = 20;
        public int Crew { get; set; } = 3;
        public ShipClass Class { get; set; } = ShipClass.Boat;
        public ShipFaction Faction { get; set; } = ShipFaction.Germany;
        public User? User { get; set; }
        public MainGun? MainGun { get; set; }
        public List<SpecialWaepon>? SpecialWaepons { get; set;}
    }
}
