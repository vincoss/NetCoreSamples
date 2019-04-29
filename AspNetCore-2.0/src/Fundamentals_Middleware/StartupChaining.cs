using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentals_Middleware
{
    public class StartupChaining
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                // Do work that doesn't write to the Response.
                await next.Invoke();
                // Do logging or other work that doesn't write to the Response.
            });

            // Chain multiple request delegates with app.Use

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello from 2nd delegate.");
            });
        }
    }
}
