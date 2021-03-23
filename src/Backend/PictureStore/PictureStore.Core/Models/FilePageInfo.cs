using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PictureStore.Core.Models
{
    public class FilePageInfo
    {
        public List<FileDownloadInfo> DownloadInfos { get; set; } = new();

        public FilePageInfo(string path)
        {
            if (!Directory.Exists(path)) return;

            DownloadInfos.AddRange(Directory.GetFiles(path).Select(f => new FileDownloadInfo(f)));
        }
    }
}