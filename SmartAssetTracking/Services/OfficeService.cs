using Microsoft.EntityFrameworkCore;
using SmartAssetTracking.Data;
using SmartAssetTracking.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAssetTracking.Services
{
    internal class OfficeService
    {
        /// <summary>
        /// Fetches all offices along with their associated hardware assets eagerly loaded.
        /// </summary>
        public List<Office> GetOfficesWithAssets()
        {
            using var context = new AssetDbContext();

            if (context.Offices == null) return new List<Office>();

            return context.Offices
                .Include(o => o.Assets)
                .OrderBy(o => o.OfficeName)
                .ToList();
        }
    }
}
