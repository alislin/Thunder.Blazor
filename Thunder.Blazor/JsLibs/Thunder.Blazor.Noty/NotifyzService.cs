using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Thunder.Blazor.Extensions;

namespace Thunder.Blazor.Services
{
    /// <summary>
    /// 提示消息服务（使用 Jgrowl）
    /// </summary>
    /// <remarks>
    /// 包含文件：
    /// noty.app.js
    /// jquery.min.js
    /// plugins/jgrowl.min.js
    /// plugins/noty.min.js
    /// 在 index.html head 中添加下面代码
    /// <script src="assets/js/jquery.min.js"></script>
    /// <script src="assets/js/plugins/jgrowl.min.js"></script>
    /// <script src="assets/js/plugins/noty.min.js"></script>
    /// <script src="assets/js/noty.app.js"></script>
    /// </remarks>
    public class NotifyService
    {
        public NotifyService()
        {
        }

        public NotifyService(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
        }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }
        public NotifyConfiguration Config { get; set; }

        public async Task Show(NotyData msg)
        {
            await JsRuntime.InvokeAsync<object>("NotyJgrowl.show", msg);
        }

        /// <summary>
        /// Http状态检查
        /// </summary>
        /// <param name="code">HttpStatusCode</param>
        public async void CheckHttpStatusCode(int code)
        {
            if (code>=400)
            {
                var msg = $"请求失败！状态代码：{code} / {((HttpStatusCode)code).ToString()}";
                if (code == 999)
                {
                    msg = "请求失败！网络连接异常。";
                }
                await Show(new NotyData { text = msg, NotyType = NotyType.error,timeout=0 });
            }
        }
    }

    public class NotyData
    {
        public string text { get; set; }
        public int timeout { get; set; } = 2500;
        public string type => NotyType.ToString();
        public string layout => NotyLayout.ToString();
        public string theme => NotyTheme.ToDescriptionString();
        public NotyTheme NotyTheme { get; set; } = NotyTheme.bootstrap_v4;
        public NotyType NotyType { get; set; } = NotyType.info;
        public NotyLayout NotyLayout { get; set; } = NotyLayout.topRight;
    }

    public enum NotyDataType
    {
        error,
        success,
        warning,
        info,
        alert,
    }

    public enum NotyTheme
    {
        [Description("mint")]
        mint,
        [Description("sunset")]
        sunset,
        [Description("relax")]
        relax,
        [Description("nest")]
        nest,
        [Description("metroui")]
        metroui,
        [Description("semanticui")]
        semanticui,
        [Description("light")]
        light,
        [Description("bootstrap-v3")]
        bootstrap_v3,
        [Description("bootstrap-v4")]
        bootstrap_v4
    }

    public enum NotyType
    {
        alert, success, warning, error, info
    }

    public enum NotyLayout
    {
        top, topLeft, topCenter, topRight, center, centerLeft, centerRight, bottom, bottomLeft, bottomCenter, bottomRight
    }

    public class NotifyConfiguration
    {

    }

}
