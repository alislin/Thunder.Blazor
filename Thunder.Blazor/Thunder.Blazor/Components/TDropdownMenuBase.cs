/* Ceated by Ya Lin. 2019/7/9 16:57:07 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thunder.Blazor.Components
{
    public class TDropdownMenuBase<TModel> : TBlockDropContextBase<TModel> where TModel: TNode<TModel>, new() 
    {
        public void ToggleDropdown()
        {
            Value.IsOpen = !Value.IsOpen;
        }

    }

    public abstract class TDropdownInfo : TNavItemInfo
    {
        public bool IsOpen { get; set; }
        public bool ShowBadgeCount { get; set; }
        public TDropdownInfo Parent { get; set; }
        public abstract void Close();
    }

    public class TDropdownItem1<TModel>:TDropdownInfo where TModel : TDropdownInfo
    {
        public List<TModel> Items { get; set; } = new List<TModel>();
        public bool HasChildren => (Items?.Count ?? 0) > 0;

        public override void Close()
        {
            IsOpen = false;
            Parent?.Close();
        }

        public void Add(TModel child, Action action = null)
        {
            child.Parent = this;
            Items.Add(child);
        }
    }
}
