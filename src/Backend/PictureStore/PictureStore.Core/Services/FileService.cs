using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PictureStore.Core.Models;
using PictureStore.Core.Models.AppSettings;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

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
                await using var imageStream = await LoadImageStreamAsync(stream, cancellationToken);
                await SaveImageAsync(imageStream, cancellationToken);

                result.Succeed = true;
            }
            catch (Exception ex)
            {
                result.Error(ex);
            }

            return result;
        }

        private static async Task<Stream> LoadImageStreamAsync(Stream fileStream, CancellationToken cancellationToken)
        {
            var imageStream = new MemoryStream();
            var encoder = new JpegEncoder {Quality = 90};

            fileStream.Seek(0, SeekOrigin.Begin);
            await fileStream.CopyToAsync(imageStream, cancellationToken);

            imageStream.Seek(0, SeekOrigin.Begin);
            var image = await Image.LoadAsync(imageStream);
            await image.SaveAsJpegAsync(imageStream, encoder, cancellationToken);

            return imageStream;
        }

        private async Task SaveImageAsync(Stream imageStream, CancellationToken cancellationToken)
        {
            var creationTime = DateTime.Now;
            var filename = $"{creationTime:yyyyMMddHHmmssfff}.jpeg";
            var filePath = Path.Combine(uploadAppSettings.Directory, filename);

            imageStream.Seek(0, SeekOrigin.Begin);

            await using var fileStream = File.Create(filePath);
            await imageStream.CopyToAsync(fileStream, cancellationToken);

            File.SetCreationTime(filePath, creationTime);
            File.SetLastWriteTime(filePath, creationTime);
        }
    }
}