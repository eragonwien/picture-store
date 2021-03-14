using System.IO;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PictureStore.Web.Api.Extensions;
using PictureStore.Web.Api.Models;

namespace PictureStore.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCoreServices()
                .AddAppSettings(Configuration);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PictureStore.Web.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PictureStore.Web.Api v1"));
                app.UseHttpsRedirection();
            }
            else
            {
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
            }

            app.UseExceptionHandler(a => ConfigureExceptionHandler(a, env));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureExceptionHandler(IApplicationBuilder app, IHostEnvironment env)
        {
            app.Run(async context =>
            {
                var errorContext = context.Features.Get<IExceptionHandlerPathFeature>();

                if (errorContext is null) return;

                var statusCode = HttpStatusCode.InternalServerError;

                switch (errorContext.Error)
                {
                    case FileNotFoundException fileNotFoundException:
                        statusCode = HttpStatusCode.BadRequest;
                        break;
                    default:
                        break;
                }

                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = "application/json";

                var result = new ErrorResponseModel(errorContext.Error, env.IsDevelopment());
                await context.Response.WriteAsJsonAsync(result);
            });
        }
    }
}
