using System;
using System.Collections.Generic;
using SmartAssetTracking.Entities;
using SmartAssetTracking.Services;
using SmartAssetTracking.Application.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace SmartAssetTracking.Application.Menus
{
    internal static class AdvancedSearchMenu
    {
        private static readonly AssetService _assetService = new AssetService();

        public static void Show()
        {
            while(true)
            {

                Console.Clear();
                Console.WriteLine("=== Advanced Search ===");
                Console.WriteLine("1. Search by Brand");
                Console.WriteLine("2. Search by Model");
                Console.WriteLine("3. Search by Office");
                Console.WriteLine("4. Search by Purchase Year");
                Console.WriteLine("0. Back to previous menu");
                Console.Write("\nSelect option: ");

                string choice = Console.ReadLine() ?? "";
                if (choice == "0") return;

                string criteria = choice switch
                {
                    "1" => "brand",
                    "2" => "model",
                    "3" => "office",
                    "4" => "year",
                    _ => ""
                };

                if (string.IsNullOrEmpty(criteria))
                {
                    PrintHelper.PrintError("Invalid option choice. Try again.");
                    Console.ReadLine();
                    continue;
                }

                Console.Write($"Enter search term for {criteria.ToUpper()}: ");
                string query = Console.ReadLine() ?? "";

                List<Asset> results = _assetService.SearchAssets(criteria, query);

                Console.Clear();
                Console.WriteLine("==================== SEARCH RESULT ====================");
                Console.WriteLine($"\nSearch: {criteria} = {query}\n");
                Console.WriteLine(string.Format("{0,-5} {1,-10} {2,-15} {3,-15}", "ID", "Type", "Model", "Office"));
                Console.WriteLine(new string('-', 50));

                foreach (var asset in results)
                {
                    string typeLabel = asset is ComputerAsset ? "Laptop" : "Mobile";
                    string officeName = asset.Office?.OfficeName.Replace(" Office", "") ?? "Unassigned";

                    Console.WriteLine(string.Format("{0,-5} {1,-10} {2,-15} {3,-15}",
                        asset.Id, typeLabel, asset.ModelName, officeName));
                }

                Console.WriteLine("\n=======================================================");
                Console.WriteLine("Press Enter to return...");
                Console.ReadLine();
            }
        }
    }
}