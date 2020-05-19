/* Ceated by Ya Lin. 2019/7/11 14:52:29 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Thunder.Blazor.Libs;
using Thunder.Blazor.Models;
using Thunder.Blazor.Services;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// 子组件基类 (View)
    /// </summary>
    public abstract class TComponent : ComponentBase, IDisposable, IThunderObject, IAnimate, IBehaverComponent, IAttachment, IBaseBehaver
    {
        private readonly string domId;
        private readonly CssBuild CssBuild = CssBuild.New;
        private readonly Random rnd = CommonData.Current.RndSeed;
        private ComponentService componentService;

        /// <summary>
        /// 销毁标志，可在关闭事件中取消
        /// </summary>
        protected bool Disposed;

        [Inject] protected ComponentService ComponentService { get => componentService;
            set 
            {
                componentService = value;
                if (componentService==null)
                {
                    return;
                }
                componentService.OnNeedUpdate += (o, e) => Update();
            }
        }

        public TComponent()
        {
            domId = NewId();
        }

        #region IThunderObject
        /// <summary>
        /// 对象名称
        /// </summary>
        [Parameter] public string ObjectName { get; set; }
        /// <summary>
        /// 自定义对象
        /// </summary>
        [Parameter] public object Tag { get; set; }
        #endregion

        /// <summary>
        /// 说明文字
        /// </summary>
        [Parameter] public string Caption { get; set; }

        /// <summary>
        /// 样式类型
        /// </summary>
        [Parameter] public string StyleClass { get; set; }
        /// <summary>
        /// Html 属性
        /// </summary>
        [Parameter(CaptureUnmatchedValues =true)] public Dictionary<string,object> AdditionalAttributes { get; set; }
        /// <summary>
        /// 仅使用设置的Style样式，跳过自动Style
        /// </summary>
        [Parameter] public bool OnlyStyleClass { get; set; }
        /// <summary>
        /// 自动Dom Id
        /// </summary>
        public string DomId => domId;
        /// <summary>
        /// 主要Css样式
        /// </summary>
        public virtual string CssStyle => GetCss();

        /// <summary>
        /// 完成初始化加载
        /// </summary>
        public bool InitLoaded { get; set; }
        /// <summary>
        /// 启用动画
        /// </summary>
        [Parameter] public bool AnimateEnabled { get; set; }
        /// <summary>
        /// 进入动画
        /// </summary>
        [Parameter] public string AnimateEnter { get; set; }
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
        /// 默认操作指令
        /// </summary>
        [Parameter] public Action<object> CommandAction { get; set; }
        /// <summary>
        /// 关闭以后调用
        /// </summary>
        public Action<object> OnClosed { get; set; }
        /// <summary>
        /// 关闭中
        /// </summary>
        public Action<object> OnClosing { get; set; }

        #endregion

        #region IBehaver
        /// <summary>
        /// 操作指令
        /// </summary>
        public EventHandler<ContextResult> OnCommand { get; set; }
        /// <summary>
        /// 绑定属性变化
        /// </summary>
        [Parameter] public EventCallback<object> OnBindChanged { get; set; }

        /// <summary>
        /// 加载
        /// </summary>
        public virtual void Load(object data) { }

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
            this.InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public virtual void Close()
        {
            Disposed = true;
            OnClosing?.Invoke(this);
            if (Disposed)
            {
                IsVisabled = false;
                this.InvokeAsync(StateHasChanged);
            }
            OnClosed?.Invoke(this);

        }
        #endregion

        #region IAttachment
        /// <summary>
        /// 附加信息
        /// </summary>
        public string AttachmentInfo { get; set; }
        /// <summary>
        /// 标注信息
        /// </summary>
        public string BadgeInfo { get; set; }

        #endregion

        #region Dispose接口
        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region 重写基础方法
        #endregion

        #region 组件服务
        /// <summary>
        /// 显示Modal窗口
        /// </summary>
        /// <param name="item">TContext 对象</param>
        /// <param name="button">按钮</param>
        protected void ShowModal(object item, List<ContextAction> buttons = null,SizeEnum sizeEnum = SizeEnum.Default, Action<object> onClose = null)
        {
            var ps = (TModal<TModalContext>)(object)ComponentService.Get(PageType.Modal);
            if (ps == null)
            {
                Log("No modal component exist.");
                return;
            }
            var child = (TContext)item;
            ps.ShowContext(child, child?.Caption, sizeEnum, buttons, onClose);
        }

        protected void ShowModal(TModalContext modalItem)
        {
            var ps = (TModal<TModalContext>)(object)ComponentService.Get(PageType.Modal);
            if (ps == null)
            {
                Log("No modal component exist.");
                return;
            }
            ps.Load(modalItem);
            ps.Show();
        }

        protected void CloseModal()
        {
            var ps = (TModal<TModalContext>)(object)ComponentService.Get(PageType.Modal);
            if (ps == null)
            {
                Log("No modal component exist.");
                return;
            }
            ps.Close();
        }

        protected void ShowAlert(object item)
        {
            var ps = ComponentService.Get(PageType.Alert);
            if (ps == null)
            {
                Log("No Alert component exist.");
                return;
            }
            ps.ShowItem(item);
        }

        protected void CloseAlert(object item)
        {
            var ps = ComponentService.Get(PageType.Alert);
            if (ps == null)
            {
                Log("No Alert component exist.");
                return;
            }
            ps.CloseItem(item);
        }

        #endregion

        #region 读取默认级联参数
        /// <summary>
        /// 读取默认级联参数，使用 TContext<TView> 包装
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public TContext<TView> GetContextParameter<TView>(string key)
        {
            var par = Paramenters?.Get<TContext<TView>>(key);
            if (par != null)
            {
                return par;
            }
            return default;
        }
        #endregion

        /// <summary>
        /// 日志输入
        /// </summary>
        /// <param name="m"></param>
        protected static void Log(string m)
        {
            Console.WriteLine(m);
        }


        /// <summary>
        /// 生成随机Id
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string NewId(string key = null)
        {
            key = string.IsNullOrWhiteSpace(key) ? "t" : key;
            var r = rnd.Next(9999999).ToString("0000000", new NumberFormatInfo());
            return $"{key}_{r}";
        }

        private string GetCss()
        {
            CssBuild.Reset().Add(StyleClass);
            if (AnimateEnabled)
            {
                CssBuild.Add(AnimateEnter);
            }
            if (!OnlyStyleClass)
            {
                StyleBuild(CssBuild);
            }
            return CssBuild.Build().CssString;
        }

        /// <summary>
        /// 生成 Style Class
        /// </summary>
        /// <param name="cssBuilder"></param>
        protected virtual void StyleBuild(CssBuild cssBuilder)
        {

        }

        public virtual void UpdateDataContext()
        {

        }

        public virtual void LoadDataContext()
        {

        }

        /// <summary>
        /// 更新组件（调用 StateHasChanged）
        /// </summary>
        public void Update()
        {
            this.InvokeAsync(StateHasChanged);
        }

    }

    /// <summary>
    /// 组件
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class TComponentObject<TModel> : TComponent where TModel : new()
    {
        protected TModel dataContext = new TModel();

        [Parameter]
        public TModel DataContext
        {
            get
            {
                //UpdateDataContext();
                return dataContext;
            }
            set
            {
                dataContext = value;
                if (dataContext != null)
                {
                    LoadDataContext();
                    //StateHasChanged();
                }
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        /// <summary>
        /// 设置子组件
        /// </summary>
        /// <param name="child">子组件数据</param>
        public virtual void SetChild(TContext child) { }

        /// <summary>
        /// 设置子组件
        /// </summary>
        /// <param name="child">子组件数据</param>
        protected void SetChild<T, TV>(T child, TV data) where T : TContext where TV : TContext
        {
            if (data == null)
            {
                throw new NullReferenceException();
            }
            data.Child = child;
            ChildContent = data.Child?.ContextFragment;
            ChildParamenters = data.Child.ContextParameters;
        }

        /// <summary>
        /// Udpate DataContext from view
        /// </summary>
        protected virtual void UpdateDataContext<T>(T dataContext) where T : TContext
        {
            if (dataContext != null)
            {
                dataContext.ObjectName = ObjectName;
                dataContext.Tag = Tag;
                //dataContext.StyleClass = StyleClass;
                dataContext.IsVisabled = IsVisabled;
                dataContext.IsActived = IsActived;
                dataContext.IsEnabled = IsEnabled;
                dataContext.CommandAction = CommandAction;
                dataContext.OnClosed = OnClosed;
                dataContext.Caption = Caption;
                dataContext.AttachmentInfo = AttachmentInfo;
                dataContext.BadgeInfo = BadgeInfo;

                dataContext.DomId = DomId;
                dataContext.SetViewAction(this);
            }
        }

        /// <summary>
        /// Load datacontext to view
        /// </summary>
        protected virtual void LoadDataContext<T>(T dataContext) where T : TContext
        {
            if (dataContext != null)
            {
                ObjectName = dataContext.ObjectName;
                Tag = dataContext.Tag;
                //StyleClass = dataContext.StyleClass;
                IsVisabled = dataContext.IsVisabled;
                IsActived = dataContext.IsActived;
                IsEnabled = dataContext.IsEnabled;
                CommandAction = dataContext.CommandAction;
                OnClosed = dataContext.OnClosed;
                Caption = dataContext.Caption;
                AttachmentInfo = dataContext.AttachmentInfo;
                BadgeInfo = dataContext.BadgeInfo;

                dataContext.DomId = DomId;
                dataContext.SetViewAction(this);
            }

        }

    }

    /// <summary>
    /// 含上下文数据的组件
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class TComponent<TModel> : TComponentObject<TModel> where TModel : TContext, new()
    {

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (HasParamenters)
            {
                try
                {
                    var para = Paramenters.Get<TModel>();
                    if (para != null)
                    {
                        dataContext = para;
                    }
                }
                catch (KeyNotFoundException ex)
                {
                    Log(ex.Message);
                }            
            }
            if (dataContext != null)
            {
                dataContext.SetViewAction(this);
            }
        }

        /// <summary>
        /// Udpate DataContext from view
        /// </summary>
        public override void UpdateDataContext()
        {
            UpdateDataContext(dataContext);
        }

        /// <summary>
        /// Load datacontext to view
        /// </summary>
        public override void LoadDataContext()
        {
            LoadDataContext(dataContext);
        }

        /// <summary>
        /// 加载
        /// </summary>
        public override void Load(object obj)
        {
            var result = obj is TModel ? (TModel)obj : null;
            if (result == null)
            {
                throw new ArgumentException($"obj is not {typeof(TModel).Name}.");
            }
            dataContext = result;
            LoadDataContext();
        }
        /// <summary>
        /// 显示 / 激活
        /// </summary>
        public override void Show()
        {
            LoadDataContext();
            base.Show();
            UpdateDataContext();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public override void Close()
        {
            LoadDataContext();
            base.Close();
            UpdateDataContext();
        }

        /// <summary>
        /// 设置子组件
        /// </summary>
        /// <param name="child">子组件数据</param>
        public override void SetChild(TContext child)
        {
            SetChild(child, dataContext);
        }
    }

    /// <summary>
    /// 带容器的组件
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class TComponentContainer<TModel> : TComponent<TModel>, IPageService where TModel : TContainer, new()
    {
        public string ServiceId { get; set; }
        public string PageType { get; set; }
        /// <summary>
        /// 返回值
        /// </summary>
        public EventCallback<ContextResult> OnResult { get; set; }

        public abstract void LoadItem(object item);
        public abstract void ShowItem(object item);
        public abstract void CloseItem(object item);


        protected override void OnInitialized()
        {
            base.OnInitialized();

            ServiceId = DomId;
            ComponentService.Regist(this);

            DataContext.Load = Load;
            DataContext.Show = Show;
            DataContext.Close = Close;

            DataContext.LoadItem = Load;
            DataContext.ShowItem = ShowItem;
            DataContext.CloseItem = CloseItem;
            DataContext.Cancel = Cancel;

        }

        public virtual void DoCommand(ContextResult result)
        {
            DataContext.OnCommand?.Invoke(this, result);
        }

        protected override void Dispose(bool disponsing)
        {
            if (disponsing)
            {
                ComponentService.UnRegist(ServiceId);
            }
        }

        public abstract void Cancel();
    }
}
