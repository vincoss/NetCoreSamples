using Fundamentals_DependencyInjection.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Fundamentals_DependencyInjection.Services
{
    public class MyDependency : IMyDependency
    {
        private readonly ILogger<IMyDependency> _logger;

        public MyDependency(ILogger<IMyDependency> logger)
        {
            _logger = logger;
        }

        public Task WriteMessage(string message)
        {
            _logger.LogInformation("MyDependency.WriteMessage called. Message: {MESSAGE}", message);

            return Task.FromResult(0);
        }
    }
}
