using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading;
using PictureStore.Core.Services;

namespace PictureStore.Functions.Functions.Http
{
    public class FileHttpFunctions : HttpFunctionBase
    {
        private const string RoutePrefix = "files/";
        private const string FunctionNamePrefix = "files-http-";

        public FileHttpFunctions(IFileService fileService) : base(fileService)
        {
        }

        [FunctionName(FunctionNamePrefix + nameof(UploadFiles))]
        public async Task<IActionResult> UploadFiles(
            [HttpTrigger(AuthorizationLevel.Function, HttpMethod.POST, Route = RoutePrefix)] HttpRequest req,
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
                        await fileService.UploadAsync(fileStream, cancellationToken);
                    }

                    return NoContent();
                });
        }

        [FunctionName(FunctionNamePrefix + nameof(ListFiles))]
        public async Task<IActionResult> ListFiles(
            [HttpTrigger(AuthorizationLevel.Function, HttpMethod.GET, Route = RoutePrefix)] HttpRequest req,
            ILogger log,
            CancellationToken cancellationToken)
        {
            return Ok();
        }

        [FunctionName(FunctionNamePrefix + nameof(DownloadFile))]
        public async Task<IActionResult> DownloadFile(
            [HttpTrigger(AuthorizationLevel.Function, HttpMethod.GET, Route = RoutePrefix + "{folder}/{filename}")] HttpRequest req,
            ILogger log,
            CancellationToken cancellationToken)
        {
            return Ok();
        }

        [FunctionName(FunctionNamePrefix + nameof(DeleteFile))]
        public async Task<IActionResult> DeleteFile(
            [HttpTrigger(AuthorizationLevel.Function, HttpMethod.DELETE, Route = RoutePrefix + "{folder}/{filename}")] HttpRequest req,
            ILogger log,
            CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
