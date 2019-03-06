using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentals_TheHost.Services
{
    // Gracefully shut down an app when required. (suicide)
    public class Stopper
    {
        private readonly IApplicationLifetime _appLifetime;

        public Stopper(IApplicationLifetime appLifetime)
        {
            _appLifetime = appLifetime;
        }

        public void Shutdown()
        {
            _appLifetime.StopApplication();
        }
    }
}
