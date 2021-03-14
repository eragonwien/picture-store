using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PictureStore.Core.Models
{
    public class FileListingModel
    {
        public string Path { get; set; }

        public List<DownloadFolderContent> Directories { get; set; } = new();

        public FileListingModel(string path)
        {
            Path = path;
        }

        public FileListingModel ListFiles(string folder)
        {
            folder ??= string.Empty;
            Directories.AddRange(Directory.GetDirectories(Path, folder, SearchOption.TopDirectoryOnly).Select(d => new DownloadFolderContent(d)));

            return this;
        }
    }
}