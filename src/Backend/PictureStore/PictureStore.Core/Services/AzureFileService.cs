using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using PictureStore.Core.Models;
using PictureStore.Core.Models.AppSettings;

namespace PictureStore.Core.Services
{
    public class AzureFileService : IFileService
    {
        private readonly IOptionsSnapshot<PictureStoreAzureAppSettings> azureAppSettingsOptions;

        private const string DefaultContainerName = "images";

        public AzureFileService(IOptionsSnapshot<PictureStoreAzureAppSettings> azureAppSettingsOptions)
        {
            this.azureAppSettingsOptions = azureAppSettingsOptions ?? throw new ArgumentNullException(nameof(azureAppSettingsOptions));
        }

        public async Task UploadAsync(string filename, Stream stream, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task UploadAsync(Stream stream, CancellationToken cancellationToken)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            cancellationToken.ThrowIfCancellationRequested();

            var client = await CreateBlobClientAsync(
                DefaultContainerName,
                Path.Combine(DateTime.UtcNow.ToString("yyyyMMdd"), $"{Guid.NewGuid():N}.jpeg"));

            await client.UploadAsync(stream, cancellationToken);
        }

        public async Task<DownloadFileModel> DownloadAsync(string folder, string filename, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<byte[]> DownloadFileAsync(string folder, string filename, CancellationToken cancellationToken)
        {
            if (folder == null) throw new ArgumentNullException(nameof(folder));
            if (filename == null) throw new ArgumentNullException(nameof(filename));
            
            cancellationToken.ThrowIfCancellationRequested();

            var client = await CreateBlobClientAsync(DefaultContainerName, BuildDownloadPath(folder, filename));

            await using var downloadStream = new MemoryStream();
            await client.DownloadToAsync(downloadStream, cancellationToken);

            return downloadStream.ToArray();
        }

        public async Task TransferFileToDownloadFolderAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DuplicateFileModel>> ListDuplicatesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string[]> PageFiles()
        {
            throw new NotImplementedException();
        }

        public async Task DeleteFileAsync(string folder, string filename, CancellationToken cancellationToken)
        {
            if (folder == null) throw new ArgumentNullException(nameof(folder));
            if (filename == null) throw new ArgumentNullException(nameof(filename));
            
            cancellationToken.ThrowIfCancellationRequested();

            var client = await CreateBlobClientAsync(DefaultContainerName, BuildDownloadPath(folder, filename));

            await client.DeleteIfExistsAsync(cancellationToken: cancellationToken);
        }

        public async Task CleanupFilesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task PrepareContainersAsync(CancellationToken cancellationToken)
        {
            await PrepareContainerAsync(DefaultContainerName, cancellationToken);
        }

        public async Task<List<string>> ListDirectoriesAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var directories = new List<string>();

            var client = await CreateContainerClientAsync(DefaultContainerName);
            var resultSegment = client.GetBlobsByHierarchyAsync(delimiter: "/").AsPages();

            await foreach (var page in resultSegment.WithCancellation(cancellationToken))
                directories.AddRange(page.Values.Where(p => p.IsPrefix).Select(p => Path.GetDirectoryName(p.Prefix)));

            return directories;
        }

        public async Task<List<string>> ListFilePathsOfDirectoryAsync(string directory, CancellationToken cancellationToken)
        {
            if (directory == null) throw new ArgumentNullException(nameof(directory));

            cancellationToken.ThrowIfCancellationRequested();

            var items = await ListBlobItemsOfDirectoryAsync(directory, cancellationToken);

            return items.Select(item => item.Name).ToList();
        }

        private async Task<List<BlobItem>> ListBlobItemsOfDirectoryAsync(string directory, CancellationToken cancellationToken)
        {
            if (directory == null) throw new ArgumentNullException(nameof(directory));

            cancellationToken.ThrowIfCancellationRequested();

            var items = new List<BlobItem>();

            var client = await CreateContainerClientAsync(DefaultContainerName);
            var resultSegment = client.GetBlobsByHierarchyAsync(prefix: $"{directory}/", delimiter: "/").AsPages();

            await foreach (var page in resultSegment.WithCancellation(cancellationToken))
                items.AddRange(page.Values.Where(p => p.IsBlob).Select(p => p.Blob));

            return items;
        }

        private async Task<BlobContainerClient> CreateContainerClientAsync(string containerName)
        {
            if (containerName == null) throw new ArgumentNullException(nameof(containerName));

            var serviceClient = new BlobServiceClient(azureAppSettingsOptions.Value.Storage.ConnectionString);

            return serviceClient.GetBlobContainerClient(containerName);
        }

        private async Task<BlobClient> CreateBlobClientAsync(string containerName, string blobName)
        {
            if (containerName == null) throw new ArgumentNullException(nameof(containerName));
            if (blobName == null) throw new ArgumentNullException(nameof(blobName));

            var containerClient = await CreateContainerClientAsync(containerName);

            return containerClient.GetBlobClient(blobName);
        }

        private async Task PrepareContainerAsync(string containerName, CancellationToken cancellationToken)
        {
            if (containerName == null) throw new ArgumentNullException(nameof(containerName));

            var containerClient = await CreateContainerClientAsync(containerName);

            await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);
        }

        private static string BuildDownloadPath(string directory, string filename) => Path.Combine(directory, Path.ChangeExtension(filename, "jpeg"));
    }
}
