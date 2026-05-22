using SmartAssetTracking.Data;
using SmartAssetTracking.Entities.Asset;


namespace SmartAssetTracking.Services
{
    public class MobileAssetService
    {
        private readonly AssetDbContext _context;

        public MobileAssetService()
        {
            _context = new AssetDbContext();
            // NOTE: Database.Migrate() intentionally removed from constructor.
            // Migrations should run once at application startup to avoid throwing
            // during static initialization and to allow controlled error handling.
        }

        public void AddAsset(MobileAsset asset)
        {
            _context.MobileAssets.Add(asset);
            _context.SaveChanges();
        }

        public List<MobileAsset> GetAssets()
        {
            // Level 2 Sorting Rule: Category (Discriminator type) first, then Purchase Date
            // Null-check _context and DbSet to avoid NullReferenceException during tests or DI scenarios
            if (_context == null || _context.MobileAssets == null) return new List<MobileAsset>();

            return _context.MobileAssets.OrderBy(a => a.AssetType).ThenBy(a => a.PurchaseDate).ToList();
        }

        public bool DeleteAsset(int id)
        {
            var asset = _context.MobileAssets.Find(id);
            if (asset == null) return false;

            _context.MobileAssets.Remove(asset);
            _context.SaveChanges();
            return true;
        }
    }
}
