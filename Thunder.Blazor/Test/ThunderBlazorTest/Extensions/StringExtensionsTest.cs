using NUnit.Framework;
using Thunder.Blazor.Extensions;
using Thunder.Blazor.Models;

namespace Thunder.BlazorTest
{
    public class StringExtensionsTest
    {
        string v = "{\"Data\":\"test\",\"DataType\":\"System.String, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e\",\"Result\":1,\"Canceled\":false}";
        ContextResult result = ContextResult.Ok("test");

        [Test]
        public void Test_tojson()
        {
            var s = result.ToJson();
            Assert.AreEqual(s, v);
        }

        [Test]
        public void Test_fromjson()
        {
            var obj = v.FromJson<ContextResult>();
            Assert.IsTrue(obj.Result == result.Result);
            Assert.IsTrue(obj.Result != null);
            Assert.IsTrue((string)obj.Data == (string)result.Data);
        }
    }
}
