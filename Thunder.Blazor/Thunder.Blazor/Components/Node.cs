/* Ceated by Ya Lin. 2019/7/10 10:42:48 */

using System;
using System.Collections.Generic;

namespace Thunder.Blazor.Components
{
    public class Node<TModel>:TagBlockContext where TModel : TagBlockContext,new()
    {
        private TModel context = new TModel();

        public Node()
        {
        }

        public Node(TModel content)
        {
            SetValue(content);
        }

        public TModel Context { get => context; set => SetValue(value); }

        public IList<Node<TModel>> ChildNodes { get; set; } = new List<Node<TModel>>();
        public bool HasChildNodes => (ChildNodes?.Count ?? 0) > 0;
        public bool IsOpen { get; set; }
        public Action Close { get; set; } = () => { };
        public void Add(Node<TModel> child, Action action = null)
        {
            ChildNodes.Add(child);
        }

        public void Add(TModel item,Action action = null)
        {
            var child = new Node<TModel>(item);
            ChildNodes.Add(child);
        }

        public void Add(IList<TModel> childs)
        {
            foreach (var item in childs)
            {
                Add(item);
            }
        }

        public virtual void SetValue(TModel value)
        {
            context = value;
            Id = value.Id;
            Index = value.Index;
            Text = value.Text;
            Icon = value.Icon;
            Count = value.Count;
            DomId = value.DomId;
            Caption = value.Caption;
            ContextType = value.ContextType;
            Child = value.Child;
            ObjectName = value.ObjectName;
            Tag = value.Tag;
            IsVisabled = value.IsVisabled;
            IsActived = value.IsActived;
            IsEnabled = value.IsEnabled;
            CommandAction = value.CommandAction;
            StateHasChanged = value.StateHasChanged;
            UpdateDataContext = value.UpdateDataContext;
            LoadDataContext = value.LoadDataContext;
            AttachmentInfo = value.AttachmentInfo;
            BadgeInfo = value.BadgeInfo;
        }
    }
}
