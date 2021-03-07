using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PictureStore.Core.Services
{
    public interface IFileService
    {
        Task UploadAsync(Stream stream, CancellationToken cancellationToken);
        Task ListAsync(int page);
        Task DownloadAsync(int id);
    }
}
