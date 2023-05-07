namespace WarshipsRPGAlpha.Models
{
    public class MainGun
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Damage { get; set; }
        public Ship? Ship { get; set; }
        public int ShipId { get; set; }
    }
}
