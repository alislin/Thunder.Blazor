/* Ceated by Ya Lin. 2019/8/13 16:51:50 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Thunder.Blazor.Components
{
    public class TTab : TComponentContainer<TTabContext>
    {
        private List<TTabItem> showItems;
        private Node<TagBlockContext> dropdownItems;
        private bool showMore;
        private TTabItem moreActivedItem;
        private TNavContext headers = new TNavContext();

        /// <summary>
        /// 最大显示标签数
        /// </summary>
        [Parameter] public int ShowCount { get; set; } = 4;


        public bool HasItems => (View?.TabsItems.Count ?? 0) > 0;
        protected bool ShowMore { get => showMore; set => showMore = value; }

        protected List<TTabItem> ShowItems { get => showItems; set => showItems = value; }
        protected Node<TagBlockContext> DropdownItems { get => dropdownItems; set => dropdownItems = value; }
        protected TTabItem MoreActivedItem { get => moreActivedItem; set => moreActivedItem = value; }
        protected TNavContext Headers { get => headers; set => headers = value; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            showMore = UpdateHeads();
        }

        public override void LoadItem(object obj)
        {
            if (obj == null)
            {
                return;
            }
            var item = (TTabItem)obj;
            var r = View?.TabsItems?.FirstOrDefault(x => x.Id == item.Id);
            if (r != null) return;
            var max = 0;
            if (HasItems) max = View.TabsItems.Max(x => x.Index) + 1;
            item.Index = max;
            View.TabsItems.Add(item);
            SetActive(item.Id);
        }

        public override void CloseItem(object item)
        {
            var k = View.TabsItems.RemoveAll(x => x.Id == ((TTabItem)item).Id);
            showMore = UpdateHeads();
            this.InvokeAsync(StateHasChanged);
        }

        private bool UpdateHeads()
        {
            headers.NavItems.Clear();
            moreActivedItem = null;
            var orderList = View?.TabsItems.OrderBy(x => x.Index).ToList();
            View.TabsItems.Clear();
            view.TabsItems.AddRange(orderList);
            foreach (var item in View.TabsItems)
            {
                item.CommandAction = (obj) => TabClick(item);
            }
            //LoadDataContext();

            var first = View?.TabsItems.FirstOrDefault(x => x.IsActived);
            if (first == null)
            {
                first = View?.TabsItems.FirstOrDefault();
                if (first != null)
                {
                    first.IsActived = true;
                    //first.Header.IsActived = true;
                }
            }

            var showCount = ShowCount;
            var count = View?.TabsItems?.Count ?? 0;
            if (count == ShowCount + 1)
            {
                showCount = count;
            }

            showItems = View?.TabsItems.Take(showCount).ToList();
            headers.NavItems.AddRange(showItems.Select(x => (TagBlockContext)x));
            if (!HasItems || View.TabsItems.Count <= showCount) return false;
            dropdownItems = new Node<TagBlockContext>(new TagBlockContext { Caption = "更多" });

            var menus = View?.TabsItems.Skip(showCount).Select(x =>
               new TagBlockContext
               {
                   Id = x.Id,
                   Index = x.Index,
                   Caption = x.Caption,
                   IsActived = x.IsActived,
                   CommandAction = (obj) =>
                   {
                       SetActive(x.Id);
                   }
               }
             ).ToList();
            dropdownItems.Add(menus);
            headers.NavItems.Add(dropdownItems);

            dropdownItems.IsActived = dropdownItems.ChildNodes.FirstOrDefault(x => x.IsActived) != null;
            if (dropdownItems.IsActived)
            {
                moreActivedItem = View.TabsItems.FirstOrDefault(x => x.IsActived);
                headers.NavItems.Add(moreActivedItem);
            }

            return true;
        }

        protected void SetActive(Guid id)
        {
            foreach (var item in View.TabsItems)
            {
                item.IsActived = false;
            }
            var r = View.TabsItems.FirstOrDefault(x => x.Id == id);
            if (r != null)
            {
                r.IsActived = true;
            }

            showMore = UpdateHeads();
            this.InvokeAsync(StateHasChanged);
        }

        protected void TabClick(object item)
        {
            var v = (TTabItem)item;
            SetActive(v?.Id??Guid.Empty);
        }

        protected void DropTabClick(object item)
        {
            var v = (TagBlockContext)item;
            SetActive(v?.Id ?? Guid.Empty);
        }

        protected void TabClose(object item)
        {
            var v = (TTabItem)item;
            CloseItem(v);
        }

        public override void ShowItem(object item)
        {
            LoadItem((TTabItem)item);
        }

        public override void LoadDataContext()
        {
            base.LoadDataContext();
            showMore = UpdateHeads();
        }

        public override void Cancel()
        {
            throw new NotImplementedException();
        }

        public override void Load(object item)
        {
            LoadItem((TTabItem)item);
        }
    }

    public class TTabContext : TContainer
    {
        public List<TTabItem> TabsItems { get; } = new List<TTabItem>();
    }

    public class TTabItem : TagBlockContext
    {
        public TTabItem()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// 是否可以关闭
        /// </summary>
        public bool CanClosed { get; set; }
        /// <summary>
        /// 标签头部
        /// </summary>
        //public TagBlockContext Header { get; set; }
    }
}
