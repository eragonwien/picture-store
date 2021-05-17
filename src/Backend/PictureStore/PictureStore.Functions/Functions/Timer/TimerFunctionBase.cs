using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace PictureStore.Functions.Functions.Timer
{
    public class TimerFunctionBase
    {
        protected Task ProcessAsync(TimerInfo timerInfo, ILogger log, CancellationToken cancellationToken, Func<Task> mainProcess)
        {
            if (timerInfo == null) throw new ArgumentNullException(nameof(timerInfo));
            if (log == null) throw new ArgumentNullException(nameof(log));

            cancellationToken.ThrowIfCancellationRequested();

            return mainProcess();
        }
    }
}
