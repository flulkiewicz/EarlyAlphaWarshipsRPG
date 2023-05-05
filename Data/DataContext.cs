using WarshipsRPGAlpha.Models;

namespace WarshipsRPGBeta.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpecialWaepon>().HasData(
                    new SpecialWaepon { Id = 1, Name= "Torpedo", Damage= 250 },
                    new SpecialWaepon { Id = 2, Name = "Rocket", Damage = 150 },
                    new SpecialWaepon { Id = 3, Name = "Depth Charge", Damage = 300 }
                );
        }

        public DbSet<Ship> Ships { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MainGun> MainGuns { get; set; }
        public DbSet<SpecialWaepon> SpecialWaepons { get; set;}
    }
}
