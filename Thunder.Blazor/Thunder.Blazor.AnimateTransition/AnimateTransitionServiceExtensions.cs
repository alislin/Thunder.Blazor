// <copyright file="AnimateTransitionServiceExtensions.cs" author="linya">
// Create time：       2020/9/10 14:51:58
// </copyright>
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thunder.Blazor.AnimateTransition
{
    public static class AnimateTransitionServiceExtensions
    {
        public static IServiceCollection AddAnimateScoped(this IServiceCollection services)
        {
            services.TryAddScoped<AnimateTransitionService>();
            return services;
        }

    }
}
