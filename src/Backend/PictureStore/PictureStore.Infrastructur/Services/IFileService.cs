using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using PictureStore.Core.Models;

namespace PictureStore.Infrastructure.Services
{
    public interface IFileService
    {
        Task UploadAsync(Stream stream, CancellationToken cancellationToken);

        Task<byte[]> DownloadAsync(string folder, string filename, CancellationToken cancellationToken);

        Task DeleteAsync(string folder, string filename, CancellationToken cancellationToken);

        Task PrepareContainersAsync(CancellationToken cancellationToken);

        Task<List<string>> ListDirectoriesAsync(CancellationToken cancellationToken);

        Task<List<string>> ListFilePathsOfDirectoryAsync(string directory, CancellationToken cancellationToken);
    }
}