/* Ceated by Ya Lin. 2019/8/8 11:40:33 */

using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Libs;

namespace Thunder.Blazor.Bootstrap
{
    public class StrapDropdownItem:StrapActionItem
    {
        protected override void StyleBuild(CssBuild cssBuilder)
        {
            cssBuilder.Add("dropdown-item");
            base.StyleBuild(cssBuilder);
        }
    }
}
