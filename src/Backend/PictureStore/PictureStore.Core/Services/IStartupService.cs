using System.Threading;
using System.Threading.Tasks;

namespace PictureStore.Core.Services
{
    public interface IStartupService
    {
        Task StartAsync(CancellationToken cancellationToken);
    }
}
