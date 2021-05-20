using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PictureStore.Core.Exceptions;
using PictureStore.Functions.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;
using PictureStore.Infrastructure.Services;

namespace PictureStore.Functions.Functions.Http
{
    public class HttpFunctionBase
    {
        protected readonly IFileService fileService;

        public HttpFunctionBase(IFileService fileService)
        {
            this.fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        protected async Task<IActionResult> ProcessAsync<TModel>(
            HttpRequest request,
            ILogger log,
            CancellationToken cancellationToken,
            Func<Task<IActionResult>> mainProcess)
            where TModel : class
        {
            if (request is null) throw new ArgumentNullException(nameof(request));
            if (log is null) throw new ArgumentNullException(nameof(log));
            if (mainProcess is null) throw new ArgumentNullException(nameof(mainProcess));

            var model = await request.ParseBodyAsync<TModel>();

            return await TryProcessAsync(
                model, 
                log,
                cancellationToken,
                _ => mainProcess());
        }

        protected async Task<IActionResult> ProcessAsync(
            HttpRequest request,
            ILogger log,
            CancellationToken cancellationToken,
            Func<dynamic, Task<IActionResult>> mainProcess)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));
            if (log is null) throw new ArgumentNullException(nameof(log));
            if (mainProcess is null) throw new ArgumentNullException(nameof(mainProcess));

            return await TryProcessAsync(
                null, 
                log,
                cancellationToken,
                mainProcess);
        }

        protected async Task<IActionResult> ProcessFileAsync(
            HttpRequest request,
            ILogger log,
            CancellationToken cancellationToken,
            Func<IFormFileCollection, Task<IActionResult>> mainProcess)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));
            if (log is null) throw new ArgumentNullException(nameof(log));
            if (mainProcess is null) throw new ArgumentNullException(nameof(mainProcess));

            return await TryProcessAsync(
                request.Form?.Files,
                log, 
                cancellationToken,
                mainProcess);
        }

        protected IActionResult Ok(object value = null)
        {
            if (value is null)
                return new OkResult();

            return new OkObjectResult(value);
        }

        protected IActionResult NoContent()
        {
            return new NoContentResult();
        }

        protected IActionResult BadRequest(object value)
        {
            if (value is null)
                return new BadRequestResult();

            return new BadRequestObjectResult(value);
        }

        protected IActionResult FileResult(byte[] bytes, string contentType = "application/octet-stream")
            => new FileContentResult(bytes, contentType);

        protected void ThrowIfFileIsEmpty(IFormFileCollection files)
        {
            if (files is null || files.Count == 0)
                throw new UploadFileEmptyException();
        }

        private async Task<IActionResult> TryProcessAsync<TModel>(
            TModel model,
            ILogger log,
            CancellationToken cancellationToken,
            Func<TModel, Task<IActionResult>> mainProcess)
            where TModel : class
        {
            if (mainProcess is null) throw new ArgumentNullException(nameof(mainProcess));

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                return await mainProcess(model);
            }
            catch (Azure.RequestFailedException azureRequestFailedEx)
            {
                throw;
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Unhandled error occurred");
                throw;
            }
        }
    }
}
