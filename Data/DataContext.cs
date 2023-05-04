using WarshipsRPGAlpha.Models;

namespace WarshipsRPGBeta.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
