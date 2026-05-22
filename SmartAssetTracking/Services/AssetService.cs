using SmartAssetTracking.Data;
using SmartAssetTracking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartAssetTracking.Services
{
    public class AssetService
    {
        public void AddAsset(Asset newAsset)
        {
            using var context = new AssetDbContext();
            context.Assets.Add(newAsset);
            context.SaveChanges();
        }

        public List<Asset> GetAllAssetsSorted()
        {
            using var context = new AssetDbContext();

            if (context.Assets == null) return new List<Asset>();

            return context.Assets
                .AsEnumerable()
                .OrderBy(a => a.GetType().Name) // Level 2 Sorting: Category (Computer vs Mobile)
                .ThenBy(a => a.PurchaseDate)    // Then sorts by date
                .ToList();
        }

        public Asset? GetAssetByIdAndType(int id, string type)
        {
            using var context = new AssetDbContext();

            if (type.Equals("Computer", StringComparison.OrdinalIgnoreCase))
            {
                return context.ComputerAssets?.Find(id);
            }

            return context.MobileAssets?.Find(id);
        }

        public void UpdateAssetSafely(Asset updatedAsset)
        {
            using var context = new AssetDbContext();

            if (updatedAsset is ComputerAsset computer)
            {
                var existingComputer = context.ComputerAssets.Find(computer.Id);
                if (existingComputer != null)
                {
                    context.Entry(existingComputer).CurrentValues.SetValues(computer);
                    existingComputer.FormFactor = computer.FormFactor; // Map type-specific properties
                }
            }
            else if (updatedAsset is MobileAsset mobile)
            {
                var existingMobile = context.MobileAssets?.Find(mobile.Id);
                if (existingMobile != null)
                {
                    context.Entry(existingMobile).CurrentValues.SetValues(mobile);
                    existingMobile.DeviceType = mobile.DeviceType; // Map type-specific properties
                }
            }

            context.SaveChanges();
        }

        public bool DeleteAsset(int id, string type)
        {
            using var context = new AssetDbContext();

            if (type.Equals("Computer", StringComparison.OrdinalIgnoreCase))
            {
                var asset = context.ComputerAssets.Find(id);
                if (asset == null) return false;

                context.ComputerAssets.Remove(asset);
                context.SaveChanges();
                return true;
            }
            else
            {
                var asset = context.MobileAssets.Find(id);
                if (asset == null) return false;

                context.MobileAssets.Remove(asset);
                context.SaveChanges();
                return true;
            }
        }
    }
}