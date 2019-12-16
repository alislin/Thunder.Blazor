/* Ceated by Ya Lin. 2019/11/29 13:34:52 */

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thunder.Blazor.Components.Base
{
    public class DynamicElement: ComponentBase
    {
        /// <summary>
        /// 元素标签
        /// </summary>
        [Parameter] public string TagType { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            builder.OpenElement(0, TagType);
            builder.AddContent(3, ChildContent);
            builder.CloseElement();
        }
    }
}
