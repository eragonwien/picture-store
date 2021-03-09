namespace PictureStore.Core.Models
{
    public class DownloadFileModel
    {
        public string ContentType { get; set; }

        public byte[] Content { get; set; }

        public bool Succeed { get; set; }

        public string Message { get; set; }  
    }
}