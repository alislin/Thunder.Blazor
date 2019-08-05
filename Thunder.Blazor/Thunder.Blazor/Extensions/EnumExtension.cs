using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace Thunder.Blazor.Extensions
{
    public static class EnumExtension
    {
        /// <summary>
        /// 输出枚举为文本
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToDescriptionString(this Enum val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : val.ToString();
        }

        /// <summary>
        /// 数字转换为枚举
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TValue ToEnum<TValue>(this int value)
        {
            return (TValue)Enum.ToObject(typeof(TValue), value);
        }

        /// <summary>
        /// 文本转换为枚举
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TValue ToEnum<TValue>(this string value)
        {
            return (TValue)Enum.Parse(typeof(TValue), value);
        }

        /// <summary>
        /// CSS生成
        /// </summary>
        /// <param name="s"></param>
        /// <param name="add"></param>
        /// <returns></returns>
        public static string CssBuild(this string s,string add)
        {
            var result = s;
            result += $" {add}";
            return result;
        }

        /// <summary>
        /// CSS生成
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string CssBuild(this IList<string> s)
        {
            var result = "";
            result = string.Join(" ", s);
            return result;
        }

        /// <summary>
        /// 连接两个枚举值输出CSS
        /// </summary>
        /// <param name="val"></param>
        /// <param name="sub"></param>
        /// <param name="join"></param>
        /// <returns></returns>
        public static string CssBuild(this Enum val,Enum sub,string join = "-")
        {
            var result = $"{val.ToDescriptionString()}{join}{sub.ToDescriptionString()}";
            return result;

        }
    }
}
