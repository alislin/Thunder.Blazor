/* Ceated by Ya Lin. 2019/8/12 16:25:40 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Components;
using Thunder.Blazor.Extensions;
using Thunder.Blazor.Libs;

namespace Thunder.Blazor.Bootstrap
{
    public class StrapNavBase:TComponent<StrapNavItemContext>
    {
        [Parameter] public AlignmentType Alignment { get; set; }
        [Parameter] public bool IsVertical { get; set; }
        [Parameter] public bool IsTab { get; set; }
        [Parameter] public bool IsPill { get; set; }
        [Parameter] public bool IsFill { get; set; }

        protected override void StyleBuild(CssBuild cssBuilder)
        {
            cssBuilder.Add("nav")
                .Add("-".Join("justify", "content", Alignment.ToDescriptionString()))
                .Add("flex-column", IsVertical)
                .Add("nav-tabs", IsTab)
                .Add("nav-pills", IsPill)
                .Add("nav-fill", IsFill);
            base.StyleBuild(cssBuilder);
        }
    }

    public class StrapNavItemContext : TContext
    {
        public List<TagBlockContext> NavItems { get; set; } = new List<TagBlockContext>();
    }
}
