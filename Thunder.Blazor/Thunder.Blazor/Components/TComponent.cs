using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using Thunder.Blazor.Models;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// 子组件基类
    /// </summary>
    public class TComponent : ComponentBase, IDisposable,IThunderObject
    {
        /// <summary>
        /// 对象名称
        /// </summary>
        [Parameter] public string ObjectName { get; set; }
        /// <summary>
        /// 自定义对象
        /// </summary>
        [Parameter] public object Tag { get; set; }

        /// <summary>
        /// 级联参数（父组件传入参数）
        /// </summary>
        [CascadingParameter] protected ComponentParamenter Paramenters { get; set; }
        /// <summary>
        /// 级联参数（传入子组件）
        /// </summary>
        [CascadingParameter] protected ComponentParamenter ChildParamenters { get; set; }
        /// <summary>
        /// 子组件
        /// </summary>
        [Parameter] protected RenderFragment ChildContent { get; set; }     //todo:需要处理子组件队列 List<T>
        /// <summary>
        /// 组件名称
        /// </summary>
        [Parameter] protected string Name { get; set; }
        /// <summary>
        /// 点击回调
        /// </summary>
        [Parameter] protected EventCallback OnClick { get; set; }
        /// <summary>
        /// 关闭回调
        /// </summary>
        [Parameter] protected EventCallback OnClose { get; set; }
        /// <summary>
        /// 是否含有传入参数
        /// </summary>
        protected bool HasParamenters => Paramenters != null;

        public virtual void Dispose()
        {
        }

        /// <summary>
        /// 日志输入
        /// </summary>
        /// <param name="m"></param>
        protected void Log(string m)
        {
            Console.WriteLine(m);
        }
    }

    /// <summary>
    /// 组件
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class TComponentObject<TModel> : TComponent where TModel : new()
    {
        [Parameter] protected TModel DataContext { get; set; } = new TModel();
    }

    /// <summary>
    /// 含数据上下文和视图的组件
    /// </summary>
    /// <typeparam name="TView">视图类型</typeparam>
    /// <typeparam name="TModel">数据上下文</typeparam>
    public abstract class TComponent<TModel, TView> : TComponent<TModel> where TModel : TContext<TView>, new()
    {

    }

    /// <summary>
    /// 含上下文数据的组件
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class TComponent<TModel>:TComponent where TModel : TContext,new()
    {
        [Parameter] protected TModel DataContext { get; set; } = new TModel();
        protected override void OnInit()
        {
            base.OnInit();
            if (HasParamenters)
            {
                try
                {
                    DataContext = Paramenters.Get<TModel>();
                }
                catch (Exception ex)
                {
                }
            }

        }

        /// <summary>
        /// 设置子组件
        /// </summary>
        /// <param name="child">子组件数据</param>
        public void SetChild(TContext child)
        {
            DataContext.Child = child;
            ChildContent = DataContext.Child.ContextFragment;
            ChildParamenters = DataContext.Child.ContextParameters;
        }
    }

    /// <summary>
    /// 带容器的组件
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class TComponentContainer<TModel> : TComponent<TModel> where TModel : TContainer, new()
    {
        protected override void OnInit()
        {
            DataContext.Load = Load;
            DataContext.Show = Show;
            DataContext.Close = Close;

            base.OnInit();
        }

        protected virtual void Load() { }
        protected virtual void Show() { }
        protected virtual void Close() { }

        public virtual void DoCommand(ContextResult result)
        {
            DataContext.OnCommand?.Invoke(this, result);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }

    /// <summary>
    /// 带容器的组件
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    /// <typeparam name="TModel"></typeparam>
    public abstract class TComponentContainer<TModel,TCon>: TComponentContainer<TCon> where TCon : TContainer<TModel>, new() where TModel:TContext,new()
    {
        protected override void OnInit()
        {
            DataContext.LoadItem = LoadItem;
            DataContext.ShowItem = ShowItem;
            DataContext.CloseItem = CloseItem;

            base.OnInit();
        }

        protected virtual void LoadItem(TModel value) { }
        protected virtual void ShowItem(TModel value) { }
        protected virtual void CloseItem(TModel value) { }

    }



    /// <summary>
    /// 组件数据
    /// </summary>
    public  class TContext: IThunderObject, IVisual, IBaseBehaver
    {
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
        public bool IsVisabled { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActived { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsEnabled { get; set; }
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
        public static TModel ToViewModel<TModel,TView>(this TModel model) where TModel:TContext
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
        /// 说明文字
        /// </summary>
        public string Caption { get; set; }

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
