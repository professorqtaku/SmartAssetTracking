using System;
using System.Collections.Generic;

namespace SmartAssetTracking.Entities
{
    public static class AssetConstants
    {
        public const int LifetimeYears = 3;
    }
    public abstract class Asset
    {
        // Core Properties
        public int Id { get; set; }
        public string AssetType { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string ModelName { get; set; } = string.Empty;
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        public decimal PurchasePriceUSD { get; set; } = 0m;
        public string SerialNumber { get; set; } = string.Empty;
        public DateTime WarrantyExpirationDate { get; set; }
        public string? Employee { get; set; }
        public string? OfficeLocation { get; set; }

        public int? OfficeId { get; set; }
        public Office? Office { get; set; }

        // Domain Rule Methods
        public int GetRemainingLifetimeMonths()
        {
            DateTime endOfLifeDate = PurchaseDate.AddYears(3);
            if (DateTime.Now >= endOfLifeDate) return 0;

            return ((endOfLifeDate.Year - DateTime.Now.Year) * 12) + endOfLifeDate.Month - DateTime.Now.Month;
        }

        public ConsoleColor GetStatusColor()
        {
            int monthsLeft = GetRemainingLifetimeMonths();
            if (monthsLeft <= 0) return ConsoleColor.Red;
            if (monthsLeft < 3) return ConsoleColor.Cyan;
            if (monthsLeft < 6) return ConsoleColor.Yellow;
            return ConsoleColor.White;
        }

        public decimal GetLocalPrice()
        {
            if (Office == null) return PurchasePriceUSD;
            return PurchasePriceUSD * (decimal)Office.ExchangeRateUsd;
        }
        public string GetFormattedLocalPrice()
        {
            // Safety Fallback: If the Office relation isn't loaded or assigned, fall back to default USD formatting
            if (Office == null)
            {
                return $"${PurchasePriceUSD:N2}";
            }

            // 1. Calculate the localized numeric price value using our existing method
            decimal localPrice = GetLocalPrice();

            // 2. Format based on the specific currency rules from the assignment guidelines
            switch (Office.CurrencyCode.ToUpper())
            {
                case "SEK":
                    // Example Output: "15,500.00 SEK" (Symbol goes after the value)
                    return $"{localPrice:N0} SEK";

                case "EUR":
                    // Example Output: "€1,250.00" (Symbol goes before the value)
                    return $"€{localPrice:N2}";

                case "TRY":
                    // Example Output: "₺45,000.00"
                    return $"₺{localPrice:N2}";

                case "USD":
                default:
                    // Default/USA Output: "$1,499.99"
                    return $"${localPrice:N2}";
            }
        }
    }

}