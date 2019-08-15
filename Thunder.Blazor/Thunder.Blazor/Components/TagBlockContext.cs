/* Ceated by Ya Lin. 2019/7/10 10:42:48 */

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Thunder.Standard.Lib.Extension;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// 基础标签数据
    /// </summary>
    public class TagBlockContext : TContext
    {
        public string Icon { get; set; }
        public int Count { get; set; }
    }

    /// <summary>
    /// 多级节点标签
    /// </summary>
    public class TagBlockNode : Node<TagBlockContext>
    {

    }

    /// <summary>
    /// 基础节点数据
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class TNodeBase<TModel>:TagBlockContext, INode<TModel> where TModel : TagBlockContext
    {
        public TModel ParentNode { get; set; }
        public IList<TModel> ChildNodes { get; set; } = new List<TModel>();
        public bool HasChildNodes => (ChildNodes?.Count ?? 0) > 0;

        public bool IsOpen { get; set; }

    }

    /// <summary>
    /// 多级节点数据
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class TNode<TModel> : TNodeBase<TModel> where TModel:TNode<TModel>
    {
        public Action Close { get; set; } = () => { };
        public void Add(TModel child, Action action = null)
        {
            child.ParentNode = (TModel)this;
            ChildNodes.Add(child);
        }
    }

    public class TNode : TNode<TNode>
    {

        public TNode()
        {
        }

        //public virtual void Add<T>(T node) where T : TagBlockContext
        //{
        //    if (node.HasChildNodes)
        //    {
        //        foreach (var item in node.ChildNodes)
        //        {

        //        }
        //    }
        //}

        //protected virtual TNode Load<T>(T node) where T : TagBlockContext
        //{
        //    var result = new TagBlockContext();
        //    if (node.HasChildNodes)
        //    {
        //        foreach (var item in node.ChildNodes)
        //        {
        //            result.
        //        }
        //    }

        //    return result;
        //}
    }

    public static class TNodeExt
    {
        public static TNode ToNode<T>(this T obj) where T : TagBlockContext
        {
            var node = TransExp<T, TNode>.Trans(obj);
            return node;
        }
    }

    public static class TransExp<TIn, TOut>
    {

        private static readonly Func<TIn, TOut> cache = GetFunc();
        private static Func<TIn, TOut> GetFunc()
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TIn), "p");
            List<MemberBinding> memberBindingList = new List<MemberBinding>();

            foreach (var item in typeof(TOut).GetProperties())
            {
                if (!item.CanWrite)
                    continue;

                MemberExpression property = Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                MemberBinding memberBinding = Expression.Bind(item, property);
                memberBindingList.Add(memberBinding);
            }

            MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList.ToArray());
            Expression<Func<TIn, TOut>> lambda = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, new ParameterExpression[] { parameterExpression });

            return lambda.Compile();
        }

        public static TOut Trans(TIn tIn)
        {
            return cache(tIn);
        }

    }

}
