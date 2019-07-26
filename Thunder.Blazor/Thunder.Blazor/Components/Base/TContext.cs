/* Ceated by Ya Lin. 2019/7/11 14:52:29 */

using Microsoft.AspNetCore.Components;
using System;
using Thunder.Blazor.Models;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// 组件数据 (ViewModel)
    /// </summary>
    public  class TContext: IThunderObject, IVisual, IBaseBehaver
    {
        /// <summary>
        /// 说明文字
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// 组件类型
        /// </summary>
        public virtual Type ContextType { get;  set; }
        /// <summary>
        /// 子组件数据
        /// </summary>
        public TContext Child { get; set; }

        /// <summary>
        /// 对象名称
        /// </summary>
        public string ObjectName { get; set; }
        /// <summary>
        /// 自定义对象
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// 背景
        /// </summary>
        public string Backgroud { get; set; }
        /// <summary>
        /// 字体颜色
        /// </summary>
        public string FontColor { get; set; }
        /// <summary>
        /// 尺寸
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 样式
        /// </summary>
        public string StyleClass { get; set; }
        /// <summary>
        /// 是否可见
        /// </summary>
        public bool IsVisabled { get; set; } = true;
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActived { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsEnabled { get; set; } = true;
        /// <summary>
        /// 操作指令
        /// </summary>
        public Action CommandAction { get; set; }

        /// <summary>
        /// 组件参数(级联传入)
        /// </summary>
        public ComponentParamenter ContextParameters => GetParamenter();
        /// <summary>
        /// 生成区块
        /// </summary>
        public RenderFragment ContextFragment => new RenderFragment(x => { x.OpenComponent(1, ContextType); x.CloseComponent(); });
        /// <summary>
        /// 状态已改变
        /// </summary>
        public Action StateHasChanged { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName => this.GetType().FullName;

        /// <summary>
        /// 自动生成参数
        /// </summary>
        /// <returns></returns>
        private ComponentParamenter GetParamenter()
        {
            var p = new ComponentParamenter(TypeName, this);
            return p;
        }
    }


    public static class TContextExt
    {
        public static TModel ToViewModel<TModel, TView>(this TModel model) where TModel : TContext
        {
            model.ContextType = typeof(TView);
            return model;
        }
    }

    /// <summary>
    /// 指定前端对象
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public class TContext<TView> : TContext
    {
        public override Type ContextType => typeof(TView);
    }

    public class TContainer : TContext, IBehaver
    {

        /// <summary>
        /// 加载
        /// </summary>
        public Action Load { get; set; }
        /// <summary>
        /// 显示 / 激活
        /// </summary>
        public Action Show { get; set; }
        /// <summary>
        /// 关闭
        /// </summary>
        public Action Close { get; set; }
        /// <summary>
        /// 加载前
        /// </summary>
        public EventHandler OnLoading { get; set; }
        /// <summary>
        /// 显示前
        /// </summary>
        public EventHandler OnShowing { get; set; }
        /// <summary>
        /// 关闭前
        /// </summary>
        public EventHandler OnClosing { get; set; }
        /// <summary>
        /// 加载后
        /// </summary>
        public EventHandler OnLoaded { get; set; }
        /// <summary>
        /// 显示后
        /// </summary>
        public EventHandler OnShowed { get; set; }
        /// <summary>
        /// 关闭后
        /// </summary>
        public EventHandler OnClosed { get; set; }
        /// <summary>
        /// 操作指令
        /// </summary>
        public EventHandler<ContextResult> OnCommand { get; set; }
    }

    /// <summary>
    /// 容器组件
    /// </summary>
    public abstract class TContainer<TModel> : TContainer, IBehaver<TModel>
    {
        /// <summary>
        /// 加载
        /// </summary>
        public Action<TModel> LoadItem { get; set; }
        /// <summary>
        /// 显示 / 激活
        /// </summary>
        public Action<TModel> ShowItem { get; set; }
        /// <summary>
        /// 关闭
        /// </summary>
        public Action<TModel> CloseItem { get; set; }
    }

    public class TContainer<TModel, TView> : TContainer<TModel>
    {
        public override Type ContextType => typeof(TView);
    }
}
