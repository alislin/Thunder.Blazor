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
        protected override void OnInit()
        {
            DataContext.Show = Show;
            base.OnInit();
        }

        protected void Show(TContext value,string caption=null,ButtonType button= ButtonType.OK,string size=null)
        {
            DataContext.Caption = caption ?? value.Caption;
            DataContext.Size = size;
            DataContext.ButtonType = button;
            DataContext.Child = value;
            Show();
            DataContext.Child.StateHasChanged?.Invoke();
        }

        protected override void Show()
        {
            DataContext.IsVisabled = true;
            StateHasChanged();
        }

        protected override void Load()
        {
            Show();
        }

        protected override void Close()
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
                    break;
                case ContextResultValue.Yes:
                    break;
                case ContextResultValue.No:
                    break;
            }

            //DataContext.Child = new TContext<TNull>();
            StateHasChanged();
        }
    }

    public class TModalContext : TContainer<TContext> 
    {
        public ButtonType ButtonType { get; set; }
        public new Action<TContext,string,ButtonType,string> Show { get; set; }
        public Action OKAction { get; set; }
        public Action CancelAction { get; set; }
        public TModalContext OK(Action action)
        {
            OKAction = action;
            return this;
        }
        public TModalContext Cancel (Action action)
        {
            CancelAction = action;
            return this;
        }
    }

}
