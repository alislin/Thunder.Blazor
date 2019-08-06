using System;
using System.Collections.Generic;
using System.Linq;

namespace Thunder.Blazor.Libs
{

    public class CssBuild
    {
        public static CssBuild New => new CssBuild();

        public List<string> CssList { get;protected set; } = new List<string>();
        public List<string> CssAdd { get; protected set; } = new List<string>();
        public List<string> CssRemove { get; protected set; } = new List<string>();

        public string CssString=> string.Join(" ", CssList);

        public CssBuild Reset()
        {
            CssAdd = new List<string>();
            CssRemove = new List<string>();
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
            if (CssList.Count==0) return this;

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
            if (!condition) return this;
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
            CssAdd = CssAdd.Union(css).ToList();
            return this;
        }

        public CssBuild Remove(IList<string> css, bool condition = true)
        {
            if (!condition) return this;
            CssRemove = CssRemove.Union(css).ToList();
            return this;
        }

        public CssBuild Remove(string css, bool condition = true)
        {
            if (!condition) return this;
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
        /// 生成CSS字串
        /// </summary>
        /// <returns></returns>
        public CssBuild Build()
        {
            CssList = CssAdd.Except(CssRemove).ToList();
            return this;
        }
    }

}
