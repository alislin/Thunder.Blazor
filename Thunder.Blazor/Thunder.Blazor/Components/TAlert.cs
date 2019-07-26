/* Ceated by Ya Lin. 2019/7/11 14:18:28 */

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// Alert 组件
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class TAlert<TModel> : TComponent<TAlertContent> where TModel:TAlertContent,new()
    {

    }

    public class TAlertContent : TContext
    {

    }

}
