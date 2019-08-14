/* Ceated by Ya Lin. 2019/8/8 11:40:33 */

using Thunder.Blazor.Libs;

namespace Thunder.Blazor.Bootstrap
{
    public class StrapNavItem : StrapActionItem
    {
        protected override void StyleBuild(CssBuild cssBuilder)
        {
            cssBuilder.Add("nav-link");
            base.StyleBuild(cssBuilder);
        }
    }
}
