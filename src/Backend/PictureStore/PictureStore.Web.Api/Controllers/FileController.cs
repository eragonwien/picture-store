using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public async Task UploadFile(IFormFile file, CancellationToken cancellationToken)
        {
            ThrowIfFileIsEmpty(file);

            await using var fileStream = file.OpenReadStream();
            await fileService.UploadAsync(file.FileName, fileStream, cancellationToken);
        }

        [HttpPost]
        [Route("move")]
        public async Task MoveToDownloadFolder(CancellationToken cancellationToken)
        {
            await fileService.TransferFileToDownloadFolderAsync(cancellationToken);
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<string> ListFiles()
        {
            return fileService.PageFiles()
                .SelectMany(f => f.Value.Select(path => Path.Combine(AppBaseUrl, "files", "download", f.Key, path)));
        }

        [HttpGet]
        [Route("page")]
        public Dictionary<string, string[]> PageFiles()
        {
            return fileService.PageFiles()
                .ToDictionary(
                    f => f.Key, 
                    f => f.Value.Select(path => Path.Combine(AppBaseUrl, "files", "download", f.Key, path)).ToArray());
        }

        [HttpGet]
        [Route("duplicates")]
        public async Task<IEnumerable<DuplicateFileModel>> GetDuplicates(CancellationToken cancellationToken)
        {
            return await fileService.ListDuplicatesAsync(cancellationToken);
        }

        [HttpGet]
        [Route("download/{folder}/{filename}")]
        public async Task<FileContentResult> DownloadFile([FromRoute] string folder, [FromRoute] string filename, CancellationToken cancellationToken)
        {
            var result = await fileService.DownloadAsync(folder, filename, cancellationToken);

            return File(result.Content, result.ContentType);
        }
    }
}
