using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;
using Microsoft.Extensions.WebEncoders;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Fundamentals_HandleErrors
{
    public class StartupUseStatusCodePages
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.Microsoft.AspNet.Builder.UseIISPlatformHandler();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.MapWhen(context => context.Request.Path == "/missingpage", builder => { });

            // "/errors/400"
            app.Map("/errors", error =>
            {
                error.Run(async context =>
                {
                    var builder = new StringBuilder();
                    builder.AppendLine("<html><body>");
                    builder.AppendLine("An error occurred, Status Code: " +
                        WebUtility.HtmlEncode(context.Request.Path.ToString().Substring(1)) + "<br />");
                    var referrer = context.Request.Headers["referer"];
                    if (!string.IsNullOrEmpty(referrer))
                    {
                        builder.AppendLine("Return to <a href=\"" + WebUtility.HtmlEncode(referrer) + "\">" +
                            WebUtility.HtmlEncode(referrer) + "</a><br />");
                    }
                    builder.AppendLine("</body></html>");
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync(builder.ToString());
                });
            });
        }
    }
}
