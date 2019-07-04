using Microsoft.AspNetCore.Components;
using System;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// 组件构造数据
    /// </summary>
    public class ComponentContent
    {
        public ComponentContent()
        {
        }

        public ComponentContent(object obj,string paraName,object paraData)
        {
            var contentType = obj.GetType();
            var parameters = new ComponentParamenter(paraName, paraData);
            Parameters = parameters;
            ContentType = contentType;
        }

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
