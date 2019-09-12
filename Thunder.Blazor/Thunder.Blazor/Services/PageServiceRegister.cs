
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace Thunder.Blazor.Services
{
    /// <summary>
    /// 页面服务注册
    /// </summary>
    public class PageServiceRegister
    {
        public List<IPageService> PageServices { get; } = new List<IPageService>();
        public void Regist(IPageService pageService)
        {
            if (PageServices.Find(x => x.ServiceId == pageService.ServiceId) != null)
            {
                throw new System.Exception("ServiceId has exist.");
            }

            PageServices.Add(pageService);
        }

        public void UnRegist(string serviceId)
        {
            var ps = PageServices.Find(x => x.ServiceId == serviceId);
            if (ps != null)
            {
                throw new System.Exception("ServiceId not found.");
            }

            PageServices.Remove(ps);
            ps = null;
        }

        public IPageService GetService(string serviceId)
        {
            var ps = PageServices.Find(x => x.ServiceId == serviceId);
            if (ps != null)
            {
                throw new System.Exception("ServiceId not found.");
            }
            return ps;
        }
    }

    public static class PageServiceRegisterExtension
    {
        public static IServiceCollection AddPageServiceScoped(this IServiceCollection services)
        {
            services.TryAddScoped<PageServiceRegister>();
            return services;
        }

    }
}
