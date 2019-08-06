using System;
using System.Collections.Generic;
using System.Linq;

namespace Thunder.Blazor.CssBuilder
{

    public class CssBuild
    {
        public List<string> CssList { get;protected set; } = new List<string>();
        public List<string> CssAdd { get; protected set; } = new List<string>();
        public List<string> CssRemove { get; protected set; } = new List<string>();

        public CssBuild Reset()
        {
            CssAdd = new List<string>();
            CssRemove = new List<string>();
            return this;
        }

        /// <summary>
        /// 添加 css 
        /// </summary>
        /// <param name="css"></param>
        /// <returns></returns>
        public CssBuild Add(string css)
        {
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
        public CssBuild Add(List<string> css)
        {
            CssAdd = CssAdd.Union(css).ToList();
            return this;
        }

        public CssBuild Remove(List<string> css)
        {
            CssRemove = CssRemove.Except(css).ToList();
            return this;
        }

        public CssBuild Remove(string css)
        {
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
        public string Build()
        {
            CssList = CssAdd.Except(CssRemove).ToList();
            var result = string.Join(" ", CssList);
            return result;
        }
    }

}
