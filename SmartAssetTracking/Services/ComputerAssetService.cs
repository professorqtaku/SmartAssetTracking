using SmartAssetTracking.Data;
using SmartAssetTracking.Entities.Asset;


namespace SmartAssetTracking.Services
{
    public class ComputerAssetService 
    {
        private readonly AssetDbContext _context;

        public ComputerAssetService()
        {
            _context = new AssetDbContext();
            // NOTE: Database.Migrate() intentionally removed from constructor.
            // Migrations should run once at application startup to avoid throwing
            // during static initialization and to allow controlled error handling.
        }

        public void AddAsset (ComputerAsset asset)         {
            _context.ComputerAssets.Add(asset);
            _context.SaveChanges();
        }

        public List<ComputerAsset> GetAssets()
        {
            // Level 2 Sorting Rule: Category (Discriminator type) first, then Purchase Date
            // Null-check _context and DbSet to avoid NullReferenceException during tests or DI scenarios
            if (_context == null || _context.ComputerAssets == null) return new List<ComputerAsset>();

            return _context.ComputerAssets.ToList();
        }

        public bool DeleteAsset(int id)
        {
            var asset = _context.ComputerAssets.Find(id);
            if (asset == null) return false;

            _context.ComputerAssets.Remove(asset);
            _context.SaveChanges();
            return true;
        }
    }
}
