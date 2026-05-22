using Microsoft.IdentityModel.Tokens;
using SmartAssetTracking.Application.Helpers;
using SmartAssetTracking.Entities.Asset;
using SmartAssetTracking.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAssetTracking.Application.Menus
{
    internal class CreateAssetMenu
    {
        private static readonly AssetService _assetService = new AssetService();
        public static void CreateComputer()
        {
            Console.Clear();
            var comp = new ComputerAsset();
            PopulateBaseAssetData(comp);

            ComputerFormFactor formFactor;
            Console.WriteLine("Enter Form Factor");
            Console.WriteLine("l = Laptop, s = Server, d = Desktop, a = All in One, antything else for Other:");
            string input = Console.ReadLine() ?? String.Empty;

            formFactor = AssetParser.ParseComputerFormFactor(input, ComputerFormFactor.Other);

            comp.FormFactor = formFactor;

            _assetService.AddAsset(comp);
            Console.WriteLine("Computer successfully registered! Press Enter...");
            Console.ReadLine();
        }

        public static void CreateMobile()
        {
            Console.Clear();
            var mob = new MobileAsset();
            PopulateBaseAssetData(mob);

            Console.WriteLine("Enter Device Type (default or p = Phone / t = Tablet): ");
            string deviceInput = Console.ReadLine() ?? String.Empty;

            mob.DeviceType = AssetParser.ParseMobileDeviceType(deviceInput, MobileDeviceType.Other);

            _assetService.AddAsset(mob);
            Console.WriteLine("Mobile Device successfully registered! Press Enter...");
            Console.ReadLine();
        }

        private static void PopulateBaseAssetData(Asset asset)
        {
            Console.WriteLine("* = recuired");
            Console.Write("Enter Brand* : ");
            asset.Brand = Console.ReadLine() ?? "Generic";

            Console.Write("Enter Model Name: ");
            asset.ModelName = Console.ReadLine() ?? "Unknown";

            // Validation logic for user friendly errors
            DateTime purchaseDate;
            while (true)
            {
                Console.Write("Enter Purchase Date (YYYY-MM-DD), leave empty for todays date: ");
                string dateInput = Console.ReadLine() ?? String.Empty;
                if (dateInput.IsNullOrEmpty())
                {
                    purchaseDate = DateTime.Now;
                    break;
                }
                else if (DateTime.TryParse(dateInput, out purchaseDate)) break;
                Console.WriteLine("Invalid Date format. Try again.");
            }
            asset.PurchaseDate = purchaseDate;
            asset.WarrantyExpirationDate = purchaseDate.AddYears(3); // Setting up baseline rule

            decimal price;
            while (true)
            {
                Console.Write("Enter Purchase Price (USD): ");
                if (decimal.TryParse(Console.ReadLine(), out price) && price >= 0) break;
                Console.WriteLine("Invalid price structure. Try again.");
            }
            asset.PurchasePriceUSD = price;

            Console.Write("Enter Serial Number: ");
            asset.SerialNumber = Console.ReadLine() ?? Guid.NewGuid().ToString();
        }
    }
}
