using Microsoft.AspNetCore.Components;
using System;
using Thunder.Blazor.Components;
using Thunder.Blazor.Extensions;
using Thunder.Blazor.Libs;

namespace Thunder.Blazor.Bootstrap
{
    public class StrapPaginationBase : TPaginationBase<StrapPaginationItem>
    {
        [Parameter] public SizeType Size { get; set; }
        [Parameter] public AlignmentType Alignment { get; set; }

        protected override void StyleBuild(CssBuild cssBuilder)
        {
            var key = "pagination";
            cssBuilder.Add(key)
                .Add("-".Join(key, Size.ToDescriptionString()))
                .Add("-".Join("justify", "content", Alignment.ToDescriptionString()));

            base.StyleBuild(cssBuilder);
        }
    }

    public class StrapPaginationItem : TPaginationItem
    {
        public override Type ContextType => typeof(StrapPagination);
    }
}
