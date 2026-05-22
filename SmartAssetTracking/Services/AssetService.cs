using Microsoft.EntityFrameworkCore;
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

        public Asset? GetAssetById(int id)
        {
            using var context = new AssetDbContext();
            return context.Assets.Find(id);
        }

        public void UpdateAssetSafely(Asset updatedAsset)
        {
            using (var context = new AssetDbContext())
            {
                if (updatedAsset is ComputerAsset computer)
                {
                    var existingComputer = context.ComputerAssets?.Find(computer.Id);
                    if (existingComputer != null)
                    {
                        context.Entry(existingComputer).CurrentValues.SetValues(computer);
                        existingComputer.FormFactor = computer.FormFactor;
                        existingComputer.OfficeId = computer.OfficeId; // 👈 Make sure the Foreign Key copies over!
                    }
                }
                else if (updatedAsset is MobileAsset mobile)
                {
                    var existingMobile = context.MobileAssets?.Find(mobile.Id);
                    if (existingMobile != null)
                    {
                        context.Entry(existingMobile).CurrentValues.SetValues(mobile);
                        existingMobile.DeviceType = mobile.DeviceType;
                        existingMobile.OfficeId = mobile.OfficeId; // 👈 Make sure the Foreign Key copies over!
                    }
                }

                context.SaveChanges();
            }
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
        public List<Asset> SearchAssets(string criteria, string query)
        {
            using var context = new AssetDbContext();
            IQueryable<Asset> nodes = context.Assets.Include(a => a.Office);

            if (string.IsNullOrWhiteSpace(query)) return nodes.ToList();
            query = query.Trim().ToLower();

            return criteria.ToLower() switch
            {
                "brand" => nodes.Where(a => a.Brand.ToLower().Contains(query)).ToList(),
                "model" => nodes.Where(a => a.ModelName.ToLower().Contains(query)).ToList(),
                "office" => nodes.Where(a => a.Office != null && a.Office.OfficeName.ToLower().Contains(query)).ToList(),
                "year" => int.TryParse(query, out int year) ? nodes.Where(a => a.PurchaseDate.Year == year).ToList() : new List<Asset>(),
                _ => nodes.ToList()
            };
        }
        public List<Asset> FilterAssets(string filterType, string argument = "")
        {
            using var context = new AssetDbContext();
            IQueryable<Asset> nodes = context.Assets.Include(a => a.Office);

            return filterType.ToLower() switch
            {
                "expired" => nodes.Where(a => a.PurchaseDate <= DateTime.Now.AddYears(-3)).ToList(),
                "computers" => nodes.Where(a => a is ComputerAsset).ToList(),
                "mobiles" => nodes.Where(a => a is MobileAsset).ToList(),
                "office" => nodes.Where(a => a.Office != null && a.Office.OfficeName.ToLower().Contains(argument.ToLower())).ToList(),
                _ => nodes.ToList()
            };
        }
    }
}