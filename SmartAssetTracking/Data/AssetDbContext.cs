using Microsoft.EntityFrameworkCore;
using SmartAssetTracking.Entities;

namespace SmartAssetTracking.Data
{
    public class AssetDbContext : DbContext
    {
        string connectionString = "Server = (localdb)\\mssqllocaldb; Database = lex2026may; Trusted_Connection = True; TrustServerCertificate=True";

        public DbSet<ComputerAsset> ComputerAssets { get; set; }
        public DbSet<MobileAsset> MobileAssets { get; set; }

        // This lets you query or add to both tables simultaneously
        public DbSet<Asset> Assets { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // THE MAGIC KEY: Tells EF Core to drop the base table and make 2 separate tables
            modelBuilder.Entity<Asset>().UseTpcMappingStrategy();

            // Map each concrete class to its own standalone table
            modelBuilder.Entity<ComputerAsset>().ToTable("ComputerAssets");
            modelBuilder.Entity<MobileAsset>().ToTable("MobileAssets");

            // prevent EF from tracking base DbSet separately
            modelBuilder.Ignore<Asset>(); // only if you remove the base mapping/DbSet usage
        }
    }
}
