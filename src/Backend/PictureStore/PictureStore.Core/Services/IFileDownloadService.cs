using System.Threading.Tasks;

namespace PictureStore.Core.Services
{
    public interface IFileDownloadService
    {
        Task DownloadAsync(int id);
        Task ListAsync(int page);
    }
}