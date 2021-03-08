using System;

namespace PictureStore.Core.Models
{
    public class FileUploadPartialResult
    {
        public FileUploadPartialResult(string fileName)
        {
            FileName = fileName;
        }

        public bool Succeed { get; set; }

        public string FileName { get; set; }

        public string Message { get; set; }

        public void Error(Exception exception)
        {
            Succeed = false;
            Message = exception?.Message;
        }
    }
}