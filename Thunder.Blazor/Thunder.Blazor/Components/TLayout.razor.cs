/* Ceated by Ya Lin. 2019/7/31 16:26:44 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Services;

namespace Thunder.Blazor.Components
{
    public class TLayoutBase: LayoutComponentBase
    {
        [Inject] public DomService DomService { get; set; }
        [Inject] public ComponentService ComponentService { get; set; }
    }
}
