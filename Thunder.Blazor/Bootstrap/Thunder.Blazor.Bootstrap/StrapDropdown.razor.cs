/* Ceated by Ya Lin. 2019/8/7 15:14:06 */

using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Components;
using Thunder.Blazor.Libs;

namespace Thunder.Blazor.Bootstrap
{
    public class StrapDropdownBase :TBlockDropdownBase<StrapDropdownMenuItem>
    {
        
        protected override void StyleBuild(CssBuild cssBuilder)
        {
            cssBuilder.Add("dropdown");
        }
    }

    public class StrapDropdownMenuItem : TNode<StrapDropdownMenuItem>
    {
        /// <summary>
        /// 独立的下拉按钮
        /// </summary>
        public bool Split { get; set; }
    }
}
