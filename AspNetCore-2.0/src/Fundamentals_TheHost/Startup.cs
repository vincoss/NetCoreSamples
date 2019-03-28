using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fundamentals_TheHost.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamentals_TheHost
{
    public class Startup
    {
        public IHostingEnvironment HostingEnvironment { get; }

        public Startup(IHostingEnvironment env) // inject into the constructor
        {
            HostingEnvironment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IScropeSampleService, ScropeSampleService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopping.Register(OnStopping);
            appLifetime.ApplicationStopped.Register(OnStopped);

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                appLifetime.StopApplication();
                // Don't terminate the process immediately, wait for the Main thread to exit gracefully.
                eventArgs.Cancel = true;
            };

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var contentRootPath = HostingEnvironment.ContentRootPath;

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World! " + contentRootPath);
            });
        }

        // Lifetime
        private void OnStarted()
        {
            // Perform post-startup activities here
        }

        private void OnStopping()
        {
            // Perform on-stopping activities here
        }

        private void OnStopped()
        {
            // Perform post-stopped activities here
        }
    }
}
