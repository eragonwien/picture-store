using System;

namespace PictureStore.Core.Models.AppSettings
{
    public class PictureStoreCleanupFilesAppSettings
    {
        public const string Section = "Cleanup";

        public TimeSpan Interval { get; set; }
    }
}