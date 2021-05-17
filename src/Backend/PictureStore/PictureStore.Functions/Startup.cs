using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PictureStore.Core.Models.AppSettings;
using PictureStore.Functions;
using PictureStore.Infrastructure.Services;

[assembly: FunctionsStartup(typeof(Startup))]
namespace PictureStore.Functions
{
    public sealed class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            var configuration = ConfigureAppSettings();
            builder.Services.AddSingleton(configuration);

            builder.Services.Configure<PictureStoreAzureAppSettings>(configuration.GetSection(PictureStoreAzureAppSettings.Section));

            builder.Services.AddTransient<IFileService, FileService>();
        }

        private static IConfiguration ConfigureAppSettings()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", false, false)
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
