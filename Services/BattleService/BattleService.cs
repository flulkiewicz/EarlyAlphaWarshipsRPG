using WarshipsRPGBeta.Data;

namespace WarshipsRPGAlpha.Services.BattleService
{
    public class BattleService : IBattleService
    {
        private readonly DataContext _context;
        private readonly IShipService _shipService;
        private readonly IMapper _mapper;

        public BattleService(DataContext context, IShipService shipService, IMapper mapper)
        {
            _context = context;
            _shipService = shipService;
            _mapper = mapper;
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
        private static int BattleLogic(ServiceResponse<BattleLogDto> response, Ship attacker, Ship opponent, int damage, List<string> attacksUsed)
        {
            int roll = new Random().Next(0, 21);
            if (roll == 21 && attacker.MainGun != null && attacker.SpecialWaepons != null)
            {
                var specialWaepon = attacker.SpecialWaepons[new Random().Next(attacker.SpecialWaepons.Count)];
                attacksUsed.Add(specialWaepon.Name);
                attacksUsed.Add(attacker.MainGun.Name);

                damage = UseMainGun(attacker, opponent) + UseSpecialWaepon(attacker, opponent, specialWaepon);
            }
            else if (roll >= 15 && roll < 21)
            {
                var specialWaepon = attacker.SpecialWaepons![new Random().Next(attacker.SpecialWaepons.Count)];
                attacksUsed.Add(specialWaepon.Name);

                damage = UseSpecialWaepon(attacker, opponent, specialWaepon);
            }
            else if (roll > 0 && roll < 15)
            {
                attacksUsed.Add(attacker.MainGun!.Name);
                damage = UseMainGun(attacker, opponent);
            }
            else
            {
                response.Data!.BattleLog.Add($"{attacker.Name}s crew failed to attack!");
            }

            foreach (var attack in attacksUsed)
            {
                response.Data!.BattleLog.Add($"{attacker.Name} attacks {opponent.Name} using {attack} with {damage} damage");
            }

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

                        damage = BattleLogic(response, attacker, opponent, damage, attacksUsed);

                        if (opponent.HitPoints <= 0)
                        {
                            victory = true;
                            attacker.Victories++;
                            opponent.Defeats++;
                            response.Data.BattleLog.Add($"{opponent.Name} has been sunk!");
                            response.Data.BattleLog.Add($"{attacker.Name} is victorious! With {attacker.HitPoints} left");
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

        public async Task<ServiceResponse<BattleLogDto>> TeamBattle(TeamBattleRequestDto request)
        {
            var response = new ServiceResponse<BattleLogDto>
            {
                Data = new BattleLogDto()
            };

            try
            {
                var team1 = await _context.Ships
                    .Include(s => s.MainGun)
                    .Include(s => s.SpecialWaepons)
                    .Where(s => request.Team1ShipIds.Contains(s.Id))
                    .ToListAsync();

                var team2 = await _context.Ships
                    .Include(s => s.MainGun)
                    .Include(s => s.SpecialWaepons)
                    .Where(s => request.Team2ShipIds.Contains(s.Id))
                    .ToListAsync();

                var allEngagedShips = new List<Ship>()
                    .Concat(team1)
                    .Concat(team2)
                    .ToList();

                bool victory = false;
                while (!victory)
                {
                    foreach (var attacker in team1)
                    {
                        var opponents = team2;
                        var opponent = opponents[new Random().Next(opponents.Count)];

                        int damage = 0;
                        List<string> attacksUsed = new List<string>();

                        damage = BattleLogic(response, attacker, opponent, damage, attacksUsed);

                        if (opponent.HitPoints <= 0)
                        {
                            attacker.Victories++;
                            opponent.Defeats++;
                            response.Data.BattleLog.Add($"{opponent.Name} has been sunk!");
                            response.Data.BattleLog.Add($"{attacker.Name} has destroyed {opponent.Name}!");
                            team2.Remove(opponent);
                        }

                        if(opponents.Count == 0)
                        {
                            response.Data.BattleLog.Add("Team1 won.");
                            victory = true;
                            break;
                        }

                    }

                    foreach (var attacker in team2)
                    {
                        var opponents = team1;
                        var opponent = opponents[new Random().Next(opponents.Count)];

                        int damage = 0;
                        List<string> attacksUsed = new List<string>();

                        damage = BattleLogic(response, attacker, opponent, damage, attacksUsed);

                        if (opponent.HitPoints <= 0)
                        {
                            attacker.Victories++;
                            opponent.Defeats++;
                            response.Data.BattleLog.Add($"{opponent.Name} has been sunk!");
                            response.Data.BattleLog.Add($"{attacker.Name} has destroyed {opponent.Name}!");
                            team1.Remove(opponent);
                        }

                        if (opponents.Count == 0)
                        {
                            response.Data.BattleLog.Add("Team2 won.");
                            victory = true;
                            break;
                        }

                    }
                }

                allEngagedShips.ForEach(s =>
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

        public async Task<ServiceResponse<List<HighScoreDto>>> GetHighscores()
        {
            var response = new ServiceResponse<List<HighScoreDto>>();

            var ships = await _context.Ships
                .Where(s => s.Fights > 0)
                .OrderByDescending(s => s.Victories)
                .ThenBy(s => s.Defeats)
                .ToListAsync();

            response.Data = ships.Select(s => _mapper.Map<HighScoreDto>(s)).ToList();

            return response;
        }

        
    }
}
