/* Ceated by Ya Lin. 2019/8/21 11:53:05 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Components;
using Thunder.Blazor.Extensions;
using Thunder.Blazor.Libs;

namespace Thunder.Blazor.Bootstrap
{
    public class StrapModalBase:TModal<TModalContext>
    {
        [Parameter] public SizeType Size { get; set; }
        protected string ModalTypeCss => GetModalTypeCss();

        private string GetModalTypeCss()
        {
            var key = "modal";
            return CssBuild.New
                .Add("modal-dialog")
                .Add("-".Join(key, Size.ToDescriptionString()))
                .Build()
                .CssString;
        }

        protected override void StyleBuild(CssBuild cssBuilder)
        {
            base.StyleBuild(cssBuilder);
            cssBuilder.Add("modal");
        }
    }
}
