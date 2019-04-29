using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.IO;


namespace Fundamentals_StaticFiles
{
    public class StartupFileExtensionContentTypeProvider
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDirectoryBrowser();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Set up custom content types -associating file extension to MIME type
            var provider = new FileExtensionContentTypeProvider();

            // Add new mappings
            provider.Mappings[".myapp"] = "application/x-msdownload";
            provider.Mappings[".htm3"] = "text/html";
            provider.Mappings[".image"] = "image/png";

            // Replace an existing mapping
            provider.Mappings[".rtf"] = "application/x-msdownload";

            // Remove MP4 videos.
            provider.Mappings.Remove(".mp4");

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images")),
                RequestPath = new PathString("/MyImages"),
                ContentTypeProvider = provider
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images")),
                RequestPath = new PathString("/MyImages")
            });
        }
    }
}
