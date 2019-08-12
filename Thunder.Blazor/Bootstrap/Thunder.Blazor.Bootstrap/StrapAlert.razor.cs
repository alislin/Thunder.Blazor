﻿/* Ceated by Ya Lin. 2019/7/31 17:19:44 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Components;
using Thunder.Blazor.Extensions;

namespace Thunder.Blazor.Bootstrap
{
    public class StrapAlertBase : TAlert<StrapAlertContext>
    {
        public override string CssStyle => ComponentType.alert
            .ToDescriptionString()
            .Css(ComponentType.alert
            .CssJoin(DataContext?.Style ?? StyleType.danger))
            .Css(StyleClass);
    }

    public class StrapAlertContext : TAlertContext
    {
        public StyleType Style { get; set; } = StyleType.danger;
        public string Text { get; set; }
    }
}
