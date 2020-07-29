/* Ceated by Ya Lin. 2019/8/21 11:53:05 */

using Microsoft.AspNetCore.Components;
using Thunder.Blazor.Components;
using Thunder.Blazor.Extensions;
using Thunder.Blazor.Libs;
using Thunder.Blazor.Models;

namespace Thunder.Blazor.Bootstrap
{
    public class StrapModalBase : TModal<TModalContext>
    {
        [Parameter] public SizeType Size { get; set; }
        protected string ModalTypeCss => GetModalTypeCss();

        private string GetModalTypeCss()
        {
            Size = View.SizeEnum switch
            {
                SizeEnum.Small => SizeType.small,
                SizeEnum.Mini => SizeType.small,
                SizeEnum.Large => SizeType.large,
                SizeEnum.Huge => SizeType.large,
                _ => SizeType.normal
            };
            var key = "modal";
            return CssBuild.New
                .Add("modal-dialog")
                .Add("modal-dialog-scrollable")
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
