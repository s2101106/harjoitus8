using Microsoft.EntityFrameworkCore;
namespace harjoitus8
{
    public class UnityHarjoitusDBContext : DbContext
    {
        public UnityHarjoitusDBContext(DbContextOptions options) : base 
            (options){ }
        public DbSet<Quest> Quests { get; set; }
    }
}
