/* Ceated by Ya Lin. 2019/7/11 14:52:29 */

using Microsoft.AspNetCore.Components;
using System;
using Thunder.Blazor.Models;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// 组件数据 (ViewModel)
    /// </summary>
    public class TContext : NotifyChanged, IThunderObject, IVisual, IBaseBehaver, IAttachment
    {
        public string DomId { get; set; }
        /// <summary>
        /// 说明文字
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// 组件类型
        /// </summary>
        public virtual Type ContextType { get; set; }
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
        /// 样式
        /// </summary>
        //public string StyleClass { get; set; }
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
        public Action<object> CommandAction { get; set; } = (obj) => { };
        /// <summary>
        /// 关闭时调用
        /// </summary>
        public Action<object> OnClosed { get; set; }

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
        public Action StateHasChanged { get; protected set; } = () => { };
        /// <summary>
        /// Udpate DataContext from view
        /// </summary>
        public Action UpdateDataContext { get; protected set; } = () => { };
        /// <summary>
        /// Load datacontext to view
        /// </summary>
        public Action LoadDataContext { get; protected set; } = () => { };

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName => this.GetType().Name;
        /// <summary>
        /// 参数Key，为空值时默认使用TypeName
        /// </summary>
        public string ParameterKey { get; set; }

        /// <summary>
        /// 附加信息
        /// </summary>
        public string AttachmentInfo { get; set; }
        /// <summary>
        /// 标注信息
        /// </summary>
        public string BadgeInfo { get; set; }
        public Guid Id { get; set; }
        public int Index { get; set; }
        public string Text { get; set; }

        /// <summary>
        /// 自动生成参数
        /// </summary>
        /// <returns></returns>
        private ComponentParamenter GetParamenter()
        {
            var key = string.IsNullOrWhiteSpace(ParameterKey) ? TypeName : ParameterKey;
            var p = new ComponentParamenter(key, this);
            return p;
        }

        public T OnAction<T>(Action<object> action) where T : TContext
        {
            CommandAction = action;
            return (T)this;
        }

        /// <summary>
        /// View方法委托到Model
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="view"></param>
        public void SetViewAction<TView>(TView view) where TView : TComponent
        {
            if (view == null)
            {
                return;
            }
            StateHasChanged = view.Update;
            UpdateDataContext = view.UpdateDataContext;
            LoadDataContext = view.LoadDataContext;
        }
    }


    public static class TContextExt
    {
        public static TModel ToViewModel<TModel, TView>(this TModel model) where TModel : TContext
        {
            if (model == null)
            {
                return model;
            }
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
        public TView View { get; set; }

        public TContext()
        {
        }

        public TContext(TView view)
        {
            View = view;
        }

        public override Type ContextType => typeof(TView);
    }

    public class TContainer : TContext, IBehaver
    {

        /// <summary>
        /// 加载
        /// </summary>
        public Action<object> Load { get; set; }
        /// <summary>
        /// 显示 / 激活
        /// </summary>
        public Action Show { get; set; }
        /// <summary>
        /// 关闭
        /// </summary>
        public Action Close { get; set; }
        ///// <summary>
        ///// 加载前
        ///// </summary>
        //public EventHandler OnLoading { get; set; }
        ///// <summary>
        ///// 显示前
        ///// </summary>
        //public EventHandler OnShowing { get; set; }
        ///// <summary>
        ///// 关闭前
        ///// </summary>
        //public EventHandler OnClosing { get; set; }
        ///// <summary>
        ///// 加载后
        ///// </summary>
        //public EventHandler OnLoaded { get; set; }
        ///// <summary>
        ///// 显示后
        ///// </summary>
        //public EventHandler OnShowed { get; set; }
        ///// <summary>
        ///// 关闭后
        ///// </summary>
        //public EventHandler OnClosed { get; set; }
        /// <summary>
        /// 操作指令
        /// </summary>
        public EventHandler<ContextResult> OnCommand { get; set; }
        /// <summary>
        /// 加载（子类）
        /// </summary>
        public Action<object> LoadItem { get; set; }
        /// <summary>
        /// 显示 / 激活（子类）
        /// </summary>
        public Action<object> ShowItem { get; set; }
        /// <summary>
        /// 关闭（子类）
        /// </summary>
        public Action<object> CloseItem { get; set; }
        /// <summary>
        /// 取消（子类）
        /// </summary>
        public Action Cancel { get; set; }
    }

}
