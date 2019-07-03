using Blazored.Localisation;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor_GetStarted_ClientSide_Localization_V3
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazoredLocalisation();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.UseBlazoredLocalisation();
            app.AddComponent<App>("app");
        }
    }
}
