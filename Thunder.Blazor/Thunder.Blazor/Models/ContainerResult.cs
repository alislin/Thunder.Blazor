/* Ceated by Ya Lin. 2019/7/9 13:58:07 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Thunder.Blazor.Models
{
    public class ContextResult
    {
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public Type DataType { get; }
        /// <summary>
        /// 返回值
        /// </summary>
        public ContextResultValue Result { get; set; }
        public bool Cancelled => Result == ContextResultValue.Cancel;

        public ContextResult(object data, Type resultType, ContextResultValue result)
        {
            Data = data;
            DataType = resultType;
            Result = result;
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

}
