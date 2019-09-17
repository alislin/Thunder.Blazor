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
            PageType = PageTypes.Alert.ToString();
            UpdateDataContext();
            DataContext.Show = Show;
            base.OnInitialized();
        }

        public override void Cancel()
        {
            Close();
        }

        public override void Close(object item)
        {
            Cancel();
        }

        public override void Load(object item)
        {
            Show(item);
        }

        public override void Show(object item)
        {
            if (item == null)
            {
                Show();
                return;
            }
            var mc = (TModel)item;
            dataContext = mc;
            LoadDataContext();
            Show();
        }
    }

    public class TAlertContext : TContainer
    {
        public bool EnableCloseButton { get; set; }
    }

}
