using SmartAssetTracking.Data;


namespace SmartAssetTracking.Services
{
    public interface IAssetService
    {

        void AddAsset();
        void RemoveAsset(string assetId);
    }
}
