/* Ceated by Ya Lin. 2019/7/10 10:42:48 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using Thunder.Blazor.Services;

namespace Thunder.Blazor.Components
{
    public class Node<TModel>:TagBlockContext where TModel : TagBlockContext,new()
    {
        private TModel context = new TModel();

        public Node()
        {
        }

        public Node(TModel content)
        {
            SetValue(content);
        }

        public TModel Context { get => context; set => SetValue(value); }

        public IList<Node<TModel>> ChildNodes { get; set; } = new List<Node<TModel>>();
        public bool HasChildNodes => (ChildNodes?.Count ?? 0) > 0;
        public bool IsOpen { get; set; }
        public Action Close { get; set; } = () => { };
        public void Add(Node<TModel> child, Action action = null)
        {
            ChildNodes.Add(child);
        }

        public void Add(TModel item,Action action = null)
        {
            var child = new Node<TModel>(item);
            ChildNodes.Add(child);
        }

        public void Add(IList<TModel> childs)
        {
            foreach (var item in childs)
            {
                Add(item);
            }
        }

        public virtual void SetValue(TModel value)
        {
            context = value;
            Id = value.Id;
            Index = value.Index;
            Text = value.Text;
            Icon = value.Icon;
            Count = value.Count;
            DomId = value.DomId;
            Caption = value.Caption;
            ContextType = value.ContextType;
            Child = value.Child;
            ObjectName = value.ObjectName;
            Tag = value.Tag;
            IsVisabled = value.IsVisabled;
            IsActived = value.IsActived;
            IsEnabled = value.IsEnabled;
            CommandAction = value.CommandAction;
            StateHasChanged = value.StateHasChanged;
            UpdateDataContext = value.UpdateDataContext;
            LoadDataContext = value.LoadDataContext;
            AttachmentInfo = value.AttachmentInfo;
            BadgeInfo = value.BadgeInfo;
        }
    }

    //public class TNodeContext<TModel> : Node<TModel> where TModel : TagBlockContext
    //{
    //    public TNodeContext()
    //    {
    //    }

    //    public TNodeContext(TModel content) : base(content)
    //    {
    //    }
    //}

    public class TNodeComponent<TModel> : TComponentObject<Node<TModel>> where TModel : TagBlockContext,new()
    {
        [Inject] public ComponentService ComponentService { get; set; }
        /// <summary>
        /// 展开状态
        /// </summary>
        [Parameter] public bool IsOpen { get; set; }
        /// <summary>
        /// 是否禁用自动关闭
        /// </summary>
        [Parameter] public bool IsDisabledAutoClose { get; set; }

        public override void LoadDataContext()
        {
            if (DataContext != null)
            {
                IsOpen = DataContext.IsOpen;
            }
            LoadDataContext(dataContext.Context);
        }

        public override void UpdateDataContext()
        {
            if (DataContext != null)
            {
                DataContext.IsOpen = IsOpen;
            }
            UpdateDataContext(dataContext.Context);
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (HasParamenters)
            {
                try
                {
                    dataContext.Context = Paramenters.Get<TModel>();
                }
                catch
                {
                }
            }
            if (dataContext != null)
            {
                dataContext.Context.SetViewAction(this);
                //DataContext.StateHasChanged = StateHasChanged;
            }
            dataContext.Close = Close;
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
        /// 设置子组件
        /// </summary>
        /// <param name="child">子组件数据</param>
        public override void SetChild(TContext child)
        {
            SetChild(child, dataContext.Context);
        }
        public void ToggleShow()
        {
            IsVisabled = !IsVisabled;
            if (IsVisabled)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public void ToggleOpen()
        {
            IsOpen = !IsOpen;
            UpdateDataContext();
            if (IsOpen)
            {
                if (!IsDisabledAutoClose)
                {
                    ComponentService.AddAction("openblock", () => { Close(); });
                }
                Open();
            }
            else
            {
                Close();
            }
        }

        public void ToggleActive()
        {
            IsActived = !IsActived;
            UpdateDataContext();
        }

        public void ToggleEnable()
        {
            IsEnabled = !IsEnabled;
            UpdateDataContext();
        }

        public void Open()
        {
            IsOpen = true;
            UpdateDataContext();
            StateHasChanged();

        }

        public void Hide()
        {
            base.Close();
        }

        public new void Close()
        {
            IsOpen = false;
            UpdateDataContext();
            StateHasChanged();

        }

        /// <summary>
        /// 扩展组件点击事件（自动关闭展开状态）
        /// </summary>
        /// <param name="commandAction"></param>
        public void OpenItemClick(Action commandAction)
        {
            ComponentService.DoAction("openblock");
            commandAction?.Invoke();
        }
    }
}
