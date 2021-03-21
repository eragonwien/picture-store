using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PictureStore.Core.Models
{
   public class DownloadFolderContent
    {
        public string FolderPath { get; set; }

        public List<DownloadFileInfo> Files { get; set; } = new();

        public List<DownloadFolderContent> Directories { get; set; } = new();

        public DownloadFolderContent(string path)
        {
            FolderPath = path;
            Files.AddRange(Directory.GetFiles(FolderPath).Select(path => new DownloadFileInfo(path)));
            Directories.AddRange(Directory.GetDirectories(FolderPath).Select(d => new DownloadFolderContent(d)));
        }
    }
}