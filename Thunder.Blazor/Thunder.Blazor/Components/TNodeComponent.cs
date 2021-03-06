﻿/* Ceated by Ya Lin. 2019/7/10 10:42:48 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using Thunder.Blazor.Services;

namespace Thunder.Blazor.Components
{
    public class TNodeComponent<TModel> : TComponentObject<Node<TModel>> where TModel : TagBlockContext, new()
    {
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
                    var para = Paramenters.Get<TModel>();
                    if (para != null)
                    {
                        dataContext.Context = para;
                    }
                }
                catch (KeyNotFoundException ex)
                {
                    Log(ex.Message);
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
        public override void Load(object obj)
        {
            var result = obj is Node<TModel> ? (Node<TModel>)obj : null;
            if (result == null)
            {
                throw new ArgumentException($"obj is not {typeof(Node<TModel>).Name}.");
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

        public void ToggleOpen(object v)
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
            this.InvokeAsync(StateHasChanged);

        }

        public void Hide()
        {
            base.Close();
        }

        public new void Close()
        {
            IsOpen = false;
            UpdateDataContext();
            this.InvokeAsync(StateHasChanged);

        }

        /// <summary>
        /// 扩展组件点击事件（自动关闭展开状态）
        /// </summary>
        /// <param name="commandAction"></param>
        public void OpenItemClick(Action<object> commandAction)
        {
            ComponentService.DoAction("openblock");
            commandAction?.Invoke(null);
        }
    }
}
