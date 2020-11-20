namespace Thunder.Blazor.Models
{
    public class EventData
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 事件数据
        /// </summary>
        public object Data { get; set; }
    }
}
