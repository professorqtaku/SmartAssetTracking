using SmartAssetTracking.Services;
using SmartAssetTracking.Entities;
using System;
using SmartAssetTracking.Application.Helpers;

namespace SmartAssetTracking.Application.Menus
{
    internal class UpdateAssetMenu
    {
        private readonly static AssetService _assetService = new AssetService();

        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("=== Modify Asset Details ===");
            Console.Write("Enter the ID of the asset you want to update: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                PrintHelper.PrintError("Invalid ID structure.");
                Console.ReadLine();
                return;
            }

            string type = MainMenu.GetAssetType();

            // 1. Locate the asset first across your tables
            Asset assetToUpdate = _assetService.GetAssetByIdAndType(id, type);

            if (assetToUpdate == null)
            {
                PrintHelper.PrintError("Asset not found in the database.");
                Console.ReadLine();
                return;
            }

            // 2. Show the existing data of the chosen asset
            Console.WriteLine("\n-------------------------------------------");
            Console.WriteLine($"Found Asset: {assetToUpdate.GetType().Name} (ID: {assetToUpdate.Id})");
            Console.WriteLine($"Current Brand: {assetToUpdate.Brand}");
            Console.WriteLine($"Current Model: {assetToUpdate.ModelName}");
            Console.WriteLine($"Current Price: ${assetToUpdate.PurchasePriceUSD:F2}");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Leave field empty and press Enter to keep current data.\n");

            // 3. Collect new inputs or fall back to old values if left empty
            Console.Write($"Enter new Brand [{assetToUpdate.Brand}]: ");
            string brandInput = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(brandInput))
            {
                assetToUpdate.Brand = brandInput.Trim();
            }

            Console.Write($"Enter new Model Name [{assetToUpdate.ModelName}]: ");
            string modelInput = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(modelInput))
            {
                assetToUpdate.ModelName = modelInput.Trim();
            }

            Console.Write($"Enter new Price USD [{assetToUpdate.PurchasePriceUSD:F2}]: ");
            string priceInput = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(priceInput))
            {
                if (decimal.TryParse(priceInput, out decimal newPrice) && newPrice >= 0)
                {
                    assetToUpdate.PurchasePriceUSD = newPrice;
                }
                else
                {
                    PrintHelper.PrintError("Invalid price input. Keeping original value.");
                }
            }

            // If there are specialized table properties, handle them using pattern matching
            if (assetToUpdate is ComputerAsset computer)
            {
                Console.Write($"Enter new Form Factor [{computer.FormFactor}]: ");
                string formInput = Console.ReadLine() ?? string.Empty;
                computer.FormFactor = AssetParser.ParseComputerFormFactor(formInput, computer.FormFactor);
            }
            else if (assetToUpdate is MobileAsset mobile)
            {
                Console.Write($"Enter new Device Type [{mobile.DeviceType}]: ");
                string deviceInput = Console.ReadLine() ?? string.Empty;
                mobile.DeviceType = AssetParser.ParseMobileDeviceType(deviceInput, mobile.DeviceType);
            }

            // 4. Save the modified object back into the database
            _assetService.UpdateAssetSafely(assetToUpdate);

            PrintHelper.PrintSuccess("Changes committed successfully! Press Enter to return...");
            Console.ReadLine();
        }
    }
}