using System;
using System.Collections.Generic;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// 基础对象
    /// </summary>
    public class TObject : IThunderObject
    {
        /// <summary>
        /// 对象名称
        /// </summary>
        public string ObjectName { get; set; }
    }

    /// <summary>
    /// 节点对象
    /// </summary>
    public class TNode : TObject, INode
    {
        /// <summary>
        /// 父节点
        /// </summary>
        public INode ParentNode { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        public Dictionary<string, INode> ChildNodes { get; set; }
    }

    /// <summary>
    /// 区块对象
    /// </summary>
    //public class TBlock : TNode, IBehaver
    //{
    //    /// <summary>
    //    /// 内容
    //    /// </summary>
    //    public string Text { get; set; }
    //    /// <summary>
    //    /// 是否可见
    //    /// </summary>
    //    public bool Visabled { get; set; }
    //    /// <summary>
    //    /// 是否激活
    //    /// </summary>
    //    public bool Actived { get; set; }

    //    /// <summary>
    //    /// 加载
    //    /// </summary>
    //    public Action Load { get; set; }
    //    /// <summary>
    //    /// 显示 / 激活
    //    /// </summary>
    //    public Action Show { get; set; }
    //    /// <summary>
    //    /// 关闭
    //    /// </summary>
    //    public Action Close { get; set; }

    //    /// <summary>
    //    /// 加载前
    //    /// </summary>
    //    public EventHandler OnLoading { get; set; }
    //    /// <summary>
    //    /// 显示前
    //    /// </summary>
    //    public EventHandler OnShowing { get; set; }
    //    /// <summary>
    //    /// 关闭前
    //    /// </summary>
    //    public EventHandler OnClosing { get; set; }
    //    /// <summary>
    //    /// 加载后
    //    /// </summary>
    //    public EventHandler OnLoaded  { get; set; }
    //    /// <summary>
    //    /// 显示后
    //    /// </summary>
    //    public EventHandler OnShowed  { get; set; }
    //    /// <summary>
    //    /// 关闭后
    //    /// </summary>
    //    public EventHandler OnClosed { get; set; }
    //}
}
