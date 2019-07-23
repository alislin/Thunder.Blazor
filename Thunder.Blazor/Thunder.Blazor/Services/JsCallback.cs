using Microsoft.JSInterop;
using System;

namespace Thunder.Blazor.Services
{
    /// <summary>
    /// JS回调类
    /// </summary>
    public class JsCallback
    {
        public Action Confirm { get; set; }
        public Action Cancel { get; set; }

        [JSInvokable]
        public void ConfirmCallback()
        {
            Confirm?.Invoke();
        }

        [JSInvokable]
        public void CancelCallback()
        {
            Cancel?.Invoke();
        }


    }

    public class JsAction
    {
        public Action Action { get; set; }

        [JSInvokable]
        public void CallAction()
        {
            Action?.Invoke();
        }
    }

    public static class JsCallbackext
    {
        //public static DotNetObjectRef ToObjectRef(this JsCallback obj)
        //{
        //    return new DotNetObjectRef(obj);
        //}

        public static DotNetObjectRef<T> ToObjectRef<T>(this T obj) where T : class
        {
            return DotNetObjectRef.Create<T>(obj);
        }
    }
}
