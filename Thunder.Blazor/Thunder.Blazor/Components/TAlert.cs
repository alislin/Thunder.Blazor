﻿/* Ceated by Ya Lin. 2019/7/11 14:18:28 */

using System;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// Alert 组件
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class TAlert<TModel> : TComponentContainer<TModel> where TModel : TAlertContext, new()
    {
        public override void Cancel()
        {
            throw new NotImplementedException();
        }

        public override void Close(object item = null)
        {
            throw new NotImplementedException();
        }

        public override void Load(object item = null)
        {
            throw new NotImplementedException();
        }

        public override void Show(object item = null)
        {
            throw new NotImplementedException();
        }
    }

    public class TAlertContext : TContainer
    {
        public bool EnableCloseButton { get; set; }
    }

}
