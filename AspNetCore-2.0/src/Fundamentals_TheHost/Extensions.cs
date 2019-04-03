using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamentals_TheHost
{
    public static class Extensions
    {
        public static IHostBuilder UseHostedService<T>(this IHostBuilder hostBuilder) where T : class, IHostedService, IDisposable
        {
            return hostBuilder.ConfigureServices(services => services.AddHostedService<T>());
        }

        public static void ShowConnectionInfoDetails(this HttpContext context)
        {
            var builder = new StringBuilder();
            builder.AppendFormat(context.Connection.ClientCertificate.ToString());
            builder.AppendFormat(context.Connection.LocalIpAddress.ToString());

            context.Response.WriteAsync(builder.ToString());
        }
    }
}
