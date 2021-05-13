using Microsoft.AspNetCore.Http;

namespace PictureStore.Functions.Models.ViewModels
{
    public class UploadRequestViewModel
    {
        public IFormCollection Files { get; set; }
    }
}
