/* Ceated by Ya Lin. 2019/7/11 14:18:28 */

using Microsoft.AspNetCore.Components;
using System;
using Thunder.Blazor.Models;
using Thunder.Blazor.Services;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// Modal 窗口
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class TModal<TModel> : TComponentContainer<TModel> where TModel : TModalContext, new()
    {
        [Parameter] public ComponentParamenter Parameters { get; set; }
        [Parameter] public int ButtonTypes { get; set; }

        protected override void OnInitialized()
        {
            IsVisabled = false;
            PageType = PageTypes.Modal.ToString();
            UpdateDataContext();
            DataContext.Show = ShowContext;
            base.OnInitialized();
        }

        public override void LoadDataContext()
        {
            base.LoadDataContext();
            ButtonTypes = dataContext.ButtonTypes;
        }

        public override void UpdateDataContext()
        {
            base.UpdateDataContext();
            dataContext.ButtonTypes = ButtonTypes;
        }

        /// <summary>
        /// 显示 Modal
        /// </summary>
        /// <param name="value">Child 对象</param>
        /// <param name="caption">标题</param>
        /// <param name="button">按钮</param>
        public void ShowContext(TContext value, string caption = null, ButtonType button = ButtonType.OK)
        {
            DataContext.Caption = caption ?? value?.Caption;
            DataContext.ButtonTypes = (int)button;
            DataContext.Child = value;
            Show();
        }

        /// <summary>
        /// 显示 Modal
        /// </summary>
        /// <param name="value">TModalContext 对象</param>
        public override void Show(object value)
        {
            if (value != null)
            {
                DataContext = (TModel)value;
            }
            Show();
        }

        public override void Show()
        {
            DataContext.IsVisabled = true;
            LoadDataContext();
            this.InvokeAsync(StateHasChanged);
        }

        public override void Load()
        {
            Show();
        }

        public override void Close()
        {
            Close(ContextResult.Cancel());
        }

        public override void Load(object item)
        {
            var value = (TModel)item;
            ShowContext(value);
        }

        protected virtual void Close(ContextResult result)
        {
            DataContext.IsVisabled = false;
            DataContext.OnCommand?.Invoke(this, ContextResult.Cancel());
            switch (result.Result)
            {
                default:
                case ContextResultValue.None:
                    break;
                case ContextResultValue.OK:
                    DataContext?.OKAction?.Invoke();
                    break;
                case ContextResultValue.Cancel:
                    DataContext?.CancelAction?.Invoke();
                    break;
                case ContextResultValue.Close:
                    DataContext?.CloseAction?.Invoke();
                    break;
                case ContextResultValue.Yes:
                    DataContext?.YesAction?.Invoke();
                    break;
                case ContextResultValue.No:
                    DataContext?.NoAction?.Invoke();
                    break;
            }

            //DataContext.Child = new TContext<TNull>();
            LoadDataContext();
            this.InvokeAsync(StateHasChanged);
        }

        public override void Cancel()
        {
            Close(ContextResult.Cancel());
        }

        public override void Close(object item)
        {
            if (item == null)
            {
                Close(ContextResult.Cancel());
                return;
            }
            var result = (ContextResult)item;
            Close(result);
        }
    }

    public class TModalContext : TContainer
    {
        public TModalContext()
        {
        }

        public TModalContext(TContext context, string title)
        {
            Child = context;
            Caption = title;
        }

        public TModalContext(TContext context) : this(context, context?.Caption)
        {

        }


        public int ButtonTypes { get; set; }
        public new Action<TContext, string, ButtonType> Show { get; set; }

        public string OKTitle { get; set; } = "确定";
        public string CancelTitle { get; set; } = "取消";
        public string YesTitle { get; set; } = "是";
        public string NoTitle { get; set; } = "否";
        public string CloseTitle { get; set; } = "关闭";

        public Action OKAction { get; set; }
        public Action CancelAction { get; set; }
        public Action YesAction { get; set; }
        public Action NoAction { get; set; }
        public Action CloseAction { get; set; }

        public TModalContext OK(Action action, string title = null)
            => SetAction(action, ButtonTypeValue.OK, title);

        public new TModalContext Cancel(Action action, string title = null)
            => SetAction(action, ButtonTypeValue.Cancel, title);

        public TModalContext Yes(Action action, string title = null)
            => SetAction(action, ButtonTypeValue.Yes, title);

        public TModalContext No(Action action, string title = null)
            => SetAction(action, ButtonTypeValue.No, title);

        public new TModalContext Close(Action action, string title = null)
            => SetAction(action, ButtonTypeValue.Close, title);

        /// <summary>
        /// 方法设置
        /// </summary>
        /// <param name="action"></param>
        /// <param name="bvalue"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        protected TModalContext SetAction(Action action, ButtonTypeValue bvalue, string title = null)
        {
            title = string.IsNullOrWhiteSpace(title) ? null : title;
            ButtonTypes |= (int)bvalue;
            switch (bvalue)
            {
                case ButtonTypeValue.Close:
                    CloseAction = action;
                    CloseTitle = title ?? CloseTitle;
                    break;
                case ButtonTypeValue.OK:
                    OKAction = action;
                    OKTitle = title ?? OKTitle;
                    break;
                case ButtonTypeValue.Cancel:
                    CancelAction = action;
                    CancelTitle = title ?? CancelTitle;
                    break;
                case ButtonTypeValue.Yes:
                    YesAction = action;
                    YesTitle = title ?? YesTitle;
                    break;
                case ButtonTypeValue.No:
                    NoAction = action;
                    NoTitle = title ?? NoTitle;
                    break;
                default:
                    break;
            }
            return this;
        }
    }

}
