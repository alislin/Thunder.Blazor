/* Ceated by Ya Lin. 2019/8/5 14:28:08 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Components;
using Thunder.Blazor.Extensions;
using Thunder.Blazor.Libs;

namespace Thunder.Blazor.Bootstrap
{
    public class StrapBadgeBase : TComponent2<TagBlockContext>
    {
        [Parameter] public bool Rounded { get; set; }
        [Parameter] public StyleType Style { get; set; }
        
        protected override void StyleBuild(CssBuild cssBuilder)
        {
            cssBuilder.Add(ComponentType.badge)
                .Add(ComponentType.badge.CssJoin(Style))
                .Add("badge-pill", Rounded);
        }
    }
}
