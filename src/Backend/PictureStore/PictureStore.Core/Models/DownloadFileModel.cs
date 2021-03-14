using PictureStore.Core.Exceptions;
using System;

namespace PictureStore.Core.Models
{
    public class DownloadFileModel
    {
        public string ContentType { get; set; }

        public byte[] Content { get; set; }
    }
}