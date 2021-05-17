namespace PictureStore.Core.Models
{
    public class AzureUploadFileModel
    {
        public string Container { get; set; }
        public string Folder { get; set; }
        public string SubFolder { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileType { get; set; }
    }
}
