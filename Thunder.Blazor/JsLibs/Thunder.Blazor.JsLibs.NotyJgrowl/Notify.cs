using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Thunder.Blazor.JsLib.NotyJgrowl
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
    public class Notify
    {
        public Notify()
        {
        }

        public Notify(IJSRuntime jsRuntime)
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
                await Show(new NotyData { text = msg, typesrc = NotyDataType.error,timeout=0 });
            }
        }
    }

    class notifydata
    {
        public bool sticky { get; set; }
        public int corners { get; set; } = 10;
        public string position { get; set; } = "top-right";
        public string theme { get; set; } = "default";
        public string type { get; set; } = "alert";
    }

    public class NotyData
    {
        public string text { get; set; }
        public string type => typesrc.ToString();
        public int timeout { get; set; } = 2500;
        public NotyDataType typesrc { get; set; } = NotyDataType.info;
    }

    public enum NotyDataType
    {
        error,
        success,
        warning,
        info,
        alert,
    }

    public class NotifyConfiguration
    {

    }
}
