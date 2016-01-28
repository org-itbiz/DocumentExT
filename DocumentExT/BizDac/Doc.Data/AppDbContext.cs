using Doc.Data.Model;
using System.Data.Entity;

namespace Doc.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<StoreProductT> StoreProduct { get; set; }

        static AppDbContext()
        {
            Database.SetInitializer<AppDbContext>(null);
        }

        public AppDbContext()
            : base("MakersnContext")
        {
        }
    }
}