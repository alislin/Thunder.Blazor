/* Ceated by Ya Lin. 2019/7/25 15:52:32 */

using NUnit.Framework;
using System;
using Thunder.Blazor.Components;
using Thunder.Blazor.Models;

namespace ThunderBlazorTest.Components
{
    class TComponentTest
    {
        TestContent tc = new TestContent { Test = "ok" };
        TestComponent tcc;

        [SetUp]
        public void Init()
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
            Assert.IsNotNull(tcc.View);
        }

        [Test]
        public void InitContent()
        {
            tcc.View = tc;
            Assert.IsTrue(tcc.DomId == tc.DomId);
            Assert.IsTrue(tcc.View.Caption == tc.Caption);
            tcc.Caption = "change 1";
            tcc.UpdateDataContext();
            Assert.IsTrue(tcc.Caption == tcc.View.Caption);
            tc.OnCommand.Invoke(this, ContextResult.Cancel());
            Assert.IsTrue(tcc.View.ObjectName == "test");
        }
    }

    class TestContent : TContainer
    {
        public string Test { get; set; }
    }

    class TestComponent : TComponentContainer<TestContent>
    {
        public override void Cancel()
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override void CloseItem(object item)
        {
            throw new NotImplementedException();
        }

        public override void Load(object item = null)
        {
            throw new NotImplementedException();
        }

        public override void LoadItem(object item)
        {
            throw new NotImplementedException();
        }

        public override void Show()
        {
            throw new NotImplementedException();
        }

        public override void ShowItem(object item)
        {
            throw new NotImplementedException();
        }
    }
}
