using System;
using System.IO;
using System.Security.Cryptography;

namespace PictureStore.Core.Models
{
    public class FileCompareDetails
    {
        public string FileName { get; set; }

        public string FileHash { get; set; }

        public static FileCompareDetails ReadFile(string filename)
        {
            using var stream = new FileStream(filename, FileMode.Open, FileAccess.Read);

            return new FileCompareDetails
            {
                FileName = filename,
                FileHash = BitConverter.ToString(SHA1.Create().ComputeHash(stream))
            };
        }
    }
}
