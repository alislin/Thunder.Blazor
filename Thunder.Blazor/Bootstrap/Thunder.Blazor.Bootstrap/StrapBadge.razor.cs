/* Ceated by Ya Lin. 2019/8/5 14:28:08 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Components;
using Thunder.Blazor.Extensions;

namespace Thunder.Blazor.Bootstrap
{
    public class StrapBadgeBase : TComponent<TagBlockContext>
    {
        [Parameter] public bool Rounded { get; set; }
        [Parameter] public StyleType Style { get; set; }
        public override string CssStyle => ComponentType.badge
            .ToDescriptionString()
            .CssBuild(ComponentType.badge
            .CssBuild(Style));

        public string ClassStyle { get; set; }
    }
}
