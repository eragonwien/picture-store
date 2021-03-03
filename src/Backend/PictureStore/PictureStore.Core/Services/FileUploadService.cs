using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PictureStore.Core.Services
{
    public class FileUploadService : IFileUploadService
    {
        public async Task UploadAsync(string rootPath, IFormFileCollection files)
        {
            var directoryPath = Path.Combine(rootPath, "temp");
            Directory.CreateDirectory(directoryPath);

            foreach (var file in files)
            {
                if (file.Length == 0) continue;

                var filePath = Path.Combine(directoryPath, file.FileName);

                await using Stream stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);
            }
        }
    }
}