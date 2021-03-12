using System;

namespace PictureStore.Core.Models.AppSettings
{
    public class PictureStoreFileTransferAppSettings
    {
        public const string Section = "FileTransfer";

        public TimeSpan Interval { get; set; }
    }
}