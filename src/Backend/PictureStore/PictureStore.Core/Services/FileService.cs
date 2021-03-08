using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PictureStore.Core.Models;
using PictureStore.Core.Models.AppSettings;
using SixLabors.ImageSharp;

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

        public async Task<FileUploadPartialResult> UploadAsync(
            string inputFileName, 
            Stream stream,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Directory.CreateDirectory(uploadAppSettings.Directory);

            var result = new FileUploadPartialResult(inputFileName);

            try
            {
                var creationTime = DateTime.Now;
                var filename = $"{creationTime:yyyyMMddHHmmssfff}.jpeg";
                var filePath = Path.Combine(uploadAppSettings.Directory, filename);

                // Copies to a temporary stream
                await using var imageStream = new MemoryStream();
                await stream.CopyToAsync(imageStream, cancellationToken);

                // Converts file to jpeg image
                imageStream.Seek(0, SeekOrigin.Begin);
                var image = await Image.LoadAsync(imageStream);
                await image.SaveAsJpegAsync(imageStream, cancellationToken);

                // Saves file
                imageStream.Seek(0, SeekOrigin.Begin);
                await using var fileStream = File.Create(filePath);
                await imageStream.CopyToAsync(fileStream, cancellationToken);

                File.SetCreationTime(filePath, creationTime);
                File.SetLastWriteTime(filePath, creationTime);

                result.Succeed = true;
            }
            catch (Exception ex)
            {
                result.Error(ex);
            }

            return result;
        }
    }
}