using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading;
using PictureStore.Infrastructure.Services;

namespace PictureStore.Functions.Functions.Http
{
    public class FileHttpFunctions : HttpFunctionBase
    {
        private const string RoutePrefix = "files";
        private const string FunctionNamePrefix = "files-http-";

        public FileHttpFunctions(IFileService fileService) : base(fileService)
        {
        }

        [FunctionName(FunctionNamePrefix + nameof(UploadFiles))]
        public Task<IActionResult> UploadFiles(
            [HttpTrigger(AuthorizationLevel.Function, HttpMethod.POST, Route = RoutePrefix)] HttpRequest req,
            ILogger log,
            CancellationToken cancellationToken)
        {
            return ProcessFileAsync(
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

        [FunctionName(FunctionNamePrefix + nameof(ListDirectories))]
        public Task<IActionResult> ListDirectories(
            [HttpTrigger(AuthorizationLevel.Function, HttpMethod.GET, Route = RoutePrefix)] HttpRequest req,
            ILogger log,
            CancellationToken cancellationToken)
        {
            return ProcessAsync(
                req,
                log,
                cancellationToken,
                mainProcess: async _ => Ok(await fileService.ListDirectoriesAsync(cancellationToken)));
        }

        [FunctionName(FunctionNamePrefix + nameof(ListFilesOfDirectory))]
        public Task<IActionResult> ListFilesOfDirectory(
            [HttpTrigger(AuthorizationLevel.Function, HttpMethod.GET, Route = RoutePrefix + "/{directory}")] HttpRequest req,
            ILogger log,
            string directory,
            CancellationToken cancellationToken)
        {
            return ProcessAsync(
                req,
                log,
                cancellationToken,
                mainProcess: async _ => Ok(await fileService.ListFilePathsOfDirectoryAsync(directory, cancellationToken)));
        }

        [FunctionName(FunctionNamePrefix + nameof(DownloadFile))]
        public Task<IActionResult> DownloadFile(
            [HttpTrigger(AuthorizationLevel.Function, HttpMethod.GET, Route = RoutePrefix + "/{directory}/{filename}")] HttpRequest req,
            ILogger log,
            string directory,
            string filename,
            CancellationToken cancellationToken)
        {
            return ProcessAsync(
                req,
                log,
                cancellationToken,
                mainProcess: async _ => Ok(await fileService.DownloadAsync(directory, filename, cancellationToken)));
        }

        [FunctionName(FunctionNamePrefix + nameof(DeleteFile))]
        public Task<IActionResult> DeleteFile(
            [HttpTrigger(AuthorizationLevel.Function, HttpMethod.DELETE, Route = RoutePrefix + "{directory}/{filename}")] HttpRequest req,
            ILogger log,
            string directory,
            string filename,
            CancellationToken cancellationToken)
        {
            return ProcessAsync(
                req,
                log,
                cancellationToken,
                mainProcess: async _ =>
                {
                    await fileService.DeleteAsync(directory, filename, cancellationToken);

                    return NoContent();
                });
        }
    }
}
