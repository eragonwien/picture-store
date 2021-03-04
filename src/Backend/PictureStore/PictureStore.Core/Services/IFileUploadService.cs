using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace PictureStore.Core.Services
{
    public interface IFileUploadService
    {
        Task UploadAsync(string envContentRootPath, IFormFileCollection files);
    }
}
