/* Ceated by Ya Lin. 2019/7/11 14:18:28 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Load datacontext to view
        /// </summary>
        public override void LoadDataContext()
        {
            base.LoadDataContext();
            ButtonTypes = dataContext.ButtonTypes;
        }

        /// <summary>
        /// Udpate DataContext from view
        /// </summary>
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
        public void ShowContext(TContext value, string caption = null, List<ContextAction> buttons=null)
        {
            DataContext.Caption = caption ?? value?.Caption;
            DataContext.ResetAction();
            if (buttons != null)
            {
                DataContext.ContextActions.AddRange(buttons);
            }
            DataContext.Child = value;
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
                DataContext = (TModel)value;
            }
            Show();
        }

        /// <summary>
        /// 显示
        /// </summary>
        public override void Show()
        {
            DataContext.IsVisabled = true;
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
            DataContext.IsVisabled = false;
            DataContext.OnCommand?.Invoke(this, ContextResult.Cancel());
            result.Action?.Invoke(data);
            OnResult.InvokeAsync(result.ContextResult(data));

            //DataContext.Child = new TContext<TNull>();
            LoadDataContext();
            this.InvokeAsync(StateHasChanged);
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

            var action = DataContext.ContextActions?.FirstOrDefault(x => x.Result == resulttype);
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
        public new Action<TContext, string, List<ContextAction>> Show { get; set; }

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
                throw new NullReferenceException("contextAction is null.");
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

        protected string SetActionTitle(ContextAction contextAction)
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
