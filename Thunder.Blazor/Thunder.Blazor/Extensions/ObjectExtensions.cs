/* Ceated by Ya Lin. 2019/9/26 17:47:55 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Thunder.Blazor.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// 空值检查
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objValue"></param>
        public static void NullCheck<T>(this T objValue)
        {
            if (objValue == null)
            {
                throw new ArgumentNullException(nameof(objValue));
            }
        }

        /// <summary>
        /// 空值检查（链式编程）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objValue"></param>
        /// <returns>原始对象</returns>
        public static T NotNull<T>(this T objValue)
        {
            objValue.NullCheck();
            return objValue;
        }
    }
}
