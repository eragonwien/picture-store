using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using PictureStore.Core.Models.AppSettings;
using PictureStore.Core.Services;
using PictureStore.Functions;

[assembly: FunctionsStartup(typeof(Startup))]
namespace PictureStore.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<PictureStoreAzureAppSettings>(PictureStoreAzureAppSettings.Key);

            builder.Services.AddScoped<IFileService, BlobFileService>();
        }
    }
}
