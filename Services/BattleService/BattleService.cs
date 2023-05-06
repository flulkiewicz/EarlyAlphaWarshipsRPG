﻿using WarshipsRPGBeta.Data;

namespace WarshipsRPGAlpha.Services.BattleService
{
    public class BattleService : IBattleService
    {
        private readonly DataContext _context;
        private readonly IShipService _shipService;

        public BattleService(DataContext context, IShipService shipService)
        {
            _context = context;
            _shipService = shipService;
        }

        private static int UseSpecialWaepon(Ship attacker, Ship opponent, SpecialWaepon specialWaepon)
        {
            if (attacker.SpecialWaepons == null)
                throw new Exception("No special waepon of this type equipped!");

            int damage = specialWaepon!.Damage + (new Random().Next(attacker.FirePower));
            damage -= new Random().Next(opponent.Defeats);

            if (damage > 0)
                opponent.HitPoints -= damage;

            return damage;
        }

        private static int UseMainGun(Ship attacker, Ship opponent)
        {
            if (attacker.MainGun == null)
                throw new Exception("No main gun equipped!");

            int damage = attacker.MainGun.Damage + (new Random().Next(attacker.FirePower));
            damage -= new Random().Next(opponent.Defeats);

            if (damage > 0)
                opponent.HitPoints -= damage;
            return damage;
        }

        public async Task<ServiceResponse<BattleLogDto>> Battle(BattleRequestDto request)
        {
            var response = new ServiceResponse<BattleLogDto>
            {
                Data = new BattleLogDto()
            };

            try
            {
                var ships = await _context.Ships
                    .Include(s => s.MainGun)
                    .Include(s => s.SpecialWaepons)
                    .Where(s => request.ShipIds.Contains(s.Id))
                    .ToListAsync();

                bool victory = false;
                while(!victory)
                {
                    foreach (var attacker in ships)
                    {
                        var opponents = ships.Where(s => s.Id != attacker.Id).ToList();
                        var opponent = opponents[new Random().Next(opponents.Count)];

                        int damage = 0;
                        List<string> attacksUsed = new List<string>();

                        int roll = new Random().Next(0, 21);
                        if (roll == 21 && attacker.MainGun != null && attacker.SpecialWaepons != null)
                        {
                            var specialWaepon = attacker.SpecialWaepons[new Random().Next(attacker.SpecialWaepons.Count)];
                            attacksUsed.Add(specialWaepon.Name);
                            attacksUsed.Add(attacker.MainGun.Name);

                            damage = UseMainGun(attacker, opponent) + UseSpecialWaepon(attacker, opponent, specialWaepon);
                        }
                        else if(roll >= 15 && roll < 21)
                        {
                            var specialWaepon = attacker.SpecialWaepons![new Random().Next(attacker.SpecialWaepons.Count)];
                            attacksUsed.Add(specialWaepon.Name);

                            damage = UseSpecialWaepon(attacker, opponent, specialWaepon);
                        }
                        else if(roll > 0 && roll < 15)
                        {
                            attacksUsed.Add(attacker.MainGun!.Name);
                            damage = UseMainGun(attacker, opponent);
                        }
                        else
                        {
                            response.Data.BattleLog.Add($"{attacker.Name}s crew failed to attack!");
                        }

                        foreach(var attack in attacksUsed)
                        {
                            response.Data.BattleLog.Add($"{attacker.Name} attacks {opponent.Name} using {attack} with {damage} damage");
                        }

                        if (opponent.HitPoints <= 0)
                        {
                            victory = true;
                            attacker.Victories++;
                            opponent.Defeats++;
                            response.Data.BattleLog.Add($"{opponent.Name} has been sink!");
                            response.Data.BattleLog.Add($"{attacker.Name} is victorious!");
                            break;
                        }
                    }
                }

                ships.ForEach(s =>
                {
                    s.Fights++;
                    s.HitPoints = 2000;
                });

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<AttackResultDto>> MainGunAttack(MainGunAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();
            try
            {
                var attacker = await _context.Ships
                    .Include(s => s.MainGun)
                    .FirstOrDefaultAsync(s => s.Id == request.AttackerId);
                var opponent = await _context.Ships.FirstOrDefaultAsync(s => s.Id == request.OpponnentId);

                if (attacker == null || opponent == null || attacker.MainGun == null)
                    throw new Exception();
                int damage = UseMainGun(attacker, opponent);

                if (opponent.HitPoints <= 0)
                    response.Message = $"{opponent.Name} has been sink. Good job captain!";

                await _context.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackersHp = attacker.HitPoints,
                    OpponentsHp = opponent.HitPoints,
                    Damage = damage
                };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }

            return response;
        }

        

        public async Task<ServiceResponse<AttackResultDto>> SpecialWaeponAttack(SpecialWaeponAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();
            try
            {
                var attacker = await _context.Ships
                    .Include(s => s.SpecialWaepons)
                    .FirstOrDefaultAsync(s => s.Id == request.AttackerId);

                var opponent = await _context.Ships.FirstOrDefaultAsync(s => s.Id == request.OpponentId);

                if (attacker == null || opponent == null || attacker.SpecialWaepons == null)
                    throw new Exception();

                var specialWaepon = attacker.SpecialWaepons.FirstOrDefault(s => s.Id == request.SpecialWaeponId);
                if (specialWaepon == null)
                {
                    response.Success = false;
                    response.Message = $"{attacker.Name} doesnt have that in equipment!";
                    return response;
                }

                int damage = UseSpecialWaepon(attacker, opponent, specialWaepon);

                if (opponent.HitPoints <= 0)
                    response.Message = $"{opponent.Name} has been sink. Good job captain!";

                await _context.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackersHp = attacker.HitPoints,
                    OpponentsHp = opponent.HitPoints,
                    Damage = damage
                };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }

            return response;
        }

        
    }
}
