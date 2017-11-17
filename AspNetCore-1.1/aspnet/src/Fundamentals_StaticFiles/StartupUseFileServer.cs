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
    public class StatupUseFileServer
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDirectoryBrowser(); // If EnableDirectoryBrowsing = true
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseFileServer();

            // or use other extensions of UseFileServer
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"MyStaticFiles")),
                RequestPath = new PathString("/StaticFiles"),
                EnableDirectoryBrowsing = false // if set to true call services.AddDirectoryBrowser(); under ConfigureServices method
            });

            // http://localhost:50652/StaticFiles               // If EnableDirectoryBrowsing = true
            // http://localhost:50652/StaticFiles/Text1.txt
        }
    }
}
