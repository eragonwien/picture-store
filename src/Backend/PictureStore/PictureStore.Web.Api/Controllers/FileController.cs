using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PictureStore.Core.Services;

namespace PictureStore.Web.Api.Controllers
{
    [Route("files")]
    public class FileController : BaseController
    {
        private readonly IFileUploadService uploadService;
        private readonly IFileDownloadService fileDownloadService;
        private readonly IWebHostEnvironment env;

        public FileController(IFileUploadService uploadService, IFileDownloadService fileDownloadService, IWebHostEnvironment env)
        {
            this.uploadService = uploadService;
            this.fileDownloadService = fileDownloadService;
            this.env = env;
        }

        [HttpPost]
        [Route("")]
        public async Task UploadFile(IFormFileCollection files)
        {

        }

        [HttpGet]
        [Route("")]
        public async Task ListFile([FromQuery] int page)
        {
            await fileDownloadService.ListAsync(page);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task DownloadFile([FromRoute] int id)
        {
            await fileDownloadService.DownloadAsync(id);
        }
    }
}
