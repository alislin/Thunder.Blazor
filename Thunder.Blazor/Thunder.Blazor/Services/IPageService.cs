using Thunder.Blazor.Components;

namespace Thunder.Blazor.Services
{
    /// <summary>
    /// 页面服务接口
    /// </summary>
    public interface IPageService
    {
        string ServiceId { get; set; }
        string PageType { get; set; }

        void LoadItem(object item);
        void ShowItem(object item);
        void CloseItem(object item);
        void Cancel();
    }

    public enum PageTypes
    {
        Default,
        Modal,
        Tab,
        Alert,
        Notify,
        Custom
    }
}
