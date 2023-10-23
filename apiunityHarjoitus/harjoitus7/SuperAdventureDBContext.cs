using Microsoft.EntityFrameworkCore;
namespace harjoitus7
{
    public class SuperAdventureDBContext : DbContext
    {
        public SuperAdventureDBContext(DbContextOptions options) : base
            (options){ }
        public DbSet<Stat> Stats { get; set; }
    }
}
