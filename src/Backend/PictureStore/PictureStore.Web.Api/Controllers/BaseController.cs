using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PictureStore.Core.Exceptions;

namespace PictureStore.Web.Api.Controllers
{
   [ApiController]
   public class BaseController : ControllerBase
   {
      protected void ThrowIfFileIsEmpty(IFormFile file)
      {
         if (file is null || file.Length == 0)
            throw new UploadFileEmptyException();
      }

      protected string AppBaseUrl => $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
   }
}
