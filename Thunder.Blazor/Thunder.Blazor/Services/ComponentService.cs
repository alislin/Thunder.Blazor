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
        protected List<ActionStack> BlockContextCloseAction { get; set; } = new List<ActionStack>();

        /// <summary>
        /// 添加ACTION
        /// </summary>
        /// <param name="type"></param>
        /// <param name="action"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public void AddAction(string type,Action action,Guid? id = null)
        {
            var idv = id ?? Guid.NewGuid();
            BlockContextCloseAction.Add(new ActionStack { Action = action, Type = type, Id = idv });
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public void DoAction(string type=null)
        {
            var all = string.IsNullOrWhiteSpace(type);
            var list = BlockContextCloseAction.Where(x => all || x.Type == type);
            foreach (var item in list)
            {
                item?.Action?.Invoke();
            }
            var idlist = list.Select(x => x.Id);
            BlockContextCloseAction.RemoveAll(x => idlist.Contains(x.Id));
        }

        public virtual void Dispose()
        {
            
        }
    }

    /// <summary>
    /// 待处理ACTION
    /// </summary>
    public class ActionStack
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public Action Action { get; set; }
    }

    public static class ComponentServiceExtentsion
    {
        public static IServiceCollection AddComponentServiceScoped(this IServiceCollection services)
        {
            services.TryAddScoped<ComponentService>();
            return services;
        }
    }
}
