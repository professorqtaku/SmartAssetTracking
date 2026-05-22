using System;
using System.Collections.Generic;
using System.Linq;
using SmartAssetTracking.Entities;
using SmartAssetTracking.Services;

namespace SmartAssetTracking.Application.Menus
{
    internal static class GlobalSummaryMenu
    {
        private static readonly AssetService _assetService = new AssetService();

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
            
            DateTime expirationThreshold = DateTime.Now.AddYears(-3).AddMonths(3);
            var expiringAssets = allAssets.Where(a => a.PurchaseDate <= expirationThreshold).ToList();

            if (!expiringAssets.Any())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("All system hardware nodes operating within nominal lifecycle parameters.");
                Console.ResetColor();
            }
            else
            {
                foreach (var asset in expiringAssets)
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Keep your status highlights in color!
                    Console.WriteLine($"- {asset.Brand} {asset.ModelName} (Purchased: {asset.PurchaseDate:yyyy-MM-dd})");
                }
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.WriteLine("Most Expensive Capital Assets");
            Console.WriteLine(new string('-', 45));

            var expensiveAssets = allAssets
                .OrderByDescending(a => a.PurchasePriceUSD)
                .Take(3)
                .ToList();

            foreach (var asset in expensiveAssets)
            {
                string assetType = asset is ComputerAsset ? "Computer" : "Mobile";
                string location = asset.Office != null ? asset.Office.OfficeName : "Unassigned Location";

                Console.WriteLine($"- [{assetType}] {asset.Brand} {asset.ModelName} | Cost: ${asset.PurchasePriceUSD:N2} ({location})");
            }

            Console.WriteLine();
            Console.WriteLine(new string('=', 48));
            Console.WriteLine("\nPress Enter to return to the Main Menu...");
            Console.ReadLine();
        }
    }
}