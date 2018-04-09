using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Host_InWindowsService.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;


namespace Host_InWindowsService
{
    /*
        Test: start command line with parameter Host_InWindowsService.exe --console
        http://localhost:5000/api/control/getProcesses
    */
    public class Program
    {
        public static void Main(string[] args)
        {
            var localArgs = new List<string>();

            foreach (var arg in args)
            {
                // Skip this
                if (string.Equals("--console", arg, StringComparison.CurrentCultureIgnoreCase))
                {
                    continue;
                }
                localArgs.Add(arg);
            }


            bool isService = true;
            if (Debugger.IsAttached || args.Contains("--console"))
            {
                isService = false;
            }

            var pathToContentRoot = Directory.GetCurrentDirectory();
            if (isService)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                pathToContentRoot = Path.GetDirectoryName(pathToExe);
            }

            var host = WebHost.CreateDefaultBuilder(localArgs.ToArray())
                .UseContentRoot(pathToContentRoot)
                .UseStartup<Startup>()
                //.UseApplicationInsights()
                .UseUrls(new[]
                {
                    //"http://localhost",
                    //"http://AH801879",
                    "http://localhost:5000",
                    //"http://AH801879:5000"
                })
                .Build();

            if (isService)
            {
                host.RunAsCustomService();
            }
            else
            {
                host.Run();
            }
        }
    }
}
