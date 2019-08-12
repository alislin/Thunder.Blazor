using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
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
        /// <returns></returns>
        public static string Css(this IList<string> s)
            => CssBuild.New
            .Add(s)
            .Build().CssString;

        /// <summary>
        /// CSS生成
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Css(this string s,string s1)
            => CssBuild.New
            .Add(s)
            .Add(s1)
            .Build().CssString;

        /// <summary>
        /// 连接两个枚举值输出CSS
        /// </summary>
        /// <param name="val"></param>
        /// <param name="sub"></param>
        /// <param name="join"></param>
        /// <returns></returns>
        public static string CssJoin(this Enum val, Enum sub, string join = "-")
            => join.Join(val.ToDescriptionString(), sub.ToDescriptionString());

        /// <summary>
        /// 连接三个枚举值输出CSS
        /// </summary>
        /// <param name="val"></param>
        /// <param name="sub"></param>
        /// <param name="join"></param>
        /// <returns></returns>
        public static string CssJoin(this Enum val, Enum sub, Enum sub2, string join = "-")
            => join.Join(val.ToDescriptionString(), sub.ToDescriptionString(), sub2.ToDescriptionString());

        /// <summary>
        /// 连接字串队列
        /// </summary>
        /// <param name="joinchar"></param>
        /// <param name="list"></param>
        /// <param name="skipEmpty">如果队列存在空值，返回空值</param>
        /// <returns></returns>
        public static string Join(this string joinchar,IList<string> list,bool skipEmpty = true)
        {
            var check = list.Where(x => string.IsNullOrWhiteSpace(x)).Count();
            var l = list.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            if (check>0)
            {
                return string.Empty;
            }
            else
            {
                return string.Join(joinchar, l);
            }
        }

        public static string Join(this string joinchar, string s1, string s2)
            => joinchar.Join(new string[] { s1, s2 });

        public static string Join(this string joinchar, string s1, string s2, string s3)
            => joinchar.Join(new string[] { s1, s2, s3 });

        public static string Join(this string joinchar, string s1, string s2, string s3, string s4)
            => joinchar.Join(new string[] { s1, s2, s3, s4 });

        public static string Join(this string joinchar, string s1, string s2, string s3, string s4, string s5)
            => joinchar.Join(new string[] { s1, s2, s3, s4,s5 });


        public static CssBuild Add(this CssBuild css, Enum val, bool condition = true)
            => css.Add(val.ToDescriptionString(), condition);
    }
}
