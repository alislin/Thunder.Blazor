/* Ceated by Ya Lin. 2019/7/10 10:42:48 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// 基础标签数据
    /// </summary>
    public class TagBlockContext : TContext
    {
        public Guid Id { get; set; }
        public int Index { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public int Count { get; set; }

        //public virtual void Close() { }
    }

    /// <summary>
    /// 基础节点数据
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class TNodeBase<TModel>:TagBlockContext, INode<TModel> where TModel : TagBlockContext
    {
        public TModel ParentNode { get; set; }
        public IList<TModel> ChildNodes { get; set; } = new List<TModel>();
        public bool HasChildren => (ChildNodes?.Count ?? 0) > 0;

        public bool IsOpen { get; set; }
    }

    /// <summary>
    /// 多级节点数据
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class TNode<TModel> : TNodeBase<TModel> where TModel:TNode<TModel>
    {
        public void Add(TModel child, Action action = null)
        {
            child.ParentNode = (TModel)this;
            ChildNodes.Add(child);
        }

        public void Close()
        {
            IsOpen = false;
            ParentNode?.Close();
        }
    }


}
