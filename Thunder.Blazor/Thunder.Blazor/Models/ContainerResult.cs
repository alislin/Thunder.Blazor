/* Ceated by Ya Lin. 2019/7/9 13:58:07 */

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thunder.Blazor.Models
{
    /// <summary>
    /// 上下文返回值类
    /// </summary>
    public class ContextResult
    {
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public Type DataType { get; set; }
        /// <summary>
        /// 返回值
        /// </summary>
        public ContextResultValue Result { get; set; }
        public bool Canceled => Result == ContextResultValue.Cancel;

        public ContextResult(object data, Type resultType, ContextResultValue result)
        {
            Data = data;
            DataType = resultType;
            Result = result;
        }

        public ContextResult()
        {
        }

        public static ContextResult Ok<T>(T result) => new ContextResult(result, typeof(T), ContextResultValue.OK);
        public static ContextResult Yes<T>(T result) => new ContextResult(result, typeof(T), ContextResultValue.Yes);
        public static ContextResult No<T>(T result) => new ContextResult(result, typeof(T), ContextResultValue.No);
        public static ContextResult Close<T>(T result) => new ContextResult(result, typeof(T), ContextResultValue.Close);
        public static ContextResult None<T>(T result) => new ContextResult(result, typeof(T), ContextResultValue.None);

        public static ContextResult Cancel() => new ContextResult(default, typeof(object), ContextResultValue.Cancel);
    }

    /// <summary>
    /// 返回值枚举
    /// </summary>
    public enum ContextResultValue
    {
        None,
        OK,
        Cancel,
        Close,
        Yes,
        No
    }

    /// <summary>
    /// 上下文委托类
    /// </summary>
    public class ContextAction
    {
        public ContextAction(string text, ContextResultValue result, Action<ContextAction> action,bool disposed=true)
        {
            Text = text;
            Result = result;
            Action = action;
            Disposed = disposed;
        }

        /// <summary>
        /// 文本（如果需要显示）
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 操作返回值
        /// </summary>
        public ContextResultValue Result { get; set; }
        /// <summary>
        /// 关闭标志
        /// </summary>
        public bool Disposed { get; set; } = true;
        /// <summary>
        /// 操作委托
        /// </summary>
        public Action<ContextAction> Action { get; set; }
        /// <summary>
        /// 后续操作委托
        /// </summary>
        public Action<ContextAction,object> ContinueAction { get; set; }
        /// <summary>
        /// 操作返回对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public ContextResult ContextResult<T>(T data)
        {
            return new ContextResult(data, typeof(T), Result);
        }
    }
}
