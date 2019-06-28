using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// 子组件基类
    /// </summary>
    public class ComponentChild: ComponentBase,IDisposable
    {
        /// <summary>
        /// 级联参数（父组件传入参数）
        /// </summary>
        [CascadingParameter] protected ComponentParamenter Paramenters { get; set; }
        [Parameter] protected RenderFragment ChildContent { get; set; }
        [Parameter] protected string Name { get; set; }
        [Parameter] protected EventCallback OnClick { get; set; }
        [Parameter] protected EventCallback OnClose { get; set; }

        protected bool HasParamenters => Paramenters != null;

        public virtual void Dispose()
        {
        }
    }

    public abstract class ComponentChild<T> : ComponentChild where T:new()
    {
        [Parameter] protected T Value { get; set; } = new T();
    }

}
