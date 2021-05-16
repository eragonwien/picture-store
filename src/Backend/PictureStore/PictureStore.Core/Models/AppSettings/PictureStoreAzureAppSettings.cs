namespace PictureStore.Core.Models.AppSettings
{
    public class PictureStoreAzureAppSettings
    {
        public const string Key = "Azure";

        public PictureStoreAzureStorageAppSettings Storage { get; set; }
    }
}
