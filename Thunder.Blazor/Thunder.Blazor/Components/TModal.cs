/* Ceated by Ya Lin. 2019/7/11 14:18:28 */

using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Models;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// Modal 窗口
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class TModalBase<TModel>:TComponentContainer<TModel> where TModel:TModalContext,new()
    {
        protected override void OnInitialized()
        {
            DataContext.Show = Show;
            base.OnInitialized();
        }

        protected void Show(TContext value,string caption=null,ButtonType button= ButtonType.OK,string size=null)
        {
            DataContext.Caption = caption ?? value.Caption;
            DataContext.ButtonType = button;
            DataContext.Child = value;
            Show();
        }

        public override void Show()
        {
            DataContext.IsVisabled = true;
            StateHasChanged();
        }

        public override void Load()
        {
            Show();
        }

        public override void Close()
        {
            Close(ContextResult.Cancel());
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
            StateHasChanged();
        }
    }

    public class TModalContext : TContainer
    {
        public ButtonType ButtonType { get; set; }
        public new Action<TContext, string, ButtonType, string> Show { get; set; }

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

        public TModalContext Cancel(Action action, string title = null)
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
        protected TModalContext SetAction(Action action,ButtonTypeValue bvalue,string title=null)
        {
            title = string.IsNullOrWhiteSpace(title) ? null : title;
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
