using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PictureStore.Core.Models.AppSettings;
using PictureStore.Core.Services;

namespace PictureStore.WorkerServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<FileTransferWorker>();
                    services.AddHostedService<CleanupFilesWorker>();
                    services.AddSingleton<IFileService, FileService>();

                    services.Configure<PictureStoreUploadAppSettings>(hostContext.Configuration.GetSection(PictureStoreUploadAppSettings.Section));
                    services.Configure<PictureStoreDownloadAppSettings>(hostContext.Configuration.GetSection(PictureStoreDownloadAppSettings.Section));
                    services.Configure<PictureStoreFileTransferAppSettings>(hostContext.Configuration.GetSection(PictureStoreFileTransferAppSettings.Section));
                    services.Configure<PictureStoreCleanupFilesAppSettings>(hostContext.Configuration.GetSection(PictureStoreCleanupFilesAppSettings.Section));
                });
    }
}
