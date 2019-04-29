using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamentals_Routing
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ExampleRoutes(app);

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        private void ExampleRoutes(IApplicationBuilder app)
        {
            var trackPackageRouteHandler = new RouteHandler(context =>
            {
                var routeValues = context.GetRouteData().Values;
                return context.Response.WriteAsync(
                    $"Hello! Route values: {string.Join(", ", routeValues)}");
            });

            var routeBuilder = new RouteBuilder(app, trackPackageRouteHandler);

            routeBuilder.MapRoute(
                "Track Package Route",
                "package/{operation:regex(track|create|detonate)}/{id:int}");

            routeBuilder.MapGet("hello/{name}", context =>
            {
                var name = context.GetRouteValue("name");
                // This is the route handler when HTTP GET "hello/<anything>"  matches
                // To match HTTP GET "hello/<anything>/<anything>, 
                // use routeBuilder.MapGet("hello/{*name}"
                return context.Response.WriteAsync($"Hi, {name}!");
            });

            // Default route
            routeBuilder.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}");

            // As above but with defaults
            routeBuilder.MapRoute(
            name: "default_route",
            template: "{controller}/{action}/{id?}",
            defaults: new { controller = "Home", action = "Index" });

            // Route with constraint
            routeBuilder.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id:int}");

            // Other with catch-all
            routeBuilder.MapRoute(
            name: "blog",
            template: "Blog/{*article}",
            defaults: new { controller = "Blog", action = "ReadArticle" });

            // Constraints and data-tokens
            routeBuilder.MapRoute(
            name: "us_english_products",
            template: "en-US/Products/{id}",
            defaults: new { controller = "Products", action = "Details" },
            constraints: new { id = new IntRouteConstraint() },
            dataTokens: new { locale = "en-US" });

            var routes = routeBuilder.Build();
            app.UseRouter(routes);
        }
    }
}
