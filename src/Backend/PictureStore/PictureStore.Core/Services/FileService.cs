using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MimeMapping;
using PictureStore.Core.Exceptions;
using PictureStore.Core.Models;
using PictureStore.Core.Models.AppSettings;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace PictureStore.Core.Services
{
    public class FileService : IFileService
    {
        private readonly PictureStoreUploadAppSettings uploadAppSettings;
        private readonly PictureStoreDownloadAppSettings downloadAppSettings;

        private const string fileLongDateFormat = "yyyyMMddHHmmssfff";
        private const string fileShortDateFormat = "yyyyMMdd";
        private const string thumbnailFolderName = "thumbnails";

        public FileService(IOptions<PictureStoreUploadAppSettings> uploadAppSettingsOptions,
            IOptions<PictureStoreDownloadAppSettings> downloadAppSettingsOptions)
        {
            downloadAppSettings = downloadAppSettingsOptions.Value;
            uploadAppSettings = uploadAppSettingsOptions.Value;
        }

        public async Task<DownloadFileModel> DownloadAsync(
            string folder,
            string filename,
            CancellationToken cancellationToken)
        {
            var result = new DownloadFileModel();

            if (cancellationToken.IsCancellationRequested)
                return result;

            var path = GetFilePath(folder, filename);

            result.Content = await File.ReadAllBytesAsync(path, cancellationToken);
            result.ContentType = MimeUtility.GetMimeMapping(path);

            return result;
        }

        public async Task TransferFileToDownloadFolderAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var uploadFilePaths = Directory.GetFiles(uploadAppSettings.Directory);

            foreach (var sourceFilePath in uploadFilePaths)
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    // Moves files from upload folder to download folder
                    var creationTime = File.GetCreationTime(sourceFilePath);
                    var destinationDirectory = Path.Combine(downloadAppSettings.Directory, creationTime.ToString(fileShortDateFormat));
                    var destinationFilePath = sourceFilePath.Replace(uploadAppSettings.Directory, destinationDirectory);

                    Directory.CreateDirectory(destinationDirectory);
                    File.Move(sourceFilePath, destinationFilePath);

                    // Adds thumbnail 
                    var thumbnailDirectory = Path.Combine(downloadAppSettings.Directory, thumbnailFolderName, creationTime.ToString(fileShortDateFormat));
                    var thumbnailFilePath = destinationFilePath.Replace(destinationDirectory, thumbnailDirectory);

                    Directory.CreateDirectory(thumbnailDirectory);
                    await CreateThumbnailImageAsync(destinationFilePath, thumbnailFilePath, cancellationToken);
                }
                catch (Exception ex)
                {
                    throw new MoveToDownloadFolderException(ex);
                }
            }
        }

        public async Task UploadAsync(
            string inputFileName,
            Stream stream,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await using var imageStream = await LoadImageStreamAsync(stream, cancellationToken);
            await SaveImageAsync(imageStream, cancellationToken);
        }

        public async Task<IEnumerable<DuplicateFileModel>> ListDuplicatesAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return GetFiles(downloadAppSettings.Directory)
               .Select(FileDetails.ReadFile)
               .GroupBy(f => f.FileHash)
               .Select(g => new DuplicateFileModel(g.Key, g.OrderBy(x => x.FileName).Select(x => x.FileName)))
               .Where(m => m.HasDuplicate)
               .ToList();
        }

        public Dictionary<string, string[]> PageFiles()
        {
            var directories = Directory.GetDirectories(downloadAppSettings.Directory)
                .Where(dir => Path.GetFileName(dir) != thumbnailFolderName)
                .ToArray();

            return directories
                .SelectMany(dir => Directory.GetFiles(dir))
                .Select(path => new
                {
                    Folder = Path.GetFileName(Path.GetDirectoryName(path)),
                    FileName = Path.GetFileNameWithoutExtension(path)
                })
                .GroupBy(x => x.Folder)
                .ToDictionary(g => g.Key, g => g.Select(x => x.FileName).ToArray());
        }

        public async Task DeleteFileAsync(string folder, string filename, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return;

            var path = GetFilePath(folder, filename);

            if (!File.Exists(path)) return;

            File.Delete(path);
        }

        public async Task CleanupFilesAsync(CancellationToken cancellationToken)
        {
            var duplicates = await ListDuplicatesAsync(cancellationToken);

            Parallel.ForEach(duplicates, (duplicate) =>
            {
                foreach (var file in duplicate.Files.Skip(1))
                {
                    if (!File.Exists(file)) return;

                    File.Delete(file);
                }
            });
        }

        private static IEnumerable<string> GetFiles(string directory)
        {
            var files = new List<string>();

            files.AddRange(Directory.GetFiles(directory));

            foreach (var subDir in Directory.GetDirectories(directory))
                files.AddRange(GetFiles(subDir));

            return files;
        }

        private static async Task<Stream> LoadImageStreamAsync(Stream fileStream, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

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
            cancellationToken.ThrowIfCancellationRequested();

            var creationTime = DateTime.Now;
            var filename = $"{creationTime.ToString(fileLongDateFormat)}.jpeg";
            var filePath = Path.Combine(uploadAppSettings.Directory, filename);

            imageStream.Seek(0, SeekOrigin.Begin);

            await using var fileStream = File.Create(filePath);
            await imageStream.CopyToAsync(fileStream, cancellationToken);

            File.SetCreationTime(filePath, creationTime);
            File.SetLastWriteTime(filePath, creationTime);
        }

        private async Task CreateThumbnailImageAsync(string filePath, string thumbnailFilePath, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var thumbnail = await Image.LoadAsync(filePath, cancellationToken);
            thumbnail.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(250, 250),
                Mode = ResizeMode.Crop
            }));

            await thumbnail.SaveAsJpegAsync(thumbnailFilePath, cancellationToken);
        }

        private string GetFilePath(string folder, string filename)
        {
            filename = Path.ChangeExtension(filename, "jpeg");
            var path = Path.Combine(downloadAppSettings.Directory, folder, filename);

            if (!File.Exists(path))
                throw new FileNotFoundException($"File {filename} not found at {path}");

            return path;
        }
    }
}