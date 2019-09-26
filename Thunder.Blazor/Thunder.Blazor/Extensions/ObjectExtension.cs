/* Ceated by Ya Lin. 2019/9/26 11:27:20 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Thunder.Blazor.Extensions
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 类型转换
        /// </summary>
        /// <typeparam name="T">转换的类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="nullOrTypeErrorThrow">空值或者类型错误抛出错误（默认：true）</param>
        /// <param name="customErrorMessage">定制异常消息</param>
        /// <returns></returns>
        public static T ToType<T>(this object obj,bool nullOrTypeErrorThrow=true,string customErrorMessage=null)
        {
            if (obj == null)
            {
                if (nullOrTypeErrorThrow)
                {
                    var err = string.IsNullOrWhiteSpace(customErrorMessage) ? "obj is null." : customErrorMessage;
                    throw new NullReferenceException(err);
                }
                return default;
            }
            if (obj is T)
            {
                return (T)obj;
            }
            else
            {
                if (nullOrTypeErrorThrow)
                {
                    var err = string.IsNullOrWhiteSpace(customErrorMessage) ? $"obj can not convert to type {typeof(T).Name}." : customErrorMessage;
                    throw new ArgumentException(err);
                }
                
            }
            return default;
        }
    }
}
