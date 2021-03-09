using System.Collections.Generic;
using System.IO;

namespace PictureStore.Core.Models
{
    public class FileListingModel
    {
        public string RootDirectory { get; set; }

        public List<DownloadFolderContent> Directories { get; set; } = new();

        public FileListingModel(string rootDirectory)
        {
            RootDirectory = rootDirectory;
        }

        public FileListingModel ListFiles(string folder)
        {
            foreach (var dirPath in Directory.GetDirectories(RootDirectory))
            {
                if (!string.IsNullOrWhiteSpace(folder) && dirPath != folder)
                    continue;

                Directories.Add(new DownloadFolderContent(dirPath));
            }

            return this;
        }
    }
}