using System;
using Thunder.Blazor.Components;
using Thunder.Blazor.Models;

namespace Thunder.Blazor.Services
{
    public interface IModalService
    {
        Action<TModalContext> ShowAction { get; set; }
        Action<TContext, string, ButtonType> ShowContextAction { get; set; }

        event Action<ContextResult> OnClose;

        void Cancel();
        void Close(ContextResult modalResult);
        void Show(object item);
        void Show(TContext item, string caption = "", ButtonType actionButton = ButtonType.OK);
    }

}
