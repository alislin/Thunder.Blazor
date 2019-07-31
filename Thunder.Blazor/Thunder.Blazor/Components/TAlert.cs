/* Ceated by Ya Lin. 2019/7/11 14:18:28 */

using System;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// Alert 组件
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class TAlert<TModel> : TComponent<TAlertContent> where TModel:TAlertContent,new()
    {
        protected override void OnInit()
        {
            base.OnInit();

            DataContext.Show = Show;
        }

        protected void Show(object obj)
        {
            DataContext.IsVisabled = true;
        }
    }

    public class TAlertContent : TContainer
    {
        public bool EnableCloseButton { get; set; }

        public Action<object> Show { get; set; }
    }

}
