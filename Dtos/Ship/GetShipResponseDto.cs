﻿using WarshipsRPGAlpha.Dtos.MainGun;
using WarshipsRPGAlpha.Dtos.SpecialWaepon;

namespace WarshipsRPGAlpha.Dtos.Ship
{
    public class GetShipResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "LS3";
        public int HitPoints { get; set; } = 180;
        public int FirePower { get; set; } = 10;
        public int Armor { get; set; } = 20;
        public int Crew { get; set; } = 3;
        public ShipClass Class { get; set; } = ShipClass.Boat;
        public ShipFaction Faction { get; set; } = ShipFaction.Germany;
        public GetMainGunDto? MainGun { get; set; }
        public List<GetSpecialWaeponDto>? SpecialWaepons { get; set; }
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}
