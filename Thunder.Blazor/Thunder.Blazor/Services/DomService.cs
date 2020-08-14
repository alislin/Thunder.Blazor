using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thunder.Blazor.Extensions;
using Thunder.Blazor.Libs;

namespace Thunder.Blazor.Services
{
    public class DomService
    {
        public DomService(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
        }

        [Inject] public IJSRuntime JsRuntime { get; set; }

        /// <summary>
        /// 设置Dom元素的CSS样式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task UpdateCss(string id,CssBuild css)
        {
            css.NullCheck();
            // 加载 DOMTokenList
            var src = await JsRuntime.InvokeAsync<CssData>("ThunderBlazor.CssBuilder.ClassList", id).ConfigureAwait(true);
            css.Build();
            // 处理移除项目
            var remove = css.CssRemove.Intersect(src.list).ToList();
            await JsRuntime.InvokeAsync<object>("ThunderBlazor.CssBuilder.Remove", new CssData { id = id, list = remove }).ConfigureAwait(true);
            // 处理添加项目
            await JsRuntime.InvokeAsync<object>("ThunderBlazor.CssBuilder.Add", new CssData { id = id, list = css.CssList }).ConfigureAwait(true);
        }

        public async Task<Navigator> GetNavigator()
        {
            var src = await JsRuntime.InvokeAsync<Navigator>("ThunderBlazor.App.getNavigator", null).ConfigureAwait(true);
            return src;
        }
        public async Task<Screen> GetScreen()
        {
            var src = await JsRuntime.InvokeAsync<Screen>("ThunderBlazor.App.getScreent", null).ConfigureAwait(true);
            return src;
        }

        public async Task<string> GetTitle()
        {
            var src = await JsRuntime.InvokeAsync<string>("ThunderBlazor.App.getTitle", null).ConfigureAwait(true);
            return src;
        }

        public async Task SetTitle(string title)
        {
            var src = await JsRuntime.InvokeAsync<object>("ThunderBlazor.App.setTitle", title).ConfigureAwait(true);
        }

    }

    public class CssData
    {
        public string id { get; set; }
        public List<string> list { get; set; }
    }

    public class Navigator
    {
        public string appCodeName { get; set; }
        public string appMinorVersion { get; set; }
        public string appName { get; set; }
        public string appVersion { get; set; }
        public string browserLanguage { get; set; }
        public bool cookieEnabled { get; set; }
        public string cpuClass { get; set; }
        public bool onLine { get; set; }
        public string platform { get; set; }
        public string systemLanguage { get; set; }
        public string userAgent { get; set; }
        public string userLanguage { get; set; }
    }

    public class Screen
    {
        public int availHeight { get; set; }
        public int availWidth { get; set; }
        public int bufferDepth { get; set; }
        public int colorDepth { get; set; }
        public int deviceXDPI { get; set; }
        public int deviceYDPI { get; set; }
        public int fontSmoothingEnabled { get; set; }
        public int height { get; set; }
        public int logicalXDPI { get; set; }
        public int logicalYDPI { get; set; }
        public int pixelDepth { get; set; }
        public int updateInterval { get; set; }
        public int width { get; set; }
    }

    public static class DomServiceExtentsion
    {
        public static IServiceCollection AddDomServiceScoped(this IServiceCollection services)
        {
            services.TryAddScoped<DomService>();
            return services;
        }
    }
}
