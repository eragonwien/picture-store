namespace PictureStore.Core.Models.AppSettings
{
    public class PictureStoreAzureStorageAppSettings
    {
        public const string Section = "Storage";

        public string ConnectionString { get; set; }
    }
}