using System;
using System.Collections.Generic;
using System.Linq;
using SmartAssetTracking.Entities.Asset;
using SmartAssetTracking.Services;

namespace SmartAssetTracking.Application
{
    public static class AssetListMenu
    {
        private static readonly ComputerAssetService _computerService = new ComputerAssetService();
        private static readonly MobileAssetService _mobileService = new MobileAssetService();

        private enum FilterType { All, Computers, Mobiles }
        private enum SortBy { None, Brand, PurchaseDate, Price }

        public static void Show()
        {
            var filter = FilterType.All;
            var sortBy = SortBy.None;
            bool descending = false;
            bool running = true;

            // Default: show current list once on entry
            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== Asset List ===");
                Console.WriteLine($"Filter: {filter}    Sort: {(sortBy == SortBy.None ? "Default" : sortBy.ToString())} {(descending ? "(desc)" : "(asc)")}");
                Console.WriteLine();

                var assets = _computerService.GetAssets().Cast<Asset>().Concat(_mobileService.GetAssets()); // base set
                var view = ApplyFilter(assets, filter);
                view = ApplySort(view, sortBy, descending);

                DisplayAssets(view);

                Console.WriteLine();
                Console.WriteLine("Options:");
                Console.WriteLine("1. Change filter (All / Computers / Mobiles)");
                Console.WriteLine("2. Change sort (Brand / PurchaseDate / Price)");
                Console.WriteLine("3. Toggle sort direction (asc/desc)");
                Console.WriteLine("4. Refresh");
                Console.WriteLine("5. Return to previous menu");
                Console.Write("\nSelect option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        filter = PromptForFilter(filter);
                        break;
                    case "2":
                        sortBy = PromptForSort(sortBy);
                        break;
                    case "3":
                        descending = !descending;
                        break;
                    case "4":
                        break; // loop will refresh
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press Enter to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private static IEnumerable<Asset> ApplyFilter(IEnumerable<Asset> assets, FilterType filter)
        {
            return filter switch
            {
                FilterType.Computers => assets.Where(a => a is ComputerAsset),
                FilterType.Mobiles => assets.Where(a => a is MobileAsset),
                _ => assets
            };
        }

        private static IEnumerable<Asset> ApplySort(IEnumerable<Asset> assets, SortBy sortBy, bool desc)
        {
            IOrderedEnumerable<Asset>? ordered = sortBy switch
            {
                SortBy.Brand => desc ? assets.OrderByDescending(a => a.Brand) : assets.OrderBy(a => a.Brand),
                SortBy.PurchaseDate => desc ? assets.OrderByDescending(a => a.PurchaseDate) : assets.OrderBy(a => a.PurchaseDate),
                SortBy.Price => desc ? assets.OrderByDescending(a => a.PurchasePriceUSD) : assets.OrderBy(a => a.PurchasePriceUSD),
                _ => null
            };

            return ordered ?? assets;
        }

        private static FilterType PromptForFilter(FilterType current)
        {
            Console.WriteLine();
            Console.WriteLine("Select filter:");
            Console.WriteLine("1. All");
            Console.WriteLine("2. Computers");
            Console.WriteLine("3. Mobiles");
            Console.Write("Choice (enter to keep current): ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) return current;

            return input.Trim() switch
            {
                "1" => FilterType.All,
                "2" => FilterType.Computers,
                "3" => FilterType.Mobiles,
                _ => current
            };
        }

        private static SortBy PromptForSort(SortBy current)
        {
            Console.WriteLine();
            Console.WriteLine("Select sort:");
            Console.WriteLine("1. Brand");
            Console.WriteLine("2. Purchase Date");
            Console.WriteLine("3. Price");
            Console.WriteLine("4. Default (no custom sort)");
            Console.Write("Choice (enter to keep current): ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) return current;

            return input.Trim() switch
            {
                "1" => SortBy.Brand,
                "2" => SortBy.PurchaseDate,
                "3" => SortBy.Price,
                "4" => SortBy.None,
                _ => current
            };
        }

        private static void DisplayAssets(IEnumerable<Asset> assets)
        {
            Console.WriteLine(string.Format("{0,-5} {1,-10} {2,-10} {3,-15} {4,-12:yyyy-MM-dd} {5,-10:C}", "ID", "Type", "Brand", "Model", "Purchased", "Price USD"));
            Console.WriteLine(new string('-', 70));

            foreach (var asset in assets)
            {
                ConsoleColor statusColor = asset.GetStatusColor();
                Console.ForegroundColor = statusColor;

                string typeLabel = asset is ComputerAsset ? ((ComputerAsset)asset).FormFactor.ToString() : ((MobileAsset)asset).DeviceType.ToString();
                Console.WriteLine(string.Format("{0,-5} {1,-10} {2,-10} {3,-15} {4,-12:yyyy-MM-dd} {5,-10:F2}",
                    asset.Id, typeLabel, asset.Brand, asset.ModelName, asset.PurchaseDate, asset.PurchasePriceUSD));
            }

            Console.ResetColor();
        }
    }
}