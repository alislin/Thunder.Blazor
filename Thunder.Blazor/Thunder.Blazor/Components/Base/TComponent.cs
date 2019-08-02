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
    public class TComponent : ComponentBase, IDisposable,IThunderObject,IAnimate, IBehaverComponent
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

        public bool InitLoaded { get; set; }
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

        #region IBaseBehaver
        /// <summary>
        /// 是否可见
        /// </summary>
        [Parameter] public bool IsVisabled { get; set; } = true;
        /// <summary>
        /// 是否激活
        /// </summary>
        [Parameter] public bool IsActived { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [Parameter] public bool IsEnabled { get; set; } = true;
        /// <summary>
        /// 操作指令
        /// </summary>
        public Action CommandAction { get; set; }

        #endregion

        #region IBehaver
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
        /// <summary>
        /// 加载
        /// </summary>
        public virtual void Load()
        {
            Show();
        }
        /// <summary>
        /// 显示 / 激活
        /// </summary>
        public virtual void Show()
        {
            if (!IsVisabled)
            {
                InitLoaded = false;
            }
            IsVisabled = true;
            if (!InitLoaded)
            {
                OnShowing?.Invoke(this, new EventArgs());
            }
            StateHasChanged();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public virtual void Close()
        {
            IsVisabled = false;
            StateHasChanged();
        }
        #endregion

        public virtual void Dispose()
        {
            Close();
        }

        /// <summary>
        /// 日志输入
        /// </summary>
        /// <param name="m"></param>
        protected void Log(string m)
        {
            Console.WriteLine(m);
        }

        /// <summary>
        /// 生成随机Id
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string NewId(string key=null)
        {
            key = string.IsNullOrWhiteSpace(key) ? "t" : key;
            var r = new Random(DateTime.Now.Millisecond).Next(9999999).ToString("0000000");
            return $"{key}_{r}";
        }

        protected override bool ShouldRender()
        {
            return base.ShouldRender();
        }

        protected override void OnAfterRender()
        {
            base.OnAfterRender();
            if (!InitLoaded)
            {
                InitLoaded = true;
                OnShowed?.Invoke(this, new EventArgs());
            }
        }

    }

    /// <summary>
    /// 组件
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class TComponentObject<TModel> : TComponent where TModel : new()
    {
        [Parameter] public TModel DataContext { get; set; } = new TModel();

        protected override void OnInit()
        {
            base.OnInit();
        }
    }

    /// <summary>
    /// 含上下文数据的组件
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class TComponent<TModel> : TComponent where TModel : TContext, new()
    {
        protected TModel dataContext = new TModel();

        //#region IBaseBehaver
        ///// <summary>
        ///// 是否可见
        ///// </summary>
        //[Parameter] public new bool IsVisabled { get => dataContext?.IsVisabled ?? false; set => dataContext.IsVisabled = value; }
        ///// <summary>
        ///// 是否激活
        ///// </summary>
        //[Parameter] public new bool IsActived { get => dataContext?.IsActived ?? false; set => dataContext.IsActived = value; }
        ///// <summary>
        ///// 是否有效
        ///// </summary>
        //[Parameter] public new bool IsEnabled { get => dataContext?.IsEnabled??false; set => dataContext.IsEnabled = value; }
        ///// <summary>
        ///// 操作指令
        ///// </summary>
        //[Parameter] public new Action CommandAction { get => dataContext?.CommandAction; set => dataContext.CommandAction = value; }
        //#endregion

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

        protected void UpdateBehaver()
        {
            dataContext.IsVisabled = IsVisabled;
            dataContext.IsEnabled = IsEnabled;
            dataContext.IsActived = IsActived;
            //dataContext.CommandAction = CommandAction;
        }

        protected void UpdateBehaverValue()
        {
            IsVisabled = dataContext.IsVisabled;
            IsEnabled = dataContext.IsEnabled;
            IsActived = dataContext.IsActived;
            CommandAction = dataContext.CommandAction;
        }

        /// <summary>
        /// 加载
        /// </summary>
        public override void Load()
        {
            Show();
        }
        /// <summary>
        /// 显示 / 激活
        /// </summary>
        public override void Show()
        {
            UpdateBehaverValue();
            base.Show();
            UpdateBehaver();
            //dataContext.IsVisabled = base.IsVisabled;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public override void Close()
        {
            UpdateBehaverValue();
            base.Close();
            UpdateBehaver();
            //dataContext.IsVisabled = base.IsVisabled;
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
        public virtual void LoadItem(object value) { }
        public virtual void ShowItem(object value) { }
        public virtual void CloseItem(object value) { }

        protected override void OnInit()
        {
            base.OnInit();
            DataContext.Load = Load;
            DataContext.Show = Show;
            DataContext.Close = Close;

            DataContext.LoadItem = LoadItem;
            DataContext.ShowItem = ShowItem;
            DataContext.CloseItem = CloseItem;
        }

        public virtual void DoCommand(ContextResult result)
        {
            DataContext.OnCommand?.Invoke(this, result);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
