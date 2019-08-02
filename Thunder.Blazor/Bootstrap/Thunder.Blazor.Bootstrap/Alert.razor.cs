/* Ceated by Ya Lin. 2019/7/31 17:19:44 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Components;

namespace Thunder.Blazor.Bootstrap
{
    public class AlertBase:TAlert<AlertContext>
    {
    }

    public class AlertContext : TAlertContext
    {
        public string Style { get; set; } = (new AlertStyle()).danger;
        public string Text { get; set; }
    }

    public class AlertStyle : IStyleType
    {
        public string primary => "alert-primary";
        public string secondary => "alert-secondary";
        public string success => "alert-success";
        public string danger => "alert-danger";
        public string warning => "alert-warning";
        public string info => "alert-info";
        public string light => "alert-light";
        public string dark => "alert-dark";
    }
}
