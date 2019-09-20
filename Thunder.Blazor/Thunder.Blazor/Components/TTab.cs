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


        public bool HasItems => (DataContext?.TabsItems.Count ?? 0) > 0;
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
            var r = DataContext?.TabsItems?.FirstOrDefault(x => x.Id == item.Id);
            if (r != null) return;
            var max = 0;
            if (HasItems) max = DataContext.TabsItems.Max(x => x.Index) + 1;
            item.Index = max;
            DataContext.TabsItems.Add(item);
            SetActive(item.Id);
        }

        public override void CloseItem(object item)
        {
            var k = DataContext.TabsItems.RemoveAll(x => x.Id == ((TTabItem)item).Id);
            showMore = UpdateHeads();
            this.InvokeAsync(StateHasChanged);
        }

        private bool UpdateHeads()
        {
            headers.NavItems.Clear();
            moreActivedItem = null;
            DataContext.TabsItems = DataContext?.TabsItems.OrderBy(x => x.Index).ToList();
            foreach (var item in DataContext.TabsItems)
            {
                item.CommandAction = () => TabClick(item);
            }
            //LoadDataContext();

            var first = DataContext?.TabsItems.FirstOrDefault(x => x.IsActived);
            if (first == null)
            {
                first = DataContext?.TabsItems.FirstOrDefault();
                if (first != null)
                {
                    first.IsActived = true;
                    //first.Header.IsActived = true;
                }
            }

            var showCount = ShowCount;
            var count = DataContext?.TabsItems?.Count ?? 0;
            if (count == ShowCount + 1)
            {
                showCount = count;
            }

            showItems = DataContext?.TabsItems.Take(showCount).ToList();
            headers.NavItems.AddRange(showItems.Select(x => (TagBlockContext)x));
            if (!HasItems || DataContext.TabsItems.Count <= showCount) return false;

            dropdownItems = new Node<TagBlockContext>(new TagBlockContext { Caption = "更多" });

            var menus = DataContext?.TabsItems.Skip(showCount).Select(x =>
               new TagBlockContext
               {
                   Id = x.Id,
                   Index = x.Index,
                   Caption = x.Caption,
                   IsActived = x.IsActived,
                   CommandAction = () =>
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
                moreActivedItem = DataContext.TabsItems.FirstOrDefault(x => x.IsActived);
                headers.NavItems.Add(moreActivedItem);
            }

            return true;
        }

        protected void SetActive(Guid id)
        {
            foreach (var item in DataContext.TabsItems)
            {
                item.IsActived = false;
            }
            var r = DataContext.TabsItems.FirstOrDefault(x => x.Id == id);
            if (r != null)
            {
                r.IsActived = true;
            }

            showMore = UpdateHeads();
            this.InvokeAsync(StateHasChanged);
        }

        protected void TabClick(object obj)
        {
            var v = (TTabItem)obj;
            SetActive(v.Id);
        }

        protected void DropTabClick(object obj)
        {
            var v = (TagBlockContext)obj;
            SetActive(v.Id);
        }

        protected void TabClose(object obj)
        {
            var v = (TTabItem)obj;
            CloseItem(v);
        }

        public override void ShowItem(object value)
        {
            LoadItem((TTabItem)value);
        }

        public override void LoadDataContext()
        {
            base.LoadDataContext();
            showMore = UpdateHeads();
        }

        public override void Show(object item)
        {
            LoadItem((TTabItem)item);
        }

        public override void Cancel()
        {
            throw new NotImplementedException();
        }

        public override void Close(object item)
        {
            TabClose(item);
        }

        public override void Load(object item)
        {
            LoadItem((TTabItem)item);
        }
    }

    public class TTabContext : TContainer
    {
        public List<TTabItem> TabsItems { get; set; } = new List<TTabItem>();
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
