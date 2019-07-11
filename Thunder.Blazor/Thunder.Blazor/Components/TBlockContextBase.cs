/* Ceated by Ya Lin. 2019/7/10 10:42:48 */

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class TBlockContextBase<TModel> : TComponent<TModel> where TModel: TNode<TModel>, new()
    {
        public void ToggleShow()
        {
            DataContext.IsVisabled = !DataContext.IsVisabled;
        }
    }


}
