/* Ceated by Ya Lin. 2019/7/11 14:52:29 */

using Microsoft.AspNetCore.Components;
using System;
using Thunder.Blazor.Libs;
using Thunder.Blazor.Models;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// 子组件基类 (View)
    /// </summary>
    public class TComponent : ComponentBase, IDisposable, IThunderObject, IAnimate, IBehaverComponent, IAttachment
    {
        private readonly string domId;
        private readonly CssBuild CssBuild = CssBuild.New;
        private readonly Random rnd = new Random(DateTime.Now.Millisecond);

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
        /// 点击回调
        /// </summary>
        //[Parameter] public EventCallback<UIMouseEventArgs> OnClick { get; set; }
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
        /// 默认操作指令
        /// </summary>
        [Parameter] public Action CommandAction { get; set; }

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

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Close();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

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
            var r = rnd.Next(9999999).ToString("0000000");
            return $"{key}_{r}";
        }

        protected override bool ShouldRender()
        {
            return base.ShouldRender();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            if (firstRender)
            {
                OnShowed?.Invoke(this, new EventArgs());
            }
        }

        private string GetCss()
        {
            CssBuild.Reset().Add(StyleClass);
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
            StateHasChanged();
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
        protected void SetChild<T, V>(T child, V data) where T : TContext where V : TContext
        {
            data.Child = child;
            ChildContent = data.Child.ContextFragment;
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
                dataContext.Caption = Caption;
                dataContext.AttachmentInfo = AttachmentInfo;
                dataContext.BadgeInfo = BadgeInfo;

                dataContext.DomId = DomId;
                dataContext.SetViewAction(this);
            }
            //dataContext.IsVisabled = IsVisabled;
            //dataContext.IsEnabled = IsEnabled;
            //dataContext.IsActived = IsActived;
            ////dataContext.CommandAction = CommandAction;
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
                Caption = dataContext.Caption;
                AttachmentInfo = dataContext.AttachmentInfo;
                BadgeInfo = dataContext.BadgeInfo;

                dataContext.DomId = DomId;
                dataContext.SetViewAction(this);
            }

            //IsVisabled = dataContext.IsVisabled;
            //IsEnabled = dataContext.IsEnabled;
            //IsActived = dataContext.IsActived;
            //CommandAction = dataContext.CommandAction;
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
                    dataContext = Paramenters.Get<TModel>();
                }
                catch
                {
                }
            }
            if (dataContext != null)
            {
                dataContext.SetViewAction(this);
                //DataContext.StateHasChanged = StateHasChanged;
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
        public override void Load()
        {
            Show();
        }
        /// <summary>
        /// 显示 / 激活
        /// </summary>
        public override void Show()
        {
            LoadDataContext();
            base.Show();
            UpdateDataContext();
            //dataContext.IsVisabled = base.IsVisabled;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public override void Close()
        {
            LoadDataContext();
            base.Close();
            UpdateDataContext();
            //dataContext.IsVisabled = base.IsVisabled;
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
    public class TComponentContainer<TModel> : TComponent<TModel> where TModel : TContainer, new()
    {
        public virtual void LoadItem(object value) { }
        public virtual void ShowItem(object value) { }
        public virtual void CloseItem(object value) { }

        protected override void OnInitialized()
        {
            base.OnInitialized();
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

        protected override void Dispose(bool disponsing)
        {
            base.Dispose();
        }

    }
}
