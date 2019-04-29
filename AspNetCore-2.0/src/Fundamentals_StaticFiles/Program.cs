using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Fundamentals_StaticFiles
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WatchForChanges(args);
        }

        public static void DefaultSample(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args)
                                 .UseStartup<Startup>();

            builder.Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        #region WatchForChanges

        public static void WatchForChanges(string[] args)
        {
            Console.WriteLine("Monitoring quotes.txt for changes (Ctrl-c to quit)...");

            while (true)
            {
                //WatchForChanges_MainAsync().GetAwaiter().GetResult();
            }
        }

        private static async Task WatchForChanges_MainAsync()
        {
            var fileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());

            IChangeToken token = fileProvider.Watch("quotes.txt");
            var tcs = new TaskCompletionSource<object>();

            token.RegisterChangeCallback(state =>
                ((TaskCompletionSource<object>)state).TrySetResult(null), tcs);

            await tcs.Task.ConfigureAwait(false);

            Console.WriteLine("quotes.txt changed");
        }
        #endregion
    }
}
