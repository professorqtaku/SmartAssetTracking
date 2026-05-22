using System;
using System.Collections.Generic;
using System.Linq;
using SmartAssetTracking.Entities;
using SmartAssetTracking.Services;

namespace SmartAssetTracking.Application.Menus
{
    internal static class GlobalSummaryMenu
    {
        public static void Show()
        {
            Console.Clear();

            var officeService = new OfficeService();
            var assetService = new AssetService();
            List<Office> offices = officeService.GetOfficesWithAssets();
            List<Asset> allAssets = assetService.GetAllAssetsSorted();

            Console.WriteLine("==================== REPORT ====================");
            Console.WriteLine();
            Console.WriteLine("Office Asset Counts");
            Console.WriteLine(new string('-', 45));

            foreach (var office in offices)
            {
                Console.WriteLine(string.Format("{0,-20} : {1}", office.OfficeName, office.Assets.Count));
            }

            Console.WriteLine();
            Console.WriteLine("Assets Near Expiration");
            Console.WriteLine(new string('-', 45));

            // Find tracking elements that have passed or are within 3 months of their 3-year life cycle marker
            DateTime expirationThreshold = DateTime.Now.AddYears(-3).AddMonths(3);

            var expiringAssets = allAssets
                .Where(a => a.PurchaseDate <= expirationThreshold)
                .ToList();

            if (expiringAssets.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("No corporate hardware assets are currently flagged close to expiration.");
                Console.ResetColor();
            }
            else
            {
                foreach (var asset in expiringAssets)
                {
                    // Highlight expiring line rows dynamically to draw attention 
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"- {asset.Brand} {asset.ModelName} (Purchased: {asset.PurchaseDate:yyyy-MM-dd})");
                }
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.WriteLine(new string('=', 48));
            Console.WriteLine("\nPress Enter to return to the Main Menu...");
            Console.ReadLine();
        }
    }
}