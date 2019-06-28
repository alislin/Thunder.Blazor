using Microsoft.AspNetCore.Components;
using System;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// 组件
    /// </summary>
    public class ComponentContent
    {
        /// <summary>
        /// 组件参数
        /// </summary>
        public ComponentParamenter Parameters { get; set; }
        /// <summary>
        /// 组件类型
        /// </summary>
        public Type ContentType { get; set; }
        /// <summary>
        /// 组件渲染块（只读）
        /// </summary>
        public RenderFragment ChildContent => new RenderFragment(x => { x.OpenComponent(1, ContentType); x.CloseComponent(); });
    }


}
