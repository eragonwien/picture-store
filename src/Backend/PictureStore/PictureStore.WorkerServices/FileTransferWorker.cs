using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PictureStore.Core.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PictureStore.WorkerServices
{
    public class FileTransferWorker : BackgroundService
    {
        private readonly ILogger<FileTransferWorker> _logger;
        private readonly IFileService _fileService;

        public FileTransferWorker(ILogger<FileTransferWorker> logger, IFileService fileService)
        {
            _logger = logger;
            _fileService = fileService;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("FileTransferWorker started at: {time}", DateTimeOffset.Now);

                await _fileService.TransferFileToDownloadFolderAsync(cancellationToken);

                _logger.LogInformation("FileTransferWorker completed at: {time}", DateTimeOffset.Now);
                _logger.LogInformation("FileTransferWorker start again at: {time}", DateTimeOffset.Now.AddSeconds(5));
                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }
        }
    }
}
