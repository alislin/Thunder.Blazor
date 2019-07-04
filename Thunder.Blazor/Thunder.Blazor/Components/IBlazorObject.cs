using System;
using System.Collections.Generic;
using System.Text;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// 基础对象
    /// </summary>
    public interface IThunderObject
    {
        /// <summary>
        /// 对象名称
        /// </summary>
        string ObjectName { get; set; }
    }

    /// <summary>
    /// 节点接口
    /// </summary>
    public interface INode
    {
        /// <summary>
        /// 父节点
        /// </summary>
        INode ParentNode { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        Dictionary<string,INode> ChildNodes { get; set; }
    }

    /// <summary>
    /// 行为接口
    /// </summary>
    public interface IBehaver
    {
        /// <summary>
        /// 是否可见
        /// </summary>
        bool Visabled { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        bool Actived { get; set; }

        /// <summary>
        /// 加载
        /// </summary>
        Action Load { get; set; }
        /// <summary>
        /// 显示 / 激活
        /// </summary>
        Action Show { get; set; }
        /// <summary>
        /// 关闭
        /// </summary>
        Action Close { get; set; }

        /// <summary>
        /// 加载前
        /// </summary>
        EventHandler OnLoading { get; set; }
        /// <summary>
        /// 显示前
        /// </summary>
        EventHandler OnShowing { get; set; }
        /// <summary>
        /// 关闭前
        /// </summary>
        EventHandler OnClosing { get; set; }

        /// <summary>
        /// 加载后
        /// </summary>
        EventHandler OnLoaded { get; set; }
        /// <summary>
        /// 显示后
        /// </summary>
        EventHandler OnShowed { get; set; }
        /// <summary>
        /// 关闭后
        /// </summary>
        EventHandler OnClosed { get; set; }
    }

    /// <summary>
    /// 可视化接口
    /// </summary>
    public interface IVisual
    {
        string Backgroud { get; set; }
        string FontColor { get; set; }
        string Size { get; set; }
    }

    /// <summary>
    /// 容器
    /// </summary>
    public interface IContainer
    {
        TComponent Content { get; set; }
    }
}
