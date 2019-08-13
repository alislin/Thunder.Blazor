/* Ceated by Ya Lin. 2019/8/13 16:51:50 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thunder.Blazor.Components
{
    public class TTab : TComponentContainer<TTabContext>
    {
        private List<TTabItem> showItems;
        private TNode dropdownItems;
        private bool showMore;
        private TTabItem moreActivedItem;
        private TNavContext headers;

        /// <summary>
        /// 最大显示标签数
        /// </summary>
        [Parameter] public int ShowCount { get; set; } = 4;


        public bool HasItems => (DataContext?.TabsItems.Count ?? 0) > 0;
        protected bool ShowMore { get => showMore; set => showMore = value; }

        protected List<TTabItem> ShowItems { get => showItems; set => showItems = value; }
        protected TNode DropdownItems { get => dropdownItems; set => dropdownItems = value; }
        protected TTabItem MoreActivedItem { get => moreActivedItem; set => moreActivedItem = value; }
        protected TNavContext Headers { get => headers; set => headers = value; }

        protected override void OnInit()
        {
            base.OnInit();
            showMore = UpdateItems();
        }

        public override void LoadItem(object obj)
        {
            var item = (TTabItem)obj;
            var r = DataContext?.TabsItems?.FirstOrDefault(x => x.Id == item.Id);
            if (r != null) return;
            var max = 0;
            if (HasItems) max = DataContext.TabsItems.Max(x => x.Index) + 1;
            item.Index = max;
            DataContext.TabsItems.Add(item);
            SetActive(item.Id);
            showMore = UpdateItems();
            StateHasChanged();
        }

        public override void CloseItem(object item)
        {
            var k = DataContext.TabsItems.RemoveAll(x => x.Id == ((TTabItem)item).Id);
            showMore = UpdateItems();
            StateHasChanged();
        }

        private bool UpdateItems()
        {
            headers = new TNavContext();
            moreActivedItem = null;
            DataContext.TabsItems = DataContext?.TabsItems.OrderBy(x => x.Index).ToList();
            foreach (var item in DataContext.TabsItems)
            {
                item.CommandAction = () => TabClick(null);
            }
            var first = DataContext?.TabsItems.FirstOrDefault(x => x.IsActived);
            if (first == null)
            {
                first = DataContext?.TabsItems.FirstOrDefault();
                if (first != null)
                {
                    first.IsActived = true;
                    first.Header.IsActived = true;
                }
            }

            var showCount = ShowCount;
            var count = DataContext?.TabsItems?.Count ?? 0;
            if (count == ShowCount + 1)
            {
                showCount = count;
            }

            showItems = DataContext?.TabsItems.Take(showCount).ToList();
            headers.NavItems.AddRange(showItems.Select(x => x.Header));
            if (!HasItems || DataContext.TabsItems.Count <= showCount) return false;

            dropdownItems = new TNode
            {
                Text = "更多",
            };
            dropdownItems.ChildNodes = DataContext?.TabsItems.Skip(showCount).Select(x =>
             new TagBlockContext
             {
                 Id = x.Id,
                 Index = x.Index,
                 Caption = x.Header.Caption,
                 IsActived = x.IsActived,
                 CommandAction = () =>
                 {
                     SetActive(x.Id);
                 }
             }).ToList();
            headers.NavItems.Add(dropdownItems);

            dropdownItems.IsActived = dropdownItems.ChildNodes.FirstOrDefault(x => x.IsActived) != null;
            if (dropdownItems.IsActived)
            {
                moreActivedItem = DataContext.TabsItems.FirstOrDefault(x => x.IsActived);
                headers.NavItems.Add(moreActivedItem.Header);
            }

            

            return true;
        }

        protected void SetActive(Guid id)
        {
            foreach (var item in DataContext.TabsItems)
            {
                item.IsActived = false;
                item.Header.IsActived = false;
            }
            var r = DataContext.TabsItems.FirstOrDefault(x => x.Id == id);
            if (r != null)
            {
                r.IsActived = true;
                r.Header.IsActived = true;
            }
        }

        protected void TabClick(object obj)
        {
            var v = (TTabItem)obj;
            SetActive(v.Id);
            Update();
        }

        protected void DropTabClick(object obj)
        {
            var v = (TagBlockContext)obj;
            SetActive(v.Id);
            Update();
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
    }

    public class TTabContext : TContainer
    {
        public List<TTabItem> TabsItems { get; set; } = new List<TTabItem>();
    }

    public class TTabItem : TContext
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 索引
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 是否可以关闭
        /// </summary>
        public bool CanClosed { get; set; }
        /// <summary>
        /// 标签头部
        /// </summary>
        public TagBlockContext Header { get; set; }
    }
}
