/* Ceated by Ya Lin. 2019/7/11 14:18:28 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thunder.Blazor.Models;
using Thunder.Blazor.Services;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// Modal 窗口
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class TModal : TComponentContainer
    {
        [Parameter] public ComponentParamenter Parameters { get; set; }
        [Parameter] public int ButtonTypes { get; set; }
        private Action OnCloseAction;

        protected override void OnInitialized()
        {
            IsVisabled = false;
            PageType = Services.PageType.Modal.ToString();
            UpdateDataContext();
            View.Show = ShowContext;
            base.OnInitialized();
        }

        /// <summary>
        /// Load datacontext to view
        /// </summary>
        public override void LoadDataContext()
        {
            base.LoadDataContext();
            ButtonTypes = view.ButtonTypes;
        }

        /// <summary>
        /// Udpate DataContext from view
        /// </summary>
        public override void UpdateDataContext()
        {
            base.UpdateDataContext();
            view.ButtonTypes = ButtonTypes;
        }

        /// <summary>
        /// 显示 Modal
        /// </summary>
        /// <param name="value">Child 对象</param>
        /// <param name="caption">标题</param>
        /// <param name="button">按钮</param>
        public void ShowContext(TContext value, string caption = null,SizeEnum sizeEnum= SizeEnum.Default, List<ContextAction> buttons=null, Action<object> onClose = null)
        {
            View.Caption = caption ?? value?.Caption;
            View.SizeEnum = sizeEnum;
            View.ResetAction();
            if (buttons != null)
            {
                View.ContextActions.AddRange(buttons);
            }
            View.Child = value;
            View.OnClosed = onClose;
            Show();
        }

        /// <summary>
        /// 显示 Modal
        /// </summary>
        /// <param name="value">TModalContext 对象</param>
        public override void ShowItem(object item)
        {
            if (item != null)
            {
                var value = item is TModel ? (TModel)item : null;
                if (value == null)
                {
                    throw new ArgumentException($"obj is not {typeof(TModel).Name}.");
                }
                View = (TModel)value;
            }
            Show();
        }

        /// <summary>
        /// 显示
        /// </summary>
        public override void Show()
        {
            View.IsVisabled = true;
            LoadDataContext();
            this.InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 加载
        /// </summary>
        public override void Load(object obj)
        {
            base.Load(obj);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public override void Close()
        {
            CloseItem(ContextResult.Cancel());
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="item"></param>
        public override void LoadItem(object item)
        {
            var value = (TContext)item;
            ShowContext(value);
        }

        protected virtual void Close(ContextAction result,object data)
        {
            if (result != null)
            {
                result.ContinueAction = (result, data) => Closed(result, data);
            }
            result?.Action?.Invoke(result);
            if (!result.Disposed)
            {
                return;
            }
            OnClosing = o =>
            {
                Disposed = result?.Disposed ?? true;
            };
            Closed(result, data); 
        }

        protected virtual void Closed(ContextAction result, object data)
        {
            View.IsVisabled = false;
            View.OnCommand?.Invoke(this, ContextResult.Cancel());
            OnResult.InvokeAsync(result?.ContextResult(data));

            //DataContext.Child = new TContext<TNull>();
            LoadDataContext();
            base.Close();
            OnCloseAction?.Invoke();
        }

        public override void Cancel()
        {
            CloseItem(ContextResult.Cancel());
        }

        public override void CloseItem(object item)
        {
            var result = item is ContextResult ? (ContextResult)item : null;

            var data = result?.Data;
            var resulttype = result?.Result ?? ContextResultValue.Cancel;

            var action = View.ContextActions?.FirstOrDefault(x => x.Result == resulttype);
            action ??= new ContextAction("", ContextResultValue.Cancel, null);
            Close(action, data);
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

        /// <summary>
        /// 按钮枚举值
        /// </summary>
        public int ButtonTypes { get; set; }
        /// <summary>
        /// 显示尺寸
        /// </summary>
        public SizeEnum SizeEnum { get; set; }
        public new Action<TContext, string,SizeEnum, List<ContextAction>,Action<object>> Show { get; set; }

        public List<ContextAction> ContextActions { get; } = new List<ContextAction>();

        /// <summary>
        /// Add OK button
        /// </summary>
        /// <param name="action"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public TModalContext OK(Action<object> action, string title = null)
            => AddAction(new ContextAction(title, ContextResultValue.OK, action));

        /// <summary>
        /// Add OK button
        /// </summary>
        /// <param name="action"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public new TModalContext Cancel(Action<object> action, string title = null)
            => AddAction(new ContextAction(title, ContextResultValue.Cancel, action));

        /// <summary>
        /// Add Yes button
        /// </summary>
        /// <param name="action"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public TModalContext Yes(Action<object> action, string title = null)
            => AddAction(new ContextAction(title, ContextResultValue.Yes, action));

        /// <summary>
        /// Add No button
        /// </summary>
        /// <param name="action"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public TModalContext No(Action<object> action, string title = null)
            => AddAction(new ContextAction(title, ContextResultValue.No, action));

        /// <summary>
        /// Add Close button
        /// </summary>
        /// <param name="action"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public new TModalContext Close(Action<object> action, string title = null)
            => AddAction(new ContextAction(title, ContextResultValue.Close, action));

        /// <summary>
        /// Add Custom button
        /// </summary>
        /// <param name="action"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public TModalContext CustomButton(Action<object> action, string title = null)
            => AddAction(new ContextAction(title, ContextResultValue.None, action));

        /// <summary>
        /// 添加按钮方法
        /// </summary>
        /// <param name="contextAction"></param>
        /// <returns></returns>
        public TModalContext AddAction(ContextAction contextAction)
        {
            if (contextAction == null)
            {
                throw new NullReferenceException();
            }
            ButtonTypes |= (int)contextAction.Result;
            contextAction.Text = SetActionTitle(contextAction);
            ContextActions.Add(contextAction);
            return this;
        }

        /// <summary>
        /// 重置按钮方法
        /// </summary>
        /// <returns></returns>
        public TModalContext ResetAction()
        {
            ContextActions.Clear();
            return this;
        }

        protected static string SetActionTitle(ContextAction contextAction)
        {
            if (!string.IsNullOrWhiteSpace(contextAction?.Text))
            {
                return contextAction.Text;
            }
            string v = contextAction!.Result switch
            {
                ContextResultValue.OK => "确定",
                ContextResultValue.Cancel => "取消",
                ContextResultValue.Yes => "是",
                ContextResultValue.No => "否",
                ContextResultValue.Close => "关闭",
                _ => ""
            };

            return v;
       }

    }

}
