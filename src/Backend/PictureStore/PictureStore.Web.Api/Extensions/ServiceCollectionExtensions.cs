using Microsoft.Extensions.DependencyInjection;
using PictureStore.Core.Services;

namespace PictureStore.Web.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<IFileUploadService, FileUploadService>();
            services.AddTransient<IFileDownloadService, FileDownloadService>();

            return services;
        }
    }
}
