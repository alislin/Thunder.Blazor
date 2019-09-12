namespace Thunder.Blazor.Services
{
    /// <summary>
    /// 页面服务接口
    /// </summary>
    public interface IPageService
    {
        string ServiceId { get; set; }
        void Show(object item = null);
        void Cancel(object item = null);
        void Close(object item = null);
        void Load(object item = null);
    }
}
