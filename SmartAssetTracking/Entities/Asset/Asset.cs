using System;
using System.Collections.Generic;

namespace SmartAssetTracking.Entities.Asset
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

        public decimal GetLocalPrice(decimal exchangeRate)
        {
            return PurchasePriceUSD * exchangeRate;
        }
    }

}