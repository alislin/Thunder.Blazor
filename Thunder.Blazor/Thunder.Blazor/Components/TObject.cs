namespace Thunder.Blazor.Components
{
    public class TObject : IThunderObject
    {
        public string ObjectName { get; set; }
    }

    public class TBlock : TObject
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 是否可见
        /// </summary>
        public bool Visabled { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool Actived { get; set; }
    }
}
