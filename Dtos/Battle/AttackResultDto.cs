namespace WarshipsRPGAlpha.Dtos.Battle
{
    public class AttackResultDto
    {
        public string Attacker { get; set; } = string.Empty;
        public string Opponent { get; set; } = string.Empty;
        public int AttackersHp { get; set; }
        public int OpponentsHp { get; set; }
        public int Damage { get; set; }
    }
}
