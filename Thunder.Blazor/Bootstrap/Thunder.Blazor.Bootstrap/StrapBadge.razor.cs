﻿/* Ceated by Ya Lin. 2019/8/5 14:28:08 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Components;
using Thunder.Blazor.Extensions;
using Thunder.Blazor.Libs;

namespace Thunder.Blazor.Bootstrap
{
    public class StrapBadgeBase : TComponent<TagBlockContext>
    {
        [Parameter] public bool Rounded { get; set; }
        [Parameter] public StyleType Style { get; set; }
        public override string CssStyle => GetCss();
        
        private string GetCss()
        {
            var css = CssBuild.New
                .Add(ComponentType.badge)
                .Add(ComponentType.badge.Css(Style))
                .Add("badge-pill", Rounded)
                .Add(StyleClass);

            return css.CssString;
        }
    }
}
