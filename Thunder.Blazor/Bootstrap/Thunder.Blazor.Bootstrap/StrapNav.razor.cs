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
    public class StrapNavBase:TNav
    {
        [Parameter] public AlignmentType Alignment { get; set; }
        [Parameter] public bool IsVertical { get; set; }
        [Parameter] public bool IsTab { get; set; }
        [Parameter] public bool IsPill { get; set; }
        [Parameter] public bool IsFill { get; set; }
        [Parameter] public string Brand { get; set; }

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

        protected Node<TagBlockContext> TryGetDropdown(TagBlockContext item)
        {
            if (IsDropdown(item))
            {
                var v = (Node<TagBlockContext>)item;
                return v;
            }
            return null;
        }

        protected bool IsDropdown(TagBlockContext item)
        {
            Log(item.TypeName);
            return item.TypeName == "StrapDropdownMenuItem" || item.TypeName.Contains("Node");
        }

        protected virtual string GetItemCss(TagBlockContext item)
        {
            return CssBuild.New.Add("nav-item")
                .Add("dropdown", IsDropdown(item))
                .Build().CssString;
        }
    }

}
