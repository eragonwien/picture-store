using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PictureStore.Core.Models.AppSettings;

namespace PictureStore.Core.Services
{
    public class FileService : IFileService
    {
        private readonly PictureStoreUploadAppSettings uploadAppSettings;

        public FileService(IOptions<PictureStoreUploadAppSettings> uploadAppSettingsOptions)
        {
            uploadAppSettings = uploadAppSettingsOptions.Value;
        }

        public Task DownloadAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task ListAsync(int page)
        {
            throw new NotImplementedException();
        }

        public async Task UploadAsync(Stream stream, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Directory.CreateDirectory(uploadAppSettings.Directory);

            // TODO: converts file to jpg file

            var creationTime = DateTime.Now;
            var filename = $"{creationTime:yyyyMMddHHmmssfff}.jpg";
            var filePath = Path.Combine(uploadAppSettings.Directory, filename);

            await using var fileStream = File.Create(filePath);
            stream.Seek(0, SeekOrigin.Begin);

            await stream.CopyToAsync(fileStream, cancellationToken);
            File.SetCreationTime(filePath, creationTime);
            File.SetLastWriteTime(filePath, creationTime);
        }
    }
}