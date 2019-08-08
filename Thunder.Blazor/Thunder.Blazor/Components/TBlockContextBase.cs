/* Ceated by Ya Lin. 2019/7/10 10:42:48 */

using Microsoft.AspNetCore.Components;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// 内容区域基础类
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class TBlockContextBase<TModel> : TComponent<TModel> where TModel: TNode<TModel>, new()
    {
        [Parameter] public bool IsOpen { get; set; }

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

        public void ToggleShow()
        {
            IsVisabled = !IsVisabled;
            UpdateDataContext();
        }

        public void ToggleOpen()
        {
            IsOpen = !IsOpen;
            UpdateDataContext();
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
    }


}
