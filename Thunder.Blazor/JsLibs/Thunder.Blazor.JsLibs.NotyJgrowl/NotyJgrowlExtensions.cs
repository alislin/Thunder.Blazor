/* Ceated by Ya Lin. 2019/7/16 17:06:15 */

using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Thunder.Blazor.JsLib.NotyJgrowl
{
    public static class NotyJgrowlExtensions
    {
        //public static IServiceCollection AddToaster(this IServiceCollection services, ToasterConfiguration configuration)
        //{
        //    if (configuration == null) throw new ArgumentNullException(nameof(configuration));
        //    services.TryAddScoped<IToaster>(builder => new Toaster(configuration));
        //    return services;
        //}

        //public static IServiceCollection AddToaster(this IServiceCollection services)
        //{
        //    return AddToaster(services, new ToasterConfiguration());
        //}

        //public static IServiceCollection AddToaster(this IServiceCollection services, Action<ToasterConfiguration> configure)
        //{
        //    if (configure == null) throw new ArgumentNullException(nameof(configure));

        //    var options = new ToasterConfiguration();
        //    configure(options);

        //    return AddToaster(services, options);
        //}
    }
}
