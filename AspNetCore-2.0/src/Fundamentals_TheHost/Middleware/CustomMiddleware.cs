using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentals_TheHost.Middleware
{
    public class CustomMiddleware
    {
        public async Task Invoke(HttpContext context, IHostingEnvironment env) // Inject inoto the invoke method.
        {
            if (env.IsDevelopment())
            {
                // Configure middleware for Development
            }
            else
            {
                // Configure middleware for Staging/Production
            }

            var contentRootPath = env.ContentRootPath;
        }
    }
}
