using System.IO;

namespace PictureStore.Core.Models
{
    public class FileUploadModel
    {
        public FileUploadModel(string name, Stream stream)
        {
            Name = name;
            Stream = stream;
        }

        public string Name { get; set; }

        public Stream Stream { get; set; }
    }
}
