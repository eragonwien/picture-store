using Microsoft.Extensions.Options;
using PictureStore.Core.Models;
using PictureStore.Core.Models.AppSettings;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PictureStore.Core.Services
{
    public class BlobFileService : IFileService
    {
        private readonly PictureStoreAzureAppSettings azureAppSettings;

        public BlobFileService(IOptions<PictureStoreAzureAppSettings> azureAppSettingsOptions)
        {
            this.azureAppSettings = azureAppSettingsOptions.Value;
        }

        public Task CleanupFilesAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteFileAsync(string folder, string filename, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<DownloadFileModel> DownloadAsync(string folder, string filename, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<DuplicateFileModel>> ListDuplicatesAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, string[]> PageFiles()
        {
            throw new System.NotImplementedException();
        }

        public Task TransferFileToDownloadFolderAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task UploadAsync(string inputFileName, Stream stream, CancellationToken cancellationToken)
        {
            
        }
    }
}
