using System.IO;
using System.Threading;
using System.Threading.Tasks;
using PictureStore.Core.Models;

namespace PictureStore.Core.Services
{
    public interface IFileService
    {
        Task<FileUploadPartialResult> UploadAsync(
            string inputFileName,
            Stream stream, 
            CancellationToken cancellationToken);

        Task ListAsync(int page);

        Task DownloadAsync(int id);

        Task CleanUpAsync(CancellationToken cancellationToken);
    }
}
