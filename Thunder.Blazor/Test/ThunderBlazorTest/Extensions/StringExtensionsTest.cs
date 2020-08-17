using NUnit.Framework;
using Thunder.Blazor.Extensions;
using Thunder.Blazor.Models;
using Thunder.Standard.Lib.Model;

namespace Thunder.BlazorTest
{
    public class StringExtensionsTest
    {
        string v = "{\"Text\":\"\\u9009\\u62E9\",\"Value\":\"OK\",\"Group\":null,\"Selected\":false,\"Object\":null}";
        SelectOption result = new SelectOption { Value = "OK", Text = "Ñ¡Ôñ" };

        [Test]
        public void Test_tojson()
        {
            var s = result.ToJson();
            Assert.AreEqual(s, v);
        }

        [Test]
        public void Test_fromjson()
        {
            var obj = v.FromJson<SelectOption>();

            Assert.IsTrue(obj.Value == result.Value);
            Assert.IsTrue(obj.Value != null);
            Assert.IsTrue(obj.Value == result.Value);
        }
    }
}
