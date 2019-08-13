/* Ceated by Ya Lin. 2019/7/31 17:19:44 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Components;
using Thunder.Blazor.Extensions;
using Thunder.Blazor.Libs;

namespace Thunder.Blazor.Bootstrap
{
    public class StrapAlertBase : TAlert<StrapAlertContext>
    {
        protected override void StyleBuild(CssBuild cssBuilder)
        {
            cssBuilder.Add(ComponentType.alert)
                .Add("-".Join(ComponentType.alert.ToDescriptionString(), (DataContext?.Style ?? StyleType.danger).ToDescriptionString()));
        }
    }

    public class StrapAlertContext : TAlertContext
    {
        public StyleType Style { get; set; } = StyleType.danger;
        public string Text { get; set; }
    }
}
