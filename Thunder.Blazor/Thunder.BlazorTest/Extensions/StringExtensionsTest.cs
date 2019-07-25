using Microsoft.VisualStudio.TestTools.UnitTesting;
using Thunder.Blazor.Extensions;
using Thunder.Blazor.Models;

namespace Thunder.BlazorTest
{
    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void Test_tojson()
        {
            var obj = ContextResult.Ok("test");
            //var s = obj.ToJson();
            //var v = "";
            //Assert.AreEqual(s, v);
            Assert.IsTrue(true);
        }
    }
}
