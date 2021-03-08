namespace PictureStore.Core.Models
{
    public class FileUploadError
    {
        public string FileName { get; set; }

        public string Message { get; set; }

        public FileUploadError(string fileName, string message)
        {
            FileName = fileName;
            Message = message;
        }
    }
}