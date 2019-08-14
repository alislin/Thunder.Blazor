/* Ceated by Ya Lin. 2019/8/8 11:47:15 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Components;
using Thunder.Blazor.Libs;

namespace Thunder.Blazor.Bootstrap
{
    public class StrapActionItemBase : TComponent<TagBlockContext>
    {
        [Parameter] public StyleType Style { get; set; }
        [Parameter] public ActionItemTag ActionItemTag { get; set; }

        protected override void StyleBuild(CssBuild cssBuilder)
        {
            cssBuilder.Add("active", IsActived)
                .Add("disabled", ActionItemTag == ActionItemTag.a && !IsEnabled);
        }
    }
}
