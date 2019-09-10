using Microsoft.AspNetCore.Components;
using System;
using Thunder.Blazor.Components;
using Thunder.Blazor.Models;

namespace Thunder.Blazor.Services
{
    public class ModalService : IModalService
    {
        public event Action<ContextResult> OnClose;

        public Action<TModalContext> ShowAction { get; set; }
        public Action<TContext,string, ButtonType> ShowContextAction { get; set; }

        private TModalContext ModalContext { get; set; }

        public void Show(object item)
        {
            var data = (TModalContext)item;
            if (!typeof(ComponentBase).IsAssignableFrom(data.Child.ContextType))
            {
                throw new ArgumentException($"{data.Child.ContextType.FullName} 组件类型错误.");
            }

            ModalContext = data;

            ShowAction?.Invoke(data);
        }

        public void Show(TContext item,string caption="",ButtonType actionButton= ButtonType.OK)
        {
            if (!typeof(ComponentBase).IsAssignableFrom(item.ContextType))
            {
                throw new ArgumentException($"{item.ContextType.FullName} 组件类型错误.");
            }
            if (ModalContext == null)
            {
                ModalContext = new TModalContext();
            }
            ModalContext.Child = item;
            Show(ModalContext);
        }

        public void Cancel()
        {
            OnClose?.Invoke(ContextResult.Cancel());
        }

        public void Close(ContextResult modalResult)
        {
            OnClose?.Invoke(modalResult);
        }
    }
}
