using System.Collections.Generic;
using System.IO;

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
            foreach (var dirPath in Directory.GetDirectories(Path))
            {
                if (!string.IsNullOrWhiteSpace(folder) && dirPath != folder)
                    continue;

                Directories.Add(new DownloadFolderContent(dirPath));
            }

            return this;
        }
    }
}