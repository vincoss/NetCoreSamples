using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Host_InWindowsService.Services
{
    public interface IControlService
    {
        IEnumerable<string> GetProcesses();
        void KillProcess(params string[] processes);
    }

    public class ControlServiceImpl : IControlService
    {
        private ILogger _logger;

        public ControlServiceImpl(ILogger<ControlServiceImpl> logger)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            _logger = logger;
        }

        public virtual void KillProcess(params string[] processes)
        {
            if (processes == null)
            {
                throw new ArgumentNullException(nameof(processes));
            }

            foreach (var processName in processes)
            {
                _logger.LogDebug("Kill process: {0}", processName);

                if (string.IsNullOrEmpty(processName))
                {
                    continue;
                }

                var name = processName;
                if (name.EndsWith(".exe", StringComparison.InvariantCultureIgnoreCase))
                {
                    name = name.Substring(0, name.IndexOf(".exe", StringComparison.InvariantCultureIgnoreCase));
                }

                Process[] collectionOfProcess = Process.GetProcessesByName(name);

                if (collectionOfProcess.Length <= 0)
                {
                    continue;
                }

                foreach (var process in collectionOfProcess)
                {
                    process.Kill();
                }
            }
        }

        public IEnumerable<string> GetProcesses()
        {
            return from x in Process.GetProcesses() select x.ProcessName;
        }

    }
}
