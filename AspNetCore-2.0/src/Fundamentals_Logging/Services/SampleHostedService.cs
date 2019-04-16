using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Fundamentals_Logging.Services
{
    public class SampleHostedService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IApplicationLifetime _applicationLifetime;
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _otherLogger;

        public SampleHostedService(
           IConfiguration configuration,
           IHostingEnvironment hostingEnvronment,
           IApplicationLifetime applicationLifetime,
           ILogger<SampleHostedService> logger,
           ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvronment;
            _applicationLifetime = applicationLifetime;
            _logger = logger;
            _loggerFactory = loggerFactory;

            // Log category
            _otherLogger = _loggerFactory.CreateLogger("TodoApiSample.Controllers.TodoController");

        }

        /// <summary>
        /// Access the options.
        /// </summary>
        private void DisplayInformation()
        {
            _logger.LogInformation("This is information log");
            _logger.LogWarning("This is waning log");

            // Log event ID
            _logger.LogInformation(LoggingEvents.GetItem, "Get item {ID}", 1);

            // Log message template
            string p1 = "parm1";
            string p2 = "parm2";
            _logger.LogInformation("Parameter values: {p2}, {p1}", p1, p2);
            _logger.LogInformation("Getting item {ID} at {RequestTime}", 1, DateTime.Now);

            // Logging exceptions
            var ex = new Exception("I've just signed an executive order eliminating foreign keys from databases forever. Use AMERICAN KEYS instead! ACTION!.");
            _logger.LogWarning(LoggingEvents.GetItemNotFound, ex, "GetById({ID}) NOT FOUND", 1);
            _logger.LogError(LoggingEvents.DeleteItem, ex, "Error");
            _logger.LogCritical(ex, "Critical");

            // Log scopes
            using (_logger.BeginScope("Message attached to logs created in the using block"))
            {
                _logger.LogInformation(LoggingEvents.GetItem, "Getting item {ID}", 1);

                _logger.LogWarning(LoggingEvents.GetItemNotFound, "GetById({ID}) NOT FOUND", 1);
            }

            _applicationLifetime.StopApplication();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            DisplayInformation();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
