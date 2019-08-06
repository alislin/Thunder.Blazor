/* Ceated by Ya Lin. 2019/8/6 15:16:11 */

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Libs;

namespace ThunderBlazorTest.Libs
{
    class Cssbuildtest
    {
        [Test]
        public void cssbuildtest()
        {
            var s1 = "alert alert-secondary mt-4";
            var css = CssBuild.New.Add(s1);
            var s2 = css.Build().CssString;

            //基础计算
            Assert.IsTrue(s1 == s2);
            //添加单个
            Assert.IsTrue("alert alert-secondary mt-4 btn" == css.Add("btn").Build().CssString);
            //移除单个
            Assert.IsTrue("alert mt-4 btn" == css.Remove("alert-secondary").Build().CssString);
            //添加队列
            Assert.IsTrue("alert mt-4 btn top-row px-4" == css.Add("top-row px-4").Build().CssString);
            //添加重复值
            Assert.IsTrue("alert mt-4 btn top-row px-4" == css.Add("top-row px-4").Build().CssString);
            Assert.IsTrue("alert mt-4 btn top-row px-4" == css.Add("").Build().CssString);
            //移除重复值
            Assert.IsTrue("alert mt-4 btn top-row px-4" == css.Remove("alert-secondary").Build().CssString);
            Assert.IsTrue("alert mt-4 btn top-row px-4" == css.Remove("").Build().CssString);
            //不存在的值
            Assert.IsTrue("alert mt-4 btn top-row px-4" == css.Remove("notthis").Build().CssString);
            //移除队列
            Assert.IsTrue("alert mt-4 btn" == css.Remove("top-row px-4").Build().CssString);
            

            //单个对比
            var listadd = new List<string>
            {
                "alert",
                "alert-secondary",
                "mt-4",
                "btn",
                "top-row",
                "px-4"
            };

            var listremove = new List<string>
            {
                "alert-secondary",
                "notthis",
                "top-row",
                "px-4"
            };

            var listCss = new List<string>
            {
                "alert",
                "mt-4",
                "btn",
            };

            css.CssAdd.ForEach(x => Assert.IsTrue(listadd.Contains(x)));
            css.CssRemove.ForEach(x => Assert.IsTrue(listremove.Contains(x)));
            css.CssList.ForEach(x => Assert.IsTrue(listCss.Contains(x)));

            Assert.IsTrue(css.AddNoList("cssok").Build().CssString == "alert mt-4 btn");
            Assert.IsTrue(css.AddOnHasList("notalone").Build().CssString == "alert mt-4 btn notalone");

            Assert.IsTrue(css.Reset().Build().CssString == "");
            Assert.IsTrue(css.AddOnHasList("notalone").Build().CssString == "");
            Assert.IsTrue(css.AddNoList("cssok").Build().CssString == "cssok");



        }
    }
}
