/* Ceated by Ya Lin. 2019/7/11 14:18:28 */

using Thunder.Blazor.Services;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// Alert 组件
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class TAlert<TModel> : TComponentContainer<TModel> where TModel : TAlertContext, new()
    {
        protected override void OnInitialized()
        {
            IsVisabled = false;
            PageType = Services.PageType.Alert.ToString();
            UpdateDataContext();
            View.Show = Show;
            base.OnInitialized();
        }

        public override void Cancel()
        {
            Close();
        }

        public override void CloseItem(object item)
        {
            Cancel();
        }

        public override void LoadItem(object item)
        {
            if (item == null)
            {
                return;
            }
            View.Child = (TContext)item;
            LoadDataContext();
        }

        public override void Load(object obj)
        {
            if (obj == null)
            {
                return;
            }
            View = (TModel)obj;
            LoadDataContext();
        }

        public override void ShowItem(object item)
        {
            Show();
        }
    }

    public class TAlertContext : TContainer
    {
        public bool EnableCloseButton { get; set; }
    }

}
