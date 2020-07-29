/* Ceated by Ya Lin. 2019/8/7 14:19:22 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Components;
using Thunder.Blazor.Extensions;
using Thunder.Blazor.Libs;

namespace Thunder.Blazor.Bootstrap
{
    public class StrapButtongroupBase : TComponent2<TagBlockContext>
    {
        [Parameter] public SizeType Size { get; set; }
        [Parameter] public bool Vertical { get; set; }

        protected override void StyleBuild(CssBuild cssBuilder)
        {
            var key = "btn-group";
            cssBuilder.Add(key, !Vertical)
                .Add("-".Join(key, VerticalType.vertical.ToString()), Vertical)
                .Add("-".Join(key, Size.ToDescriptionString()), !string.IsNullOrWhiteSpace(Size.ToDescriptionString()));

        }
    }
}
