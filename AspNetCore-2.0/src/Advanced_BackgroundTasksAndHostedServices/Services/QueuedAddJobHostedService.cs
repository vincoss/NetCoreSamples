using Advanced_BackgroundTasksAndHostedServices.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Advanced_BackgroundTasksAndHostedServices
{
    // Add task/job into the queue
    public class QueuedTasksHostedService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IBackgroundTaskQueue _queue;

        public QueuedTasksHostedService(IBackgroundTaskQueue queue, ILogger<QueuedTasksHostedService> logger)
        {
            _queue = queue;
            _logger = logger;
        }

        public IServiceProvider Services { get; }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Queued Add Job Hosted Service is starting.");

            DoWork();

            return Task.CompletedTask;
        }

        private void DoWork()
        {
            _logger.LogInformation("Queued Add Job Hosted Service is working.");

            _queue.QueueBackgroundWorkItem(async token =>
            {
                _logger.LogInformation("Queued Add Job Hosted Service - Begin run job.");

                await Task.Delay(TimeSpan.FromSeconds(5), token);

                _logger.LogInformation("Queued Add Job Hosted Service - End run job.");
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Queued Add Job Hosted Service is stopping.");

            return Task.CompletedTask;
        }
    }
}
