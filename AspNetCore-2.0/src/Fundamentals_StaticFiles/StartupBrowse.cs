using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentals_StaticFiles
{
    public class StartupBrowse
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDirectoryBrowser();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // NOTE: Directory browsing open lots of security issues
            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images")),
                RequestPath = new PathString("/MyImages")
            });
        }
    }
}
