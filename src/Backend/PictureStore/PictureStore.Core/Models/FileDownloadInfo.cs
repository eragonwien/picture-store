using System.IO;

namespace PictureStore.Core.Models
{
    public class FileDownloadInfo
    {
        public string Folder { get; set; }

        public string FileName { get; set; }

        public FileDownloadInfo(string path)
        {
            FileName = Path.GetFileNameWithoutExtension(path);
            Folder = Path.GetFileName(Path.GetDirectoryName(path));
        }
    }
}