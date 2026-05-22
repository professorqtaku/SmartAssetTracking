using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using SmartAssetTracking.Application.Menus;
using SmartAssetTracking.Data;
using SmartAssetTracking.Entities.Asset;
using System;

namespace SmartAssetTracking.Application
{
    public class App
    {
        public static void Run()
        {
            using (var ctx = new AssetDbContext())
            {
                // 1. Get access to the relational database creation service
                var databaseCreator = ctx.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;

                // 2. Check if the database itself or its tables are missing
                // HasTables() returns false if the DB exists but contains no tables yet
                if (databaseCreator == null || !databaseCreator.Exists() || !databaseCreator.HasTables())
                {
                    Console.WriteLine("Database tables missing. Provisioning schema updates...");
                    ctx.Database.Migrate();
                }

                // 3. Run seeding logic (which has its own safety .Any() checks)
                SeedData(ctx);
            }

            // 4. Launch UI
            MainMenu.Show();
        }
        private static void SeedData(AssetDbContext ctx)
        {
            //Check if ANY assets exist across either table.If data exists, skip seeding!
            if (ctx.ComputerAssets.Any() || ctx.MobileAssets.Any())
            {
                return;
            }

            Console.WriteLine("Database is empty. Seeding dummy office assets...");

            // Add Computer Assets
            ctx.ComputerAssets.AddRange(
                new ComputerAsset
                {
                    Brand = "Lenovo",
                    ModelName = "ThinkPad X1 Carbon",
                    FormFactor = ComputerFormFactor.Laptop,
                    PurchasePriceUSD = 1499.99m,
                    PurchaseDate = DateTime.Now.AddMonths(-18),
                    SerialNumber = "LNV-THINK-9821X"
                },
                new ComputerAsset
                {
                    Brand = "Apple",
                    ModelName = "Mac Studio",
                    FormFactor = ComputerFormFactor.Desktop,
                    PurchasePriceUSD = 1999.00m,
                    PurchaseDate = DateTime.Now.AddMonths(-6),
                    SerialNumber = "APL-STUDIO-4412M"
                },
                new ComputerAsset
                {
                    Brand = "Dell",
                    ModelName = "OptiPlex 7000",
                    FormFactor = ComputerFormFactor.Desktop,
                    PurchasePriceUSD = 849.50m,
                    PurchaseDate = DateTime.Now.AddYears(-2),
                    SerialNumber = "DLL-OPTIPLEX-009A"
                }
            );

            // Add Mobile Assets
            ctx.MobileAssets.AddRange(
                new MobileAsset
                {
                    Brand = "Apple",
                    ModelName = "iPhone 15 Pro",
                    DeviceType = MobileDeviceType.Phone,
                    PurchasePriceUSD = 1099.00m,
                    PurchaseDate = DateTime.Now.AddMonths(-3),
                    SerialNumber = "APL-IPHONE-7731P"
                },
                new MobileAsset
                {
                    Brand = "Samsung",
                    ModelName = "Galaxy S24 Ultra",
                    DeviceType = MobileDeviceType.Phone,
                    PurchasePriceUSD = 1299.99m,
                    PurchaseDate = DateTime.Now.AddMonths(-1),
                    SerialNumber = "SSG-GALAXY-4410U"
                },
                new MobileAsset
                {
                    Brand = "Apple",
                    ModelName = "iPad Air",
                    DeviceType = MobileDeviceType.Tablet,
                    PurchasePriceUSD = 599.00m,
                    PurchaseDate = DateTime.Now.AddYears(-1),
                    SerialNumber = "APL-IPADAIR-8821A"
                }
            );

            // Commit all changes to their respective separate MySQL tables
            ctx.SaveChanges();
            Console.WriteLine("Seeding completed successfully!");
            System.Threading.Thread.Sleep(1000); // Brief pause so user can see it seeded
        }
    }
}