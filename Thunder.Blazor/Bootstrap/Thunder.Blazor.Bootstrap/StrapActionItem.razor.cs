/* Ceated by Ya Lin. 2019/8/8 11:47:15 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Components;
using Thunder.Blazor.Libs;
using Thunder.Blazor.Models;

namespace Thunder.Blazor.Bootstrap
{
    public class StrapActionItemBase : TComponent<TagBlockContext>
    {
        [Parameter] public StyleType Style { get; set; }
        [Parameter] public ActionItemTag ActionItemTag { get; set; }
        /// <summary>
        /// 委托（不为空时优先使用）
        /// </summary>
        [Parameter] public ContextAction ContextAction { get; set; }

        protected override void StyleBuild(CssBuild cssBuilder)
        {
            cssBuilder.Add("active", IsActived)
                .Add("disabled", ActionItemTag == ActionItemTag.a && !IsEnabled);
        }

        protected void UpdateContext()
        {
            if (ContextAction==null)
            {
                return;
            }

            Caption = ContextAction.Text;
            CommandAction = ContextAction.Action;
        }
    }
}
