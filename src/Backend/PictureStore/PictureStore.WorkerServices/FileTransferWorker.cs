using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PictureStore.Core.Models.AppSettings;
using PictureStore.Core.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PictureStore.WorkerServices
{
    public class FileTransferWorker : BackgroundService
    {
        private readonly ILogger<FileTransferWorker> logger;
        private readonly IFileService fileService;
        private readonly PictureStoreFileTransferAppSettings fileTransferAppSettings;

        public FileTransferWorker(ILogger<FileTransferWorker> logger,
            IFileService fileService,
            IOptions<PictureStoreFileTransferAppSettings> fileTransferAppSettingsOptions)
        {
            this.logger = logger;
            this.fileService = fileService;
            fileTransferAppSettings = fileTransferAppSettingsOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                logger.LogInformation("FileTransferWorker started at: {time}", DateTimeOffset.Now);

                await fileService.TransferFileToDownloadFolderAsync(cancellationToken);

                logger.LogInformation("FileTransferWorker completed at: {time}", DateTimeOffset.Now);
                logger.LogInformation("FileTransferWorker start again at: {time}", DateTimeOffset.Now.Add(fileTransferAppSettings.Interval));
                await Task.Delay(fileTransferAppSettings.Interval, cancellationToken);
            }
        }
    }
}
