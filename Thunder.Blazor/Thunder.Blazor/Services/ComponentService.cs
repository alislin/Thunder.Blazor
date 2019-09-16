﻿using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Thunder.Blazor.Services
{
    /// <summary>
    /// 组件服务(含Js调用)
    /// </summary>
    public class ComponentService : IDisposable
    {

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

        public virtual void Dispose()
        {

        }


        #region 关闭组件队列
        /// <summary>
        /// 等待关闭状态的组件
        /// </summary>
        protected List<ActionStack> BlockContextCloseAction { get; set; } = new List<ActionStack>();

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
            if (PageServices.FirstOrDefault(x => x.ServiceId == pageService.ServiceId) != null)
            {
                throw new System.Exception($"ServiceId[{pageService.ServiceId} / {pageService.PageType}] has exist.");
            }

            PageServices.Add(pageService);
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
                throw new System.Exception($"ServiceId[{serviceId}] not found.");
            }

            PageServices.Remove(ps);
            //ps = null;
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
                throw new System.Exception($"ServiceId[{serviceId}] not found.");
            }
            return ps;
        }

        /// <summary>
        /// 获取组件,按照组件类型获取一个默认组件
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IPageService Get(PageTypes type)
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
            var ps = PageServices.FirstOrDefault(x => type == PageTypes.Default.ToString() || x.PageType == type);
            if (ps == null)
            {
                throw new System.Exception($"PageService (type:{type}) not found.");
            }
            return ps;
        }

        #endregion

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
