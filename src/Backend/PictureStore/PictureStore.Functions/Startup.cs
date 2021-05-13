using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using PictureStore.Functions;

[assembly: FunctionsStartup(typeof(Startup))]
namespace PictureStore.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

        }
    }
}
