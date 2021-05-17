namespace PictureStore.Core.Models.AppSettings
{
    public class PictureStoreAzureAppSettings
    {
        public const string Section = "Azure";

        public PictureStoreAzureStorageAppSettings Storage { get; set; }
    }
}