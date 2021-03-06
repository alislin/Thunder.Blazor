﻿using System.Collections.Generic;
using System.Linq;

namespace Thunder.Blazor.Libs
{

    public class CssBuild
    {
        public static CssBuild New => new CssBuild();

        public List<string> CssList { get; } = new List<string>();
        public List<string> CssAdd { get;  } = new List<string>();
        public List<string> CssRemove { get;  } = new List<string>();

        public string CssString => string.Join(" ", CssList);

        public CssBuild Reset()
        {
            CssAdd.Clear();
            CssRemove.Clear();
            return this;
        }

        /// <summary>
        /// 如果已经存在值就添加
        /// </summary>
        /// <param name="css"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public CssBuild AddOnHasList(string css, bool condition = true)
        {
            if (!condition) return this;
            Build();
            if (CssList.Count == 0) return this;

            return Add(css);
        }

        /// <summary>
        /// 如果已经没有值就添加
        /// </summary>
        /// <param name="css"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public CssBuild AddNoList(string css, bool condition = true)
        {
            if (!condition) return this;
            Build();
            if (CssList.Count > 0) return this;

            return Add(css);
        }

        /// <summary>
        /// 添加 css 
        /// </summary>
        /// <param name="css"></param>
        /// <returns></returns>
        public CssBuild Add(string css, bool condition = true)
        {
            if (!condition || string.IsNullOrWhiteSpace(css)) return this;
            var csslist = new List<string>();
            var list = css.Split(' ');
            foreach (var item in list)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    csslist.Add(item.Trim());
                }
            }
            return Add(csslist);
        }

        /// <summary>
        /// 添加 css列表
        /// </summary>
        /// <param name="css"></param>
        /// <returns></returns>
        public CssBuild Add(IList<string> css, bool condition = true)
        {
            if (!condition) return this;
            var list = CssAdd.Union(css).ToList();
            CssAdd.Clear();
            CssAdd.AddRange(list);
            return this;
        }

        /// <summary>
        /// 移除 Css 列表
        /// </summary>
        /// <param name="css"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public CssBuild Remove(IList<string> css, bool condition = true)
        {
            if (!condition) return this;
            var list = CssRemove.Union(css).ToList();
            CssRemove.Clear();
            CssRemove.AddRange(list);
            return this;
        }

        /// <summary>
        /// 移除 Css 列表
        /// </summary>
        /// <param name="css"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public CssBuild Remove(string css, bool condition = true)
        {
            if (!condition || string.IsNullOrWhiteSpace(css)) return this;
            var csslist = new List<string>();
            var list = css.Split(' ');
            foreach (var item in list)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    csslist.Add(item.Trim());
                }
            }
            return Remove(csslist);
        }

        /// <summary>
        /// 开关 Css 项目
        /// </summary>
        /// <param name="css"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public CssBuild Toggle(IList<string> css, bool condition = true)
        {
            if (!condition) return this;
            var cRemove = CssAdd.Intersect(css).ToList();
            var cAdd = CssRemove.Intersect(css).ToList();
            CssAdd.AddRange(cAdd);
            CssRemove.AddRange(cRemove);
            return this;
        }

        /// <summary>
        /// 生成CSS字串
        /// </summary>
        /// <returns></returns>
        public CssBuild Build()
        {
            var list = CssAdd.Except(CssRemove).ToList();
            CssList.Clear();
            CssList.AddRange(list);
            return this;
        }
    }

}
