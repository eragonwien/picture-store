using System;
using System.IO;

namespace PictureStore.Core.Models
{
    public class DownloadFileInfo
    {
        public byte[] Content { get; set; }

        public string ContentType { get; set; }

        public DownloadFileInfo(byte[] content, string contentType)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
            ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
        }
    }
}