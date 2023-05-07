namespace WarshipsRPGAlpha.Dtos.Battle
{
    public class TeamBattleRequestDto
    {
        public List<int> Team1ShipIds { get; set; } = new List<int>();
        public List<int> Team2ShipIds { get; set; } = new List<int>();
    }
}
