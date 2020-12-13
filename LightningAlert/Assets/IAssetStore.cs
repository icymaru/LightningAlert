using LightningAlert.Dtos;

namespace LightningAlert.Assets
{
    // This is not needed as of the moment, but it might be good to make this ready
    // in the event that we put the assets in different storage (e.g. database)
    public interface IAssetStore
    {
        AssetDto Get(double latitude, double longitude, int levelOfDetail);
    }
}
