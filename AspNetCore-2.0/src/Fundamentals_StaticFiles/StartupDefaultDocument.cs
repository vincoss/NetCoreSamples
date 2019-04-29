using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace Fundamentals_StaticFiles
{
    public class StartupDefaultDocument
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDirectoryBrowser();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("MyIndex.html");
            app.UseDefaultFiles(options); // NOTE: Must be called before UseStaticFiles to server default file

            // Or
            //app.UseDefaultFiles(); // without options

            app.UseStaticFiles();
        }
    }
}
