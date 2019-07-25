/* Ceated by Ya Lin. 2019/7/24 16:13:42 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Thunder.Blazor.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// 使用 .netcore 内置 json 转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        /// <summary>
        /// 使用 .netcore 内置 json 转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <returns></returns>
        public static T FromJson<T>(this string src)
        {
            return JsonSerializer.Deserialize<T>(src);
        }
    }
}
