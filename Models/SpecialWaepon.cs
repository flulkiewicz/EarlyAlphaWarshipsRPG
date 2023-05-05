namespace WarshipsRPGAlpha.Models
{
    public class SpecialWaepon
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; }
        public List<Ship>? Ships { get; set; }
    }
}
