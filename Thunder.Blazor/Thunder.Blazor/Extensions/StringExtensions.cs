/* Ceated by Ya Lin. 2019/7/24 16:13:42 */
#define NET_CORE
using System;
using System.Collections.Generic;
using System.Text;
#if NET_CORE
using System.Text.Json;
#else
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
#endif

namespace Thunder.Blazor.Extensions
{
    public static class StringExtensions
    {
#if NET_CORE
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

#else
        private static JsonSerializerSettings settings;
        static StringExtensions()
        {
            IsoDateTimeConverter datetimeConverter = new IsoDateTimeConverter();
            datetimeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

            settings = new JsonSerializerSettings();
            settings.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            settings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            settings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            settings.Converters.Add(datetimeConverter);
        }

        public static string ToJson(this object objValue)
        {
            return JsonConvert.SerializeObject(objValue, Formatting.None, settings);
        }

        public static T FromJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

#endif
    }
}
