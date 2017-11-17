using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Fundamentals_Routing
{
    public class UrlGenerationReferenceStartup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Using routing middleware
            services.AddRouting();

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var routeBuilder = new RouteBuilder(app);
            var routes = routeBuilder.Build();
            app.UseRouter(routes);

            app.Run(async (context) =>
            {
                var dictionary = new RouteValueDictionary
                {
                    { "operation", "create" },
                    { "id", 123}
                };

                var vpc = new VirtualPathContext(context, null, dictionary, "Track Package Route");
                var path = routes.GetVirtualPath(vpc).VirtualPath;

                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("Menu<hr/>");
                await context.Response.WriteAsync($"<a href='{path}'>Create Package 123</a><br/>");
            });
        }
    }
}
