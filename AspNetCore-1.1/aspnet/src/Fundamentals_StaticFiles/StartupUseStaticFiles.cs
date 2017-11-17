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
    public class StartupUseStaticFiles
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // WARNING: UseDirectoryBrowser and UseStaticFiles can leak secrets. 

            services.AddDirectoryBrowser();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // 1. Serving static files (fore wwwroot folder)
            app.UseStaticFiles();

            // NOTE: For static files security is not applied.Use controller and file result instead.
            // http://localhost:50652/staticfiles/text1.txt
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"MyStaticFiles")),
                RequestPath = new PathString("/StaticFiles"),

                // Can set repsonse headers (example caching)
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=600");
                }

            });

            // NOTE: Directory browsing open lots of sec issues
            // http://localhost:50652/staticfiles/
            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"MyStaticFiles")),
                RequestPath = new PathString("/StaticFiles")
            });
        }
    }
}
