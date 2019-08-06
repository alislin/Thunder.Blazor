/* Ceated by Ya Lin. 2019/7/25 15:52:32 */

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Thunder.Blazor.Components;
using Thunder.Blazor.Models;

namespace ThunderBlazorTest.Components
{
    class TComponentTest
    {
        TestContent tc = new TestContent { Test = "ok" };
        TestComponent tcc;

        [SetUp]
        public  void Init()
        {
            tcc = new TestComponent();
            tc = new TestContent { Caption = "OK" };
            tc.OnCommand += (o, e) =>
            {
                e = ContextResult.No("no");
                tc.ObjectName = "test";
                tc.LoadDataContext();
            };
        }

        [Test]
        public void InitComponent()
        {
            Assert.IsNotNull(tcc.DataContext);
        }

        [Test]
        public void InitContent()
        {
            tcc.DataContext = tc;
            Assert.IsTrue(tcc.DomId == tc.DomId);
            Assert.IsTrue(tcc.DataContext.Caption == tc.Caption);
            tcc.Caption = "change 1";
            Assert.IsTrue(tcc.Caption == tcc.DataContext.Caption);
            tc.OnCommand.Invoke(this, ContextResult.Cancel());
            Assert.IsTrue(tcc.DataContext.ObjectName == "test");
        }
    }

    class TestContent : TContainer
    {
        public string Test { get; set; }
    }

    class TestComponent : TComponentContainer<TestContent>
    {

    }
}
