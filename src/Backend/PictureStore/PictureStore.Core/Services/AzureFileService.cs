using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
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

            var client = await CreateContainerClientAsync(DefaultContainerName);
            var blobName = CreateUploadBlobName();

            await client.UploadBlobAsync(blobName, stream, cancellationToken);
        }

        public async Task<DownloadFileModel> DownloadAsync(string folder, string filename, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public async Task CleanupFilesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task PrepareContainersAsync(CancellationToken cancellationToken)
        {
            await PrepareContainerAsync(DefaultContainerName, cancellationToken);
        }

        private async Task<BlobContainerClient> CreateContainerClientAsync(string containerName)
        {
            if (containerName == null) throw new ArgumentNullException(nameof(containerName));

            var serviceClient = new BlobServiceClient(azureAppSettingsOptions.Value.Storage.ConnectionString);

            return serviceClient.GetBlobContainerClient(containerName);
        }

        private async Task PrepareContainerAsync(string containerName, CancellationToken cancellationToken)
        {
            if (containerName == null) throw new ArgumentNullException(nameof(containerName));

            var containerClient = await CreateContainerClientAsync(containerName);

            await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);
        }

        private static string CreateUploadBlobName() =>
            Path.Combine(DateTime.UtcNow.ToString("yyyyMMdd"), $"{Guid.NewGuid():N}.jpeg");
    }
}
