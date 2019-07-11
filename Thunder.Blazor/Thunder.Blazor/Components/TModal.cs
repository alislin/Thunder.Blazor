/* Ceated by Ya Lin. 2019/7/11 14:18:28 */

using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Models;

namespace Thunder.Blazor.Components
{
    public class TModalBase:TComponentContainer<TModalContext>
    {
        protected override void Show()
        {
            Value.IsVisabled = true;
        }

        protected override void Load()
        {
            Show();
        }

        protected override void Close()
        {
            Value.IsVisabled = false;
            Value.OnCommand?.Invoke(this, ContextResult.Cancel());
        }
    }

    public class TModalContext : TContainer
    {
        public ButtonType ButtonType { get; set; }
    }
}
