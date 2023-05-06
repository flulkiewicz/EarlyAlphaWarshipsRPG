
namespace WarshipsRPGAlpha.Services.BattleService
{
    public interface IBattleService
    {
        Task<ServiceResponse<AttackResultDto>> MainGunAttack (MainGunAttackDto request);
        Task<ServiceResponse<AttackResultDto>> SpecialWaeponAttack (SpecialWaeponAttackDto request);
        Task<ServiceResponse<BattleLogDto>> Battle(BattleRequestDto request);

    }
}
