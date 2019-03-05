using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamentals_Middleware
{
    /*
        Example Order

        1. Exception/error handling
        2. HTTP Strict Transport Security Protocol
        3. HTTPS redirection
        4. Static file server
        5. Cookie policy enforcement
        6. Authentication
        7. Session
        8. MVC
    */

    public class StartupBasic
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello, World!");
            });
        }
    }

    public class StartupMultiple
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                // Do work that doesn't write to the Response.

                await next.Invoke();

                // Do logging or other work that doesn't write to the Response.
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello from 2nd delegate.");
            });
        }
    }

    public class StartupOrder
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // When the app runs in the Development environment:
                //   Use the Developer Exception Page to report app runtime errors.
                //   Use the Database Error Page to report database runtime errors.
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                // When the app doesn't run in the Development environment:
                //   Enable the Exception Handler Middleware to catch exceptions
                //     thrown in the following middlewares.
                //   Use the HTTP Strict Transport Security Protocol (HSTS)
                //     Middleware.
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // Use HTTPS Redirection Middleware to redirect HTTP requests to HTTPS.
            app.UseHttpsRedirection();

            // Return static files and end the pipeline.
            app.UseStaticFiles();

            // Use Cookie Policy Middleware to conform to EU General Data 
            // Protection Regulation (GDPR) regulations.
            app.UseCookiePolicy();

            // Authenticate before the user accesses secure resources.
            app.UseAuthentication();

            // If the app uses session state, call Session Middleware after Cookie 
            // Policy Middleware and before MVC Middleware.
            app.UseSession();

            // Add MVC to the request pipeline.
            app.UseMvc();
        }
    }

    public class StartupUseRunMap
    {
        private static void HandleMapTest1(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test 1");
            });
        }

        private static void HandleMapTest2(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test 2");
            });
        }

        private static void HandleBranch(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var branchVer = context.Request.Query["branch"];
                await context.Response.WriteAsync($"Branch used = {branchVer}");
            });
        }

        private static void HandleMultiSeg(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map multiple segments.");
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Map("/map1", HandleMapTest1);   // https://localhost:44315/map1

            app.Map("/map2", HandleMapTest2);   // https://localhost:44315/map1

            app.MapWhen(context => context.Request.Query.ContainsKey("branch"), HandleBranch); // https://localhost:44315/?branch=dev

            // Nesting
            app.Map("/level1", level1App => 
            {
                level1App.Map("/level2a", level2AApp => 
                {
                    // "/level1/level2a" processing
                });
                level1App.Map("/level2b", level2BApp => 
                {
                    // "/level1/level2b" processing
                });
            });

            // Multisegment
            app.Map("/map3/seg1", HandleMultiSeg);

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello from non-Map delegate. <p>"); // https://localhost:44315/
            });
        }
    }
}
