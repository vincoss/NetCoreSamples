using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentals_Middleware
{

    /*
        Run - method short-circuits the pipeline (does not call next request)
        Map - method if the request path start with given path then branch will be executed
     
    */

    public class StartupRunMapUse
    {
        public void Configure(IApplicationBuilder app)
        {
            // With custom class and extension method
            app.UseRequestCulture();

            app.Map("/map1", HandleMapTest1);

            app.Map("/map2", HandleMapTest2);

            app.MapWhen(context => context.Request.Query.ContainsKey("branch"), HandleBranch);

            // Map nesting

            app.Map("/level1", level1App =>
            {
                level1App.Map("/level2a", level2AApp =>
                {
                    // "/level1/level2a"
                    //...
                });
                level1App.Map("/level2b", level2BApp =>
                {
                    // "/level1/level2b"
                    //...
                });
            });

            // multiple segments once

            app.Map("/level1/level2", HandleMultiSeg);

            app.Run(async context =>
            {
                await context.Response.WriteAsync($"<p>Hello from non-Map delegate. Culture: {CultureInfo.CurrentCulture.DisplayName} <p>");
            });
        }
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
                await context.Response.WriteAsync("HandleMultiSeg");
            });
        }

    }
}
