using System;
using System.Collections.Generic;
using SmartAssetTracking.Entities;
using SmartAssetTracking.Services;

namespace SmartAssetTracking.Application.Menus
{
    internal static class OfficeReportMenu
    {

        public static void Show()
        {
            var officeService = new OfficeService();
            Console.Clear();
            List<Office> offices = officeService.GetOfficesWithAssets();

            if (offices.Count == 0)
            {
                Console.WriteLine("No operational office nodes located in the system database. Run seed script first.");
                Console.ReadLine();
                return;
            }

            foreach (var office in offices)
            {
                Console.WriteLine($"\n==================== {office.OfficeName.ToUpper()} ====================");
                Console.WriteLine();
                Console.WriteLine(string.Format("{0,-5} {1,-10} {2,-15} {3,-20}", "ID", "Asset", "Brand", "Price"));
                Console.WriteLine(new string('-', 55));

                decimal totalOfficeValueLocal = 0;

                foreach (var asset in office.Assets)
                {
                    // 🎨 Apply your existing Level 2 custom status color logic globally across the line
                    Console.ForegroundColor = asset.GetStatusColor();

                    string assetTypeLabel = asset is ComputerAsset ? "Laptop" : "Mobile";

                    // 💱 Uses your asset localized calculation method
                    string localizedPriceString = asset.GetFormattedLocalPrice();

                    Console.WriteLine(string.Format("{0,-5} {1,-10} {2,-15} {3,-20}",
                        asset.Id, assetTypeLabel, asset.Brand, localizedPriceString));

                    totalOfficeValueLocal += asset.GetLocalPrice();
                }

                Console.ResetColor(); // Reset color layout back to default gray
                Console.WriteLine(new string('-', 55));

                // Format total output suffix correctly matching your currency codes
                string totalSuffix = office.CurrencyCode == "SEK" ? $" {office.CurrencyCode}" : "";
                string totalPrefix = office.CurrencyCode != "SEK" ? $"{office.CurrencyCode} " : "";

                Console.WriteLine($"Total Office Value: {totalPrefix}{totalOfficeValueLocal:N2}{totalSuffix}");
                Console.WriteLine(new string('=', 60));
                Console.WriteLine();
            }

            Console.WriteLine("\nPress Enter to return to the Main Menu...");
            Console.ReadLine();
        }
    }
}