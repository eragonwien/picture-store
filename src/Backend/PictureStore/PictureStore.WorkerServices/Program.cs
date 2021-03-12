using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                    services.AddSingleton<IFileService, FileService>();
                });
    }
}
