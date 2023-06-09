﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using WarshipsRPGAlpha.Services.BattleService;

namespace WarshipsRPGAlpha.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BattleController : ControllerBase
    {
        private readonly IBattleService _battleService;

        public BattleController(IBattleService battleService)
        {
            _battleService = battleService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("MainGun")]
        public async Task<ActionResult<ServiceResponse<AttackResultDto>>> MainGunAttack(MainGunAttackDto request)
        {

            return Ok(await _battleService.MainGunAttack(request));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("SpecialWeapon")]
        public async Task<ActionResult<ServiceResponse<SpecialWaeponAttackDto>>> SpecialWaeponAttack(SpecialWaeponAttackDto request)
        {

            return Ok(await _battleService.SpecialWaeponAttack(request));
        }

        [HttpPost("Battle")]
        public async Task<ActionResult<ServiceResponse<BattleLogDto>>> Battle(BattleRequestDto request)
        {
            return Ok(await _battleService.Battle(request));
        }

        [HttpPost("TeamBattle")]
        public async Task<ActionResult<ServiceResponse<BattleLogDto>>> TeamBattle(TeamBattleRequestDto request)
        {
            return Ok(await _battleService.TeamBattle(request));
        }

        [AllowAnonymous]
        [HttpGet("Higscores")]
        public async Task<ActionResult<ServiceResponse<List<HighScoreDto>>>> Highscores()
        {
            return Ok(await _battleService.GetHighscores());
        }



    }
}
 