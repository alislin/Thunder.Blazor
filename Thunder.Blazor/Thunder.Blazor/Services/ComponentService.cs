using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thunder.Blazor.Components;

namespace Thunder.Blazor.Services
{
    /// <summary>
    /// 组件服务(含Js调用)
    /// </summary>
    public class ComponentService :IDisposable
    {
        public ComponentService(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
        }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }
        public List<Action> BlockContextCloseAction { get; set; } = new List<Action>();

        public async Task CloseBlockContext()
        {
            foreach (var item in BlockContextCloseAction)
            {
                item?.Invoke();
            }
        }

        public virtual void Dispose()
        {
            
        }
    }

    public static class ComponentServiceExtentsion
    {
        public static IServiceCollection AddDomServiceScoped(this IServiceCollection services)
        {
            services.TryAddScoped<ComponentService>();
            return services;
        }
    }
}
