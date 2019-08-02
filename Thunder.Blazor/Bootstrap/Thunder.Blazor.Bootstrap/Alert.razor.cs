/* Ceated by Ya Lin. 2019/7/31 17:19:44 */

using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Components;

namespace Thunder.Blazor.Bootstrap
{
    public class AlertBase:TAlert<TAlertContent>
    {
        public void Close()
        {

        }
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
