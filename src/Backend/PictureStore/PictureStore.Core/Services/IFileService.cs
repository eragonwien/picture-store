using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using PictureStore.Core.Models;

namespace PictureStore.Core.Services
{
    public interface IFileService
    {
        Task UploadAsync(string filename, Stream stream, CancellationToken cancellationToken);

        Task UploadAsync(Stream stream, CancellationToken cancellationToken);

        Task<DownloadFileModel> DownloadAsync(string folder, string filename, CancellationToken cancellationToken);

        Task<byte[]> DownloadFileAsync(string folder, string filename, CancellationToken cancellationToken);

        Task TransferFileToDownloadFolderAsync(CancellationToken cancellationToken);

        Task<IEnumerable<DuplicateFileModel>> ListDuplicatesAsync(CancellationToken cancellationToken);

        Dictionary<string, string[]> PageFiles();

        Task DeleteFileAsync(string folder, string filename, CancellationToken cancellationToken);

        Task CleanupFilesAsync(CancellationToken cancellationToken);

        Task PrepareContainersAsync(CancellationToken cancellationToken);

        Task<List<string>> ListDirectoriesAsync(CancellationToken cancellationToken);

        Task<List<string>> ListFilePathsOfDirectoryAsync(string directory, CancellationToken cancellationToken);
    }
}
