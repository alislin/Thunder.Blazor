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
        bool IsVisabled { get; set; }
        int ServiceIndex { get; set; }

        void LoadItem(object item);
        void ShowItem(object item);
        void CloseItem(object item);
        void Cancel();
    }

    public enum PageType
    {
        Default,
        Modal,
        Tab,
        Alert,
        Notify,
        Block,
        Custom
    }
}
