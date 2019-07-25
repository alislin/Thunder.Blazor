/* Ceated by Ya Lin. 2019/7/22 16:35:13 */

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Thunder.Blazor.Services
{
    public static class AnimateServiceExtensions
    {
        public static IServiceCollection AddAnimateScoped(this IServiceCollection services)
        {
            services.TryAddScoped<AnimateService>();
            return services;
        }
    }
}
