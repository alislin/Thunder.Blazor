using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Models;

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
        /// <summary>
        /// 自定义对象
        /// </summary>
        object Tag { get; set; }
    }

    /// <summary>
    /// 节点接口
    /// </summary>
    public interface INode<TModel>
    {
        /// <summary>
        /// 父节点
        /// </summary>
        TModel ParentNode { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        IList<TModel> ChildNodes { get; set; }
    }
    /// <summary>
    /// 基础行为
    /// </summary>
    public interface IBaseBehaver
    {
        /// <summary>
        /// 是否可见
        /// </summary>
        bool IsVisabled { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        bool IsActived { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        bool IsEnabled { get; set; }
        /// <summary>
        /// 操作指令
        /// </summary>
        Action<object> CommandAction { get; set; }
    }

    /// <summary>
    /// 组件行为
    /// </summary>
    public interface IBehaverComponent
    {
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
        /// <summary>
        /// 操作指令
        /// </summary>
        EventHandler<ContextResult> OnCommand { get; set; }
        /// <summary>
        /// 加载
        /// </summary>
        void Load();
        /// <summary>
        /// 显示 / 激活
        /// </summary>
        void Show();
        /// <summary>
        /// 关闭
        /// </summary>
        void Close();
    }

    public interface IBehaver
    {
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

    }

    /// <summary>
    /// 行为接口
    /// </summary>
    public interface IBehaver<T>: IBehaver
    {
        /// <summary>
        /// 加载
        /// </summary>
        Action<T> LoadItem { get; set; }
        /// <summary>
        /// 显示 / 激活
        /// </summary>
        Action<T> ShowItem { get; set; }
        /// <summary>
        /// 关闭
        /// </summary>
        Action<T> CloseItem { get; set; }

    }

    /// <summary>
    /// 可视化接口
    /// </summary>
    public interface IVisual
    {
        Action StateHasChanged { get; }
    }

    /// <summary>
    /// 容器
    /// </summary>
    public interface IContainer
    {
        TComponent Content { get; set; }
    }

    /// <summary>
    /// CSS 动画
    /// </summary>
    public interface IAnimate
    {
        /// <summary>
        /// 启用动画
        /// </summary>
        bool AnimateEnabled { get; set; }
        /// <summary>
        /// 进入动画
        /// </summary>
        string AnimateEnter { get; set; }
        /// <summary>
        /// 退出动画
        /// </summary>
        string AnimateExit { get; set; }
    }

    /// <summary>
    /// 附加信息
    /// </summary>
    public interface IAttachment
    {
        /// <summary>
        /// 附加信息
        /// </summary>
        string AttachmentInfo { get; set; }
        /// <summary>
        /// 标注信息
        /// </summary>
        string BadgeInfo { get; set; }
    }
}
