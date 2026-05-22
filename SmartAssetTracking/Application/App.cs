using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using SmartAssetTracking.Application.Menus;
using SmartAssetTracking.Data;
using SmartAssetTracking.Entities;
using System;
using System.Linq;

namespace SmartAssetTracking.Application
{
    public class App
    {
        public static void Run()
        {
            using (var ctx = new AssetDbContext())
            {
                // Force migration updates to ensure new columns are always pushed cleanly
                ctx.Database.Migrate();

                // Run seeding logic
                SeedData(ctx);
            }

            // Launch UI
            MainMenu.Show();
        }

        private static void SeedData(AssetDbContext ctx)
        {
            // Check if ANY assets exist across either table. If data exists, skip seeding!
            if (ctx.ComputerAssets.Any() || ctx.MobileAssets.Any())
            {
                return;
            }

            Console.WriteLine("Database is empty. Seeding dummy office assets...");

            Office swedenOffice = new Office { OfficeName = "Stockholm Corporate Hub", Country = "Sweden", CurrencyCode = "SEK", ExchangeRateUsd = 10.65 };
            Office usaOffice = new Office { OfficeName = "Silicon Valley Office", Country = "USA", CurrencyCode = "USD", ExchangeRateUsd = 1.0 };
            Office germanyOffice = new Office { OfficeName = "Germany Corporate Office", Country = "Germany", CurrencyCode = "EUR", ExchangeRateUsd = 0.9 };
            Office turkeyOffice = new Office { OfficeName = "Turkey Valley Office", Country = "Turkey", CurrencyCode = "TRY", ExchangeRateUsd = 46.0 };

            ctx.Offices.AddRange(swedenOffice, usaOffice);

            // Save now to generate valid relational database IDs for the offices!
            ctx.SaveChanges();

            // Add Computer Assets
            ctx.ComputerAssets.AddRange(
                new ComputerAsset
                {
                    Brand = "Lenovo",
                    ModelName = "ThinkPad X1 Carbon",
                    FormFactor = ComputerFormFactor.Laptop,
                    PurchasePriceUSD = 1499.99m,
                    PurchaseDate = DateTime.Now.AddMonths(-18),
                    SerialNumber = "LNV-THINK-9821X",
                    OfficeId = swedenOffice.Id // Assigned to Sweden
                },
                new ComputerAsset
                {
                    Brand = "Apple",
                    ModelName = "Mac Studio",
                    FormFactor = ComputerFormFactor.Desktop,
                    PurchasePriceUSD = 1999.00m,
                    PurchaseDate = DateTime.Now.AddMonths(-6),
                    SerialNumber = "APL-STUDIO-4412M",
                    OfficeId = swedenOffice.Id // Assigned to Sweden
                },
                new ComputerAsset
                {
                    Brand = "Dell",
                    ModelName = "OptiPlex 7000",
                    FormFactor = ComputerFormFactor.Desktop,
                    PurchasePriceUSD = 849.50m,
                    PurchaseDate = DateTime.Now.AddYears(-2),
                    SerialNumber = "DLL-OPTIPLEX-009A",
                    OfficeId = usaOffice.Id // FIXED: Was missing an OfficeId! Assigned to USA
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
                    SerialNumber = "APL-IPHONE-7731P",
                    OfficeId = swedenOffice.Id // Assigned to Sweden
                },
                new MobileAsset
                {
                    Brand = "Samsung",
                    ModelName = "Galaxy S24 Ultra",
                    DeviceType = MobileDeviceType.Phone,
                    PurchasePriceUSD = 1299.99m,
                    PurchaseDate = DateTime.Now.AddMonths(-1),
                    SerialNumber = "SSG-GALAXY-4410U",
                    OfficeId = swedenOffice.Id // Assigned to Sweden
                },
                new MobileAsset
                {
                    Brand = "Apple",
                    ModelName = "iPad Air",
                    DeviceType = MobileDeviceType.Tablet,
                    PurchasePriceUSD = 599.00m,
                    PurchaseDate = DateTime.Now.AddYears(-1),
                    SerialNumber = "APL-IPADAIR-8821A",
                    OfficeId = usaOffice.Id // FIXED: Was missing an OfficeId! Assigned to USA
                }
            );

            // 3. Commit all tracking items safely to database
            ctx.SaveChanges();
            Console.WriteLine("Seeding completed successfully!");
            System.Threading.Thread.Sleep(1000);
        }
    }
}