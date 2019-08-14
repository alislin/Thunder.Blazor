/* Ceated by Ya Lin. 2019/8/5 16:36:31 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Components;

namespace Thunder.Blazor.Bootstrap
{
    public class BootStrapBase: TComponent<TagBlockContext>
    {
        [Parameter] public StyleType Style { get; set; }
    }
}
