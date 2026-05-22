using System;
using System.Collections.Generic;

namespace SmartAssetTracking.Entities
{
    public class Office
    {
        public int Id { get; set; }

        public string OfficeName { get; set; } = string.Empty; // E.g., "Stockholm Corporate Hub"

        public string Country { get; set; } = string.Empty;    // E.g., "Sweden"

        public string CurrencyCode { get; set; } = string.Empty; // E.g., "SEK", "USD", "EUR", "TRY"

        // 👈 2. FIXED TYPO: Changed "ExchangeRateToToUsd" to "ExchangeRateToUsd"
        // Fixed conversion rate helper (Value of 1 USD in this currency)
        // E.g., if CurrencyCode is SEK, this value might be 10.65
        public double ExchangeRateUsd { get; set; }

        // 🔗 One-to-Many Navigation Property: One office manages a group of assets
        public List<Asset> Assets { get; set; } = new List<Asset>();
    }
}