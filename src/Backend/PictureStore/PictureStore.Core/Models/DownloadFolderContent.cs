using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PictureStore.Core.Models
{
    public class DownloadFolderContent
    {
        public string Path { get; set; }

        public List<string> Files { get; set; } = new();

        public List<DownloadFolderContent> Directories { get; set; } = new();

        public DownloadFolderContent(string path)
        {
            Path = path;
            Files.AddRange(Directory.GetFiles(Path));
            Directories.AddRange(Directory.GetDirectories(Path).Select(d => new DownloadFolderContent(d)));
        }
    }
}