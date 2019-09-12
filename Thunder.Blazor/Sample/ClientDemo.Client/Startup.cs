using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Thunder.Blazor.Services;

namespace ClientDemo.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddComponentServiceScoped()
                .AddNotyScoped()
                .AddPageServiceScoped()
                .AddAnimateScoped();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
