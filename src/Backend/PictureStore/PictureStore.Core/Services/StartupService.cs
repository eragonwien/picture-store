using System;
using System.Threading;
using System.Threading.Tasks;

namespace PictureStore.Core.Services
{
    public class StartupService : IStartupService
    {
        private readonly IFileService fileService;
        private static bool started;

        public StartupService(IFileService fileService)
        {
            this.fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested || started) return;

            await fileService.PrepareContainersAsync(cancellationToken);
            started = true;
        }
    }
}