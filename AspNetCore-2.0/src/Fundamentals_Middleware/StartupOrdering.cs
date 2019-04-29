using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentals_Middleware
{
    /// <summary>
    /// The order that middleware components are added in the Configure method defines the order in which they are invoked on requests, and the reverse order for the response. 
    /// This ordering is critical for security, performance, and functionality.
    /// </summary>
    public class StartupOrdering
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseExceptionHandler("/Home/Error"); // Call first to catch exceptions
                                                    // thrown in the following middleware.

            app.UseStaticFiles();                   // Return static files and end pipeline. Static files responses wont be compressed

            app.UseIdentity();                     // Authenticate before you access
                                                   // secure resources.
            app.UseResponseCompression();
            app.UseMvcWithDefaultRoute();          // Add MVC to the request pipeline. MVC responses will be compressed
        }
    }
}
