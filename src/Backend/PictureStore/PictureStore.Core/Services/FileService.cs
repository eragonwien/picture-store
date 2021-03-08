using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
        private readonly PictureStoreDownloadAppSettings downloadAppSettings;

        private const string fileLongDateFormat = "yyyyMMddHHmmssfff";
        private const string fileShortDateFormat = "yyyyMMdd";

        public FileService(IOptions<PictureStoreUploadAppSettings> uploadAppSettingsOptions,
            IOptions<PictureStoreDownloadAppSettings> downloadAppSettingsOptions)
        {
            this.downloadAppSettings = downloadAppSettingsOptions.Value;
            uploadAppSettings = uploadAppSettingsOptions.Value;
        }

        public Task DownloadAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task MoveToDownloadFolderAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var uploadFilePaths = Directory.GetFiles(uploadAppSettings.Directory);

            var errors = new List<MovingFileError>();

            foreach (var sourceFilePath in uploadFilePaths)
            {
                string destinationFilePath = null;
                try
                {
                    var creationTime = File.GetCreationTime(sourceFilePath);
                    var destinationDirectory = Path.Combine(downloadAppSettings.Directory, creationTime.ToString(fileShortDateFormat));
                    destinationFilePath = sourceFilePath.Replace(uploadAppSettings.Directory, destinationDirectory);

                    Directory.CreateDirectory(destinationDirectory);
                    File.Move(sourceFilePath, destinationFilePath);
                }
                catch (Exception ex)
                {
                    errors.Add(new MovingFileError(sourceFilePath, destinationFilePath, ex.Message));
                }

            }

            if (errors.Any())
                throw new MoveToDownloadFolderException(errors);
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

        public async Task CleanupAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var duplicates = GetFiles(downloadAppSettings.Directory)
                .Select(FileCompareDetails.ReadFile)
                .GroupBy(f => f.FileHash)
                .Select(g => new { FileHash = g.Key, Files = g.Select(x => x.FileName).OrderBy(x => x).ToList() })
                .SelectMany(f => f.Files.Skip(1))
                .ToList();

            duplicates.ForEach(File.Delete);
        }

        private ICollection<string> GetFiles(string directory)
        {
            var files = new List<string>();

            files.AddRange(Directory.GetFiles(directory));

            foreach (var subDir in Directory.GetDirectories(directory))
                files.AddRange(GetFiles(subDir));

            return files;
        }

        private static async Task<Stream> LoadImageStreamAsync(Stream fileStream, CancellationToken cancellationToken)
        {
            var imageStream = new MemoryStream();
            var encoder = new JpegEncoder { Quality = 90 };

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
            var filename = $"{creationTime.ToString(fileLongDateFormat)}.jpeg";
            var filePath = Path.Combine(uploadAppSettings.Directory, filename);

            imageStream.Seek(0, SeekOrigin.Begin);

            await using var fileStream = File.Create(filePath);
            await imageStream.CopyToAsync(fileStream, cancellationToken);

            File.SetCreationTime(filePath, creationTime);
            File.SetLastWriteTime(filePath, creationTime);
        }
    }
}