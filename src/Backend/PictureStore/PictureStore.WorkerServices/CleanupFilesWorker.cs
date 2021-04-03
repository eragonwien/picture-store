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
    public class CleanupFilesWorker : BackgroundService
    {
        private readonly ILogger<FileTransferWorker> logger;
        private readonly IFileService fileService;
        private readonly PictureStoreCleanupFilesAppSettings cleanupFilesAppSettings;

        public CleanupFilesWorker(ILogger<FileTransferWorker> logger, 
            IFileService fileService,
            IOptions<PictureStoreCleanupFilesAppSettings> cleanupFilesAppSettingsOptions)
        {
            this.logger = logger;
            this.fileService = fileService;
            cleanupFilesAppSettings = cleanupFilesAppSettingsOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                logger.LogInformation("CleanupFilesWorker started at: {time}", DateTimeOffset.Now);

                try
                {
                    await fileService.CleanupFilesAsync(cancellationToken);

                    logger.LogInformation("CleanupFilesWorker completed at: {time}", DateTimeOffset.Now);
                    logger.LogInformation("CleanupFilesWorker start again at: {time}", DateTimeOffset.Now.Add(cleanupFilesAppSettings.Interval));
                }
                catch (TaskCanceledException)
                {
                    logger.LogInformation("FileTransferWorker was cancelled at: {time}", DateTimeOffset.Now);
                    break;
                }
            }
        }
    }
}
