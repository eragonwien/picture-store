using Microsoft.AspNetCore.Mvc;

namespace PictureStore.WebApi.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/files")]
    public class FileController : V1Controller
    {
        [HttpGet]
        public string List()
        {
            return "List of things";
        }
    }
}
