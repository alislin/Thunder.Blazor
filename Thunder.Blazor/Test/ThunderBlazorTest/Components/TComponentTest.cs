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
        TestContainer tca;
        TestComponent tcc;

        [SetUp]
        public  void Init()
        {
            tcc = new TestComponent();
            tca = new TestContainer { Caption = "OK" };
            tca.OnCommand += (o, e) =>
            {
                e = ContextResult.No("no");
                tca.ObjectName = "test";
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
            tcc.DataContext = tca;
            Assert.IsTrue(tcc.DataContext.Caption == tca.Caption);
            tca.OnCommand.Invoke(this, ContextResult.Cancel());
            Assert.IsTrue(tcc.DataContext.ObjectName == "test");
        }
    }

    class TestContent : TContext
    {
        public string Test { get; set; }
    }

    class TestComponent : TComponent<TestContainer>
    {

    }

    class TestContainer : TContainer<TestContent>
    {

    }
}
