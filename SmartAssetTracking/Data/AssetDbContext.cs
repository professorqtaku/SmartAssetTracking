using Microsoft.EntityFrameworkCore;
using SmartAssetTracking.Entities;

namespace SmartAssetTracking.Data
{
    public class AssetDbContext : DbContext
    {
        string connectionString = "Server = (localdb)\\mssqllocaldb; Database = lex2026may; Trusted_Connection = True; TrustServerCertificate=True";
        
        public DbSet<Office> Offices { get; set; }
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
            // Tells EF Core to use TPC mapping strategy
            modelBuilder.Entity<Asset>().UseTpcMappingStrategy();

            // 💰 FIXED DECIMAL WARNING: Configure money scale and precision for the base property
            modelBuilder.Entity<Asset>()
                .Property(a => a.PurchasePriceUSD)
                .HasColumnType("decimal(18,2)");

            // Map each concrete class to its own standalone table
            modelBuilder.Entity<ComputerAsset>().ToTable("ComputerAssets");
            modelBuilder.Entity<MobileAsset>().ToTable("MobileAssets");
            modelBuilder.Entity<Office>().ToTable("Offices");

            // Establish the clean Foreign Key relationship link
            modelBuilder.Entity<Asset>()
                .HasOne(a => a.Office)
                .WithMany(o => o.Assets)
                .HasForeignKey(a => a.OfficeId);
        }
    }
}
