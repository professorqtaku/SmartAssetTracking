using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using SmartAssetTracking.Entities;
using SmartAssetTracking.Services;
using SmartAssetTracking.Application.Helpers;

namespace SmartAssetTracking.Application.Menus
{
    internal static class DataExportMenu
    {
        private static readonly AssetService _assetService = new AssetService();

        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("=== Business Data Export Engine ===");
            Console.WriteLine("1. Export to Plain Text (.txt)");
            Console.WriteLine("2. Export to Comma-Separated Values (.csv)");
            Console.WriteLine("3. Export to JSON Structure (.json)");
            Console.Write("\nSelect export target: ");

            string choice = Console.ReadLine() ?? "";
            List<Asset> assets = _assetService.GetAllAssetsSorted();

            if (assets.Count == 0)
            {
                PrintHelper.PrintError("No source assets available in memory to export.");
                Console.ReadLine();
                return;
            }

            var projectDirectory = GetDownloadDirectory();

            var exportTimeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            switch (choice)
            {
                case "1":
                    ExportAsTxt(assets, Path.Combine(projectDirectory, $"AssetReport-{exportTimeStamp}.txt"));
                    break;
                case "2":
                    ExportAsCsv(assets, Path.Combine(projectDirectory, $"AssetReport-{exportTimeStamp}.csv"));
                    break;
                case "3":
                    ExportAsJson(assets, Path.Combine(projectDirectory, $"AssetReport-{exportTimeStamp}.json"));
                    break;
                default:
                    PrintHelper.PrintError("Invalid export selection context.");
                    Console.ReadLine();
                    break;
            }
        }

        private static void ExportAsTxt(List<Asset> assets, string filePath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("=== GLOBAL CAPITAL ASSETS REPORT ===");
            foreach (var a in assets)
            {
                sb.AppendLine($"ID: {a.Id} | {a.Brand} {a.ModelName} | Price: ${a.PurchasePriceUSD:F2} | Office: {a.Office?.OfficeName ?? "N/A"}");
            }
            File.WriteAllText(filePath, sb.ToString());
            PrintHelper.PrintSuccess($"TXT Report written successfully to:\n{filePath}\n\nPress Enter...");
            Console.ReadLine();
        }

        private static void ExportAsCsv(List<Asset> assets, string filePath)
        {
            var sb = new StringBuilder();
            // Match your CSV specification header layout exactly
            sb.AppendLine("Id,Type,Brand,Model,Office,Price");

            foreach (var a in assets)
            {
                string typeLabel = a is ComputerAsset ? "Laptop" : "Mobile";
                string officeLabel = a.Office?.OfficeName.Replace(" Office", "") ?? "Unassigned";

                // Converted localized clean format or flat base value matching specs
                decimal priceVal = a.Office?.CurrencyCode == "SEK" ? a.GetLocalPrice() : a.PurchasePriceUSD;

                sb.AppendLine($"{a.Id},{typeLabel},{a.Brand},{a.ModelName},{officeLabel},{priceVal:F0}");
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
            PrintHelper.PrintSuccess($"CSV Data matrix exported to:\n{filePath}\n\nPress Enter...");
            Console.ReadLine();
        }

        private static void ExportAsJson(List<Asset> assets, string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            // Project clean anonymous shape objects to prevent serialization circular loops with the Office entity model context
            var dtoSummary = new List<object>();
            foreach (var a in assets)
            {
                dtoSummary.Add(new
                {
                    Id = a.Id,
                    AssetClass = a.GetType().Name,
                    Brand = a.Brand,
                    Model = a.ModelName,
                    PurchaseDate = a.PurchaseDate.ToString("yyyy-MM-dd"),
                    PriceUSD = a.PurchasePriceUSD,
                    AssignedOffice = a.Office?.OfficeName ?? "None"
                });
            }

            string jsonString = JsonSerializer.Serialize(dtoSummary, options);
            File.WriteAllText(filePath, jsonString);

            PrintHelper.PrintSuccess($"JSON Configuration schema saved to:\n{filePath}\n\nPress Enter...");
            Console.ReadLine();
        }

        private static string GetDownloadDirectory ()
        {
            string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo? directory = new DirectoryInfo(exeDirectory);

            // Climb up 4 levels (netX.0 -> Debug -> bin -> Project Root)
            for (int i = 0; i < 4 && directory != null; i++)
            {
                directory = directory.Parent;
            }

            string projectDirectory = directory?.FullName ?? exeDirectory;

            // TARGET DOWNLOAD FOLDER: Placed cleanly inside the base project directory
            string downloadFolderPath = Path.Combine(projectDirectory, "AssetDownloads");

            // Automatically initializes the folder on disk if it doesn't exist yet
            if (!Directory.Exists(downloadFolderPath))
            {
                Directory.CreateDirectory(downloadFolderPath);
            }

            return downloadFolderPath;
        }
    }
}