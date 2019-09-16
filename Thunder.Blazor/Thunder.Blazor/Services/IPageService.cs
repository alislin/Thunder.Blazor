namespace Thunder.Blazor.Services
{
    /// <summary>
    /// 页面服务接口
    /// </summary>
    public interface IPageService
    {
        string ServiceId { get; set; }
        string PageType { get; set; }

        void Show(object item);
        void Cancel();
        void Close(object item);
        void Load(object item);
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
