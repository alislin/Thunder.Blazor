/* Ceated by Ya Lin. 2019/8/21 14:52:54 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Components;
using Thunder.Blazor.Extensions;
using Thunder.Blazor.Libs;

namespace Thunder.Blazor.Bootstrap
{
    public class StrapProgressBase: TProgress
    {
        [Parameter] public StyleType Style { get; set; }
        [Parameter] public bool ShowValue { get; set; }
        [Parameter] public bool ShowAnimate { get; set; }
        [Parameter] public int Height { get; set; }
        [Parameter] public bool IsStriped { get; set; }

        protected string CssHeight => Height > 0 ? $"height: {Height}px;" : "";
        protected string ProgressPercent => GetProgressPercent();
        protected bool ShowLabel => !string.IsNullOrWhiteSpace(Caption);
        protected string CssProgress => GetCssProgress();

        private string GetCssProgress()
        {
            var bg = "-".Join("bg", Style.ToDescriptionString());
            return CssBuild.New
                .Add("progress-bar")
                .Add("progress-bar-striped", IsStriped)
                .Add("progress-bar-animated", ShowAnimate)
                .Add(bg)
                .Build().CssString;
        }

        private new string GetProgressPercent()
        {
            var p = base.GetProgressPercent();
            return $"{p.ToString("F1")}%";
        }

        protected override void StyleBuild(CssBuild cssBuilder)
        {
            cssBuilder.Add("progress");
        }
    }
}
