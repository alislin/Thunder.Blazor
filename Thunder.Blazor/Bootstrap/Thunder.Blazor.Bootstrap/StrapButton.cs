/* Ceated by Ya Lin. 2019/8/5 16:32:13 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Components;
using Thunder.Blazor.Extensions;
using Thunder.Blazor.Libs;

namespace Thunder.Blazor.Bootstrap
{
    public class StrapButton: StrapActionItem
    {
        /// <summary>
        /// 外框线样式
        /// </summary>
        [Parameter] public bool Outline { get; set; }
        /// <summary>
        /// 尺寸
        /// </summary>
        [Parameter] public SizeType Size { get; set; }
        /// <summary>
        /// 禁用按钮样式
        /// </summary>
        [Parameter] public bool DisableButtonStyle { get; set; }

        protected override void StyleBuild(CssBuild cssBuilder)
        {
            if (!DisableButtonStyle)
            {
                var btn = ComponentType.button;
                cssBuilder.Add(btn)
                    .Add(btn.CssJoin(Style), !Outline)
                    .Add(btn.CssJoin(OutlineType.outline, Style), Outline)
                    .Add(btn.CssJoin(Size), !string.IsNullOrWhiteSpace(Size.ToDescriptionString()));
            }

            base.StyleBuild(cssBuilder);
        }
    }
}
