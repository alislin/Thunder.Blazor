using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using Thunder.Blazor.Libs;

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
        public static string Css(this string s, string add)
            => CssBuild.New
            .Add(s)
            .Add(add)
            .Build().CssString;

        /// <summary>
        /// CSS生成
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Css(this IList<string> s)
            => CssBuild.New
            .Add(s)
            .Build().CssString;

        /// <summary>
        /// 连接两个枚举值输出CSS
        /// </summary>
        /// <param name="val"></param>
        /// <param name="sub"></param>
        /// <param name="join"></param>
        /// <returns></returns>
        public static string Css(this Enum val, Enum sub, string join = "-")
            => val.ToDescriptionString().Css(sub.ToDescriptionString(), "-");

        /// <summary>
        /// 连接三个枚举值输出CSS
        /// </summary>
        /// <param name="val"></param>
        /// <param name="sub"></param>
        /// <param name="join"></param>
        /// <returns></returns>
        public static string Css(this Enum val, Enum sub, Enum sub2, string join = "-")
            => val.ToDescriptionString().Css(sub.ToDescriptionString(), sub2.ToDescriptionString(), "-");

        /// <summary>
        /// 连接两个字串输出CSS
        /// </summary>
        /// <param name="val"></param>
        /// <param name="sub"></param>
        /// <param name="join"></param>
        /// <returns></returns>
        public static string Css(this string val, string sub, string join = "-")
            => val.Css(sub, "", join);

        public static string Css(this string val, string sub,string sub2, string join = "-")
        {
            var list = CssBuild.New.Add(val)
                .Add(sub)
                .Add(sub2)
                .CssAdd;
            if (list.Count>1)
            {
                return string.Join(join, list);
            }
            else
            {
                return string.Empty;
            }
        }

        public static CssBuild Add(this CssBuild css, Enum val, bool condition = true)
            => css.Add(val.ToDescriptionString(), condition);
    }
}
