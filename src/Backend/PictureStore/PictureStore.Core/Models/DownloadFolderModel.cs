using System.Collections.Generic;

namespace PictureStore.Core.Models
{
    public class DownloadFolderModel
    {
        public string Name { get; set; }

        public List<string> Files { get; set; } = new();
    }
}