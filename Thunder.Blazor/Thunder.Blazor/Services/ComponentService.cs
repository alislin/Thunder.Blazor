using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thunder.Blazor.Components;

namespace Thunder.Blazor.Services
{
    /// <summary>
    /// 组件服务
    /// </summary>
    public class ComponentService<T> :IDisposable where T: TContext,new()
    {
        public ComponentService(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
        }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        public Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 加载
        /// </summary>
        public Action<T> Load { get; set; }
        /// <summary>
        /// 显示 / 刷新
        /// </summary>
        public Action<T> Show { get; set; }
        /// <summary>
        /// 关闭
        /// </summary>
        public Action<T> Close { get; set; }
        /// <summary>
        /// 在加载后
        /// </summary>
        public Action<T> OnLoaded { get; set; }
        /// <summary>
        /// 在显示后
        /// </summary>
        public Action<T> OnShowed { get; set; }
        /// <summary>
        /// 在关闭后
        /// </summary>
        public Action<T> OnClosed { get; set; }

        public virtual void Dispose()
        {
            
        }
    }
}
