using Blazor_GetStarted_ClientSide_V3.Interfaces;
using Blazor_GetStarted_ClientSide_V3.Models;
using Blazor_GetStarted_ClientSide_V3.Services;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor_GetStarted_ClientSide_V3
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IBudgetService, BudgetService>();
            services.AddSingleton<ToastService>();
            services.AddSingleton<AppState>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
