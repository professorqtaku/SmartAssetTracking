using SmartAssetTracking.Data;
using SmartAssetTracking.Entities.Asset;
using System.Collections.Generic;
using System.Linq;

namespace SmartAssetTracking.Services
{
    public class AssetService
    {
        // 📥 SAVE: Pass in a Computer OR a Mobile. 
        // EF Core looks at the type and routes it to the correct table automatically!
        public void AddAsset(Asset newAsset)
        {
            using (var context = new AssetDbContext())
            {
                context.Assets.Add(newAsset);
                context.SaveChanges();
            }
        }

        // 📤 READ: EF Core automatically runs a SQL 'UNION ALL' behind the scenes 
        // to combine your two separate tables into one list for your UI.
        public List<Asset> GetAllAssetsSorted()
        {
            using (var context = new AssetDbContext())
            {
                return context.Assets
                    .AsEnumerable()
                    .OrderBy(a => a.GetType().Name) // Sorts by Category (Computer vs Mobile)
                    .ThenBy(a => a.PurchaseDate)    // Then sorts by date
                    .ToList();
            }
        }

        // 1. Safe Find: Look into specific tables rather than the unified tracker
        public Asset GetAssetByIdAndType(int id, string type)
        {
            using (var context = new AssetDbContext())
            {
                if (type.Equals("Computer", StringComparison.OrdinalIgnoreCase))
                {
                    return context.ComputerAssets.Find(id); // Only looks in ComputerAssets table
                }

                return context.MobileAssets.Find(id); // Only looks in MobileAssets table
            }
        }

        // 2. Safe Update: Target the precise entity set directly
        public void UpdateAssetSafely(Asset updatedAsset)
        {
            using (var context = new AssetDbContext())
            {
                if (updatedAsset is ComputerAsset computer)
                {
                    context.ComputerAssets.Update(computer);
                }
                else if (updatedAsset is MobileAsset mobile)
                {
                    context.MobileAssets.Update(mobile);
                }

                context.SaveChanges();
            }
        }

        //// ❌ DELETE: Finds the asset by ID across either table and deletes it
        //public bool DeleteAsset(int id)
        //{
        //    using (var context = new AssetDbContext())
        //    {
        //        var asset = context.Assets.Find(id);
        //        if (asset == null) return false;

        //        context.Assets.Remove(asset);
        //        context.SaveChanges();
        //        return true;
        //    }
        //}
    }
}