using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thunder.Blazor.Libs;

namespace Thunder.Blazor.Services
{
    public class CssBuilderService
    {
        public CssBuilderService(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
        }

        [Inject] public IJSRuntime JsRuntime { get; set; }

        /// <summary>
        /// 设置Dom元素的CSS样式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Update(string id,CssBuild css)
        {
            // 加载 DOMTokenList
            var src = await JsRuntime.InvokeAsync<CssData>("ThunderBlazor.CssBuilder.ClassList", id);
            css.Build();
            // 处理移除项目
            var remove = css.CssRemove.Intersect(src.list).ToList();
            await JsRuntime.InvokeAsync<object>("ThunderBlazor.CssBuilder.Remove", new CssData { id = id, list = remove });
            // 处理添加项目
            await JsRuntime.InvokeAsync<object>("ThunderBlazor.CssBuilder.Add", new CssData { id = id, list = css.CssList });
        }
    }

    public class CssData
    {
        public string id { get; set; }
        public List<string> list { get; set; }
    }

    public static class CssBuilderServiceExtentsion
    {
        public static IServiceCollection AddCssBuilderScoped(this IServiceCollection services)
        {
            services.TryAddScoped<CssBuilderService>();
            return services;
        }
    }

}
