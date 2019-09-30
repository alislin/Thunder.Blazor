using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thunder.Blazor.Extensions;

namespace Thunder.Blazor.Services
{
    /// <summary>
    /// 组件服务(含Js调用)
    /// </summary>
    public class ComponentService : IDisposable
    {
        public event EventHandler<string> OnMessage;

        public ComponentService(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
        }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        /// <summary>
        /// 页面组件服务
        /// </summary>
        public List<IPageService> PageServices { get; } = new List<IPageService>();

        #region Dispose接口
        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region 关闭组件队列
        /// <summary>
        /// 等待关闭状态的组件
        /// </summary>
        protected List<ActionStack> BlockContextCloseAction { get; } = new List<ActionStack>();

        /// <summary>
        /// 添加ACTION
        /// </summary>
        /// <param name="type"></param>
        /// <param name="action"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public void AddAction(string type, Action action, Guid? id = null)
        {
            var idv = id ?? Guid.NewGuid();
            BlockContextCloseAction.Add(new ActionStack { Action = action, Type = type, Id = idv });
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public void DoAction(string type = null)
        {
            var all = string.IsNullOrWhiteSpace(type);
            var list = BlockContextCloseAction.Where(x => all || x.Type == type);
            foreach (var item in list)
            {
                item?.Action?.Invoke();
            }
            var idlist = list.Select(x => x.Id);
            BlockContextCloseAction.RemoveAll(x => idlist.Contains(x.Id));
        }
        #endregion

        #region 页面组件方法

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="pageService"></param>
        public void Regist(IPageService pageService)
        {
            pageService.NullCheck();
            if (PageServices.FirstOrDefault(x => x.ServiceId == pageService.ServiceId) != null)
            {
                var log = $"ServiceId[{pageService.ServiceId} / {pageService.PageType}] has exist.";
                Log(log);
                return;
            }

            PageServices.Add(pageService);

#if DEBUG
            Console.WriteLine($"Regist ServiceId[{pageService.ServiceId} / {pageService.PageType}]");
#endif
        }

        /// <summary>
        /// 移除注册
        /// </summary>
        /// <param name="serviceId"></param>
        public void UnRegist(string serviceId)
        {
            var ps = PageServices.FirstOrDefault(x => x.ServiceId == serviceId);
            if (ps == null)
            {
                var log = $"ServiceId[{serviceId}] not found.";
                Log(log);
                return;
            }

            PageServices.Remove(ps);
#if DEBUG
            Console.WriteLine($"Unregist ServiceId[{serviceId}]");
#endif
        }

        /// <summary>
        /// 获取组件
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public IPageService GetService(string serviceId)
        {
            var ps = PageServices.FirstOrDefault(x => x.ServiceId == serviceId);
            if (ps == null)
            {
                var log = $"ServiceId[{serviceId}] not found.";
                Log(log);
                return ps;
            }
            return ps;
        }

        /// <summary>
        /// 获取组件,按照组件类型获取一个默认组件
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IPageService Get(PageType type)
        {
            return Get(type.ToString());
        }

        /// <summary>
        /// 获取组件,按照组件类型获取一个默认组件
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IPageService Get(string type)
        {
            var ps = PageServices.FirstOrDefault(x => type == PageType.Default.ToString() || x.PageType == type);
            if (ps == null)
            {
                var log = $"PageService (type:{type}) not found.";
                Log(log);
                return ps;
            }
            return ps;
        }

        #endregion

        #region 消息事件
        public async void SendMessage(string msg)
        {
            await Task.Run(() => { OnMessage?.Invoke(this, msg); });
        }
        #endregion

        private static void Log(string log,bool throwFlag=false)
        {
#if DEBUG
            if (throwFlag)
            {
                throw new System.Exception(log);
            }
            else
            {
                Console.WriteLine(log);
            }
#else
                Console.WriteLine(log);
#endif
        }

    }

    /// <summary>
    /// 待处理ACTION
    /// </summary>
    public class ActionStack
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public Action Action { get; set; }
    }

    public static class ComponentServiceExtentsion
    {
        public static IServiceCollection AddComponentServiceScoped(this IServiceCollection services)
        {
            services.TryAddScoped<ComponentService>();
            return services;
        }
    }
}
