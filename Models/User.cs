﻿using WarshipsRPGAlpha.Services.ShipService;

namespace WarshipsRPGAlpha.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];
        public List<Ship>? Ships { get; set; }
    }
}
