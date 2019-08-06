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
    public class StrapButtonBase: TComponent<TagBlockContext>
    {
        [Parameter] public StyleType Style { get; set; }
        [Parameter] public bool Outline { get; set; }
        [Parameter] public SizeType Size { get; set; }
        [Parameter] public ButtonTag ButtonTag { get; set; }

        public override string CssStyle => GetCss();

        private string GetCss()
        {
            var btn = ComponentType.button;
            var css = CssBuild.New
                .Add(btn)
                .Add(btn.Css(Style),!Outline)
                .Add(btn.Css(OutlineType.outline,Style), Outline)
                .Add(btn.Css(Size),string.IsNullOrWhiteSpace(Size.ToDescriptionString()))
                .Add("active",IsActived)
                .Add(StyleClass);

            return css.CssString;
        }
    }
}
