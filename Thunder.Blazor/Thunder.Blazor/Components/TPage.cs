/* Ceated by Ya Lin. 2019/7/31 16:26:44 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Thunder.Blazor.Components
{
    public class TPage<TModel>:TComponent<TModel> where TModel:TPageContext,new()
    {
    }

    public class TPageContext : TContext
    {
        public bool ShowAlert => Alert != null && Alert.IsVisabled;
        public bool ShowModal => Modal != null && Modal.IsVisabled;

        public TAlertContent Alert { get; set; }
        public TModalContext Modal { get; set; }
    }
}
