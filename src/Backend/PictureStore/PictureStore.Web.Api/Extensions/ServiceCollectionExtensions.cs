using Microsoft.Extensions.DependencyInjection;
using PictureStore.Core.Models.AppSettings;
using PictureStore.Core.Services;

namespace PictureStore.Web.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<IFileService, FileService>();

            return services;
        }

        public static IServiceCollection AddAppSettings(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            services.Configure<PictureStoreUploadAppSettings>(configuration.GetSection(PictureStoreUploadAppSettings.Section));
            services.Configure<PictureStoreDownloadAppSettings>(configuration.GetSection(PictureStoreDownloadAppSettings.Section));

            return services;
        }
    }
}
