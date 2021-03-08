using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PictureStore.Core.Models;
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
        public async Task<FileUploadResult> UploadFile(IFormFileCollection files, CancellationToken cancellationToken)
        {
            var results = new FileUploadResult();

            foreach (var file in files)
            {
                if (file.Length == 0) continue;

                await using var fileStream = file.OpenReadStream();
                results.Add(await fileService.UploadAsync(file.FileName, fileStream, cancellationToken));
            }

            return results;
        }

        [HttpPost]
        [Route("move")]
        public async Task MoveToDownloadFolder(CancellationToken cancellationToken)
        {
            await fileService.MoveToDownloadFolderAsync(cancellationToken);
        }

        [HttpPost]
        [Route("cleanup")]
        public async Task Cleanup(CancellationToken cancellationToken)
        {
            await fileService.CleanupAsync(cancellationToken);
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
