using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advanced_BackgroundTasksAndHostedServices.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Advanced_BackgroundTasksAndHostedServices
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Register hosted service
            services.AddHostedService<TimedHostedService>();
            services.AddHostedService<ConsumeScopedServiceHostedService>();
            services.AddHostedService<QueuedHostedService>();
            services.AddHostedService<QueuedTasksHostedService>();
            // Or
            //services.AddSingleton<TimedHostedService>();

            // Scoped
            services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
            // Singleton
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
