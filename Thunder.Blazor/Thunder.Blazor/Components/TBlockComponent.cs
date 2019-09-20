/* Ceated by Ya Lin. 2019/7/10 10:42:48 */

using Microsoft.AspNetCore.Components;
using System;
using Thunder.Blazor.Services;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// 内容区域基础类
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class TBlockComponent<TModel> : TComponent<TModel> where TModel : TNode<TModel>, new()
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

        public override void UpdateDataContext()
        {
            if (DataContext != null)
            {
                DataContext.IsOpen = IsOpen;
            }
            base.UpdateDataContext();
        }

        public override void LoadDataContext()
        {
            if (DataContext != null)
            {
                IsOpen = DataContext.IsOpen;
            }
            base.LoadDataContext();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            DataContext.Close = Close;
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
        public void OpenItemClick(Action commandAction)
        {
            ComponentService.DoAction("openblock");
            commandAction?.Invoke();
        }

    }
}
