using Microsoft.EntityFrameworkCore;
using SmartAssetTracking.Entities.Asset;

namespace AssetTracking2.Data
{
    public class AssetDbContext : DbContext
    {
        string connectionString = "Server = (localdb)\\mssqllocaldb; Database = lex2026may; Trusted_Connection = True; TrustServerCertificate=True";

        public DbSet<ComputerAsset> ComputerAssets { get; set; }
        public DbSet<MobileAsset> MobileAssets { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
        }
    }
}
