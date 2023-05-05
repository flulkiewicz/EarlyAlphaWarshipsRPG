namespace WarshipsRPGAlpha.Dtos.MainGun
{
    public class AddMainGunDto
    {
        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; }
        public int ShipId { get; set; }
    }
}
