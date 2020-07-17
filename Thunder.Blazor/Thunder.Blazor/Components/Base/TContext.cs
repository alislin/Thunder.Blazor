/* Ceated by Ya Lin. 2019/7/11 14:52:29 */

using Microsoft.AspNetCore.Components;
using System;
using Thunder.Blazor.Models;

namespace Thunder.Blazor.Components
{
    /// <summary>
    /// 组件数据 (ViewModel)
    /// </summary>
    public class TContext : NotifyChanged
    {
        /// <summary>
        /// 组件类型
        /// </summary>
        public virtual Type ContextType { get; set; }
        /// <summary>
        /// 子组件数据
        /// </summary>
        public TContext Child { get; set; }
        /// <summary>
        /// 自定义对象
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// 组件参数(级联传入)
        /// </summary>
        public ComponentParamenter ContextParameters => GetParamenter();
        /// <summary>
        /// 生成区块
        /// </summary>
        public RenderFragment ContextFragment => new RenderFragment(x => { x.OpenComponent(1, ContextType); x.CloseComponent(); });

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName => this.GetType().Name;
        /// <summary>
        /// 参数Key，为空值时默认使用TypeName
        /// </summary>
        public string ParameterKey { get; set; }

        /// <summary>
        /// 自动生成参数
        /// </summary>
        /// <returns></returns>
        private ComponentParamenter GetParamenter()
        {
            var key = string.IsNullOrWhiteSpace(ParameterKey) ? TypeName : ParameterKey;
            var p = new ComponentParamenter(key, this);
            return p;
        }
    }

    /// <summary>
    /// 指定前端对象
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public class TContext<TView> : TContext
    {
        public TView View { get; set; }

        public TContext()
        {
        }

        public TContext(TView view)
        {
            View = view;
        }

        public override Type ContextType => typeof(TView);
    }

}
