﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PictureStore.Core.Services;

namespace PictureStore.Functions.Functions.Timer
{
    public class StartupTimerFunctions : TimerFunctionBase
    {
        private const string FunctionNamePrefix = "startup-timer-";

        private readonly IStartupService startupService;

        public StartupTimerFunctions(IStartupService startupService)
        {
            this.startupService = startupService ?? throw new ArgumentNullException(nameof(startupService));
        }

        [FunctionName(FunctionNamePrefix + nameof(StartAsync))]
        public Task StartAsync(
            [TimerTrigger("0 0 0 * * *", RunOnStartup = true)] TimerInfo timerInfo, 
            ILogger log, 
            CancellationToken cancellationToken)
        {
            return ProcessAsync(
                timerInfo, 
                log, 
                cancellationToken, 
                mainProcess: async () =>
                {
                    await startupService.StartAsync(cancellationToken);
                });
        }
    }
}
