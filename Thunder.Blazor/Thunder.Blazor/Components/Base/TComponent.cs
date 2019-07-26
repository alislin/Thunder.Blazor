/* Ceated by Ya Lin. 2019/7/11 14:52:29 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using Thunder.Blazor.Models;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// 子组件基类 (View)
    /// </summary>
    public class TComponent : ComponentBase, IDisposable,IThunderObject,IAnimate
    {
        private string domId;

        public TComponent()
        {
            domId = NewId();
        }

        /// <summary>
        /// 对象名称
        /// </summary>
        [Parameter] public string ObjectName { get; set; }
        /// <summary>
        /// 自定义对象
        /// </summary>
        [Parameter] public object Tag { get; set; }
        /// <summary>
        /// 自动Dom Id
        /// </summary>
        public string DomId => domId;

        /// <summary>
        /// 启用动画
        /// </summary>
        [Parameter] public bool AnimateEnabled { get; set; }
        /// <summary>
        /// 进入动画
        /// </summary>
        [Parameter] public string AnimateEnter{ get; set; }
        /// <summary>
        /// 退出动画
        /// </summary>
        [Parameter] public string AnimateExit { get; set; }

        /// <summary>
        /// 级联参数（父组件传入参数）
        /// </summary>
        [CascadingParameter] public ComponentParamenter Paramenters { get; set; }
        /// <summary>
        /// 级联参数（传入子组件）
        /// </summary>
        [CascadingParameter] public ComponentParamenter ChildParamenters { get; set; }
        /// <summary>
        /// 子组件
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }     //todo:需要处理子组件队列 List<T>
        /// <summary>
        /// 组件名称
        /// </summary>
        [Parameter] public string Name { get; set; }
        /// <summary>
        /// 点击回调
        /// </summary>
        [Parameter] public EventCallback OnClick { get; set; }
        /// <summary>
        /// 关闭回调
        /// </summary>
        [Parameter] public EventCallback OnClose { get; set; }
        /// <summary>
        /// 是否含有传入参数
        /// </summary>
        public bool HasParamenters => Paramenters != null;

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

        public string NewId(string key=null)
        {
            key = string.IsNullOrWhiteSpace(key) ? "t" : key;
            var r = new Random(DateTime.Now.Millisecond).Next(9999999).ToString("0000000");
            return $"{key}_{r}";
        }
    }

    /// <summary>
    /// 组件
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class TComponentObject<TModel> : TComponent where TModel : new()
    {
        [Parameter] public TModel DataContext { get; set; } = new TModel();
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
    public abstract class TComponent<TModel> : TComponent where TModel : TContext, new()
    {
        private TModel dataContext = new TModel();

        [Parameter] public TModel DataContext {
            get => dataContext;
            set
            {
                dataContext = value;
                if (dataContext!=null)
                {
                    dataContext.StateHasChanged = StateHasChanged;
                }
            }
        }

        protected override void OnInit()
        {
            base.OnInit();
            if (HasParamenters)
            {
                try
                {
                    DataContext = Paramenters.Get<TModel>();
                }
                catch
                {
                }
            }
            if (DataContext != null)
            {
                DataContext.StateHasChanged = StateHasChanged;
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

}
