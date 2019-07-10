/* Ceated by Ya Lin. 2019/7/9 16:57:07 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thunder.Blazor.Components
{
    public class TBlockDropdownBase<TModel> : TBlockContextBase<TModel> where TModel: TNode<TModel>, new() 
    {
        public void ToggleDropdown()
        {
            Value.IsOpen = !Value.IsOpen;
        }

    }
}
