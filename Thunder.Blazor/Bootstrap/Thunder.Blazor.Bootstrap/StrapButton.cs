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
        [Parameter] public bool Outline { get; set; }
        [Parameter] public SizeType Size { get; set; }

        protected override void StyleBuild(CssBuild cssBuilder)
        {
            var btn = ComponentType.button;
            cssBuilder.Add(btn)
                .Add(btn.Css(Style), !Outline)
                .Add(btn.Css(OutlineType.outline, Style), Outline)
                .Add(btn.Css(Size), !string.IsNullOrWhiteSpace(Size.ToDescriptionString()));

            base.StyleBuild(cssBuilder);
        }
    }
}
