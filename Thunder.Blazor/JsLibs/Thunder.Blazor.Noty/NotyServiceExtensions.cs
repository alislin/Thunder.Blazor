using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Thunder.Blazor.Services
{
    public static class NotyServiceExtensions
    {
        public static IServiceCollection AddAnimateScoped(this IServiceCollection services)
        {
            services.TryAddScoped<NotifyService>();
            return services;
        }
    }

}
