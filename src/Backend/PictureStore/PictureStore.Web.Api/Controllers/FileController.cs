using System;
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

        [HttpGet]
        [Route("list")]
        public FileListingModel ListFile([FromQuery] string folder, CancellationToken cancellationToken)
        {
            return fileService.ListFiles(folder, cancellationToken);
        }

        [HttpGet]
        [Route("duplicates")]
        public async Task<IEnumerable<DuplicateFileModel>> GetDuplicates(CancellationToken cancellationToken)
        {
            return await fileService.ListDuplicatesAsync(cancellationToken);
        }

        [HttpGet]
        [Route("download/{path}")]
        public async Task DownloadFile([FromRoute] string path)
        {
            await fileService.DownloadAsync(path);
        }
    }
}
