using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PictureStore.Core.Models.AppSettings;
using PictureStore.Core.Services;

namespace PictureStore.Web.Api.Controllers
{
    [Route("files")]
    public class FileController : BaseController
    {
        private readonly IFileService fileService;

        public FileController(IFileService uploadService)
        {
            fileService = uploadService;
        }

        [HttpPost]
        [Route("")]
        public async Task UploadFile(IFormFileCollection files, CancellationToken cancellationToken)
        {
            foreach (var file in files)
            {
                if (file.Length == 0) continue;

                await using Stream fileStream = file.OpenReadStream();
                await fileService.UploadAsync(fileStream, cancellationToken);
            }
        }

        [HttpGet]
        [Route("")]
        public async Task ListFile([FromQuery] int page)
        {
            await fileService.ListAsync(page);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task DownloadFile([FromRoute] int id)
        {
            await fileService.DownloadAsync(id);
        }
    }
}
