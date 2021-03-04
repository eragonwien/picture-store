using System.IO;

namespace PictureStore.Core.Models
{
    public class FileModel
    {
        public FileModel(string name, Stream stream)
        {
            Name = name;
            Stream = stream;
        }

        public string Name { get; set; }
        public Stream Stream { get; set; }
    }
}
