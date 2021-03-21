using PictureStore.Core.Extensions;
using System.IO;
using MimeMapping;

namespace PictureStore.Core.Models
{
   public class DownloadFileInfo
   {
      public string FilePath { get; set; }

      public string Folder { get; set; }

      public string FileName { get; set; }

      public string FileNameWithoutExtension { get; set; }

      public string MimeType { get; set; }

      public DownloadFileInfo(string path)
      {
         FilePath = path;
         FileName = Path.GetFileName(path);
         FileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
         MimeType = MimeUtility.GetMimeMapping(FileName);
         Folder = path.GetParentDirectory();
      }
   }
}