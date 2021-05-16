using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading;
using PictureStore.Core.Services;
using PictureStore.Functions.Models;

namespace PictureStore.Functions.Functions.Http
{
    public class FileHttpFunctions : HttpFunctionBase
    {
        private const string routePrefix = "files/";
        private const string functionNamePrefix = "files-";

        public FileHttpFunctions(IFileService fileService) : base(fileService)
        {
        }

        [FunctionName(functionNamePrefix + nameof(UploadFiles))]
        public async Task<IActionResult> UploadFiles(
            [HttpTrigger(AuthorizationLevel.Function, HttpMethod.POST, Route = routePrefix)] HttpRequest req,
            ILogger log,
            CancellationToken cancellationToken)
        {
            return await ProcessFileAsync(
                req,
                log,
                cancellationToken,
                mainProcess: async (files) =>
                {
                    ThrowIfFileIsEmpty(files);

                    foreach (var file in files)
                    {
                        await using var fileStream = file.OpenReadStream();
                        await fileService.UploadAsync(file.FileName, fileStream, cancellationToken);
                    }

                    return NoContent();
                });
        }

        [FunctionName(functionNamePrefix + nameof(ListFiles))]
        public async Task<IActionResult> ListFiles(
            [HttpTrigger(AuthorizationLevel.Function, HttpMethod.GET, Route = routePrefix)] HttpRequest req,
            ILogger log,
            CancellationToken cancellationToken)
        {
            return Ok();
        }

        [FunctionName(functionNamePrefix + nameof(DownloadFile))]
        public async Task<IActionResult> DownloadFile(
            [HttpTrigger(AuthorizationLevel.Function, HttpMethod.GET, Route = routePrefix + "{folder}/{filename}")] HttpRequest req,
            ILogger log,
            CancellationToken cancellationToken)
        {
            return Ok();
        }

        [FunctionName(functionNamePrefix + nameof(DeleteFile))]
        public async Task<IActionResult> DeleteFile(
            [HttpTrigger(AuthorizationLevel.Function, HttpMethod.DELETE, Route = routePrefix + "{folder}/{filename}")] HttpRequest req,
            ILogger log,
            CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
