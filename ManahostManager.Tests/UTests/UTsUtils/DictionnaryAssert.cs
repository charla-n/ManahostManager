using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Tests.UTests.UTsUtils
{
    public static class DictionnaryAssert
    {
        public static void DictionnaryHasValueAndError(ModelStateDictionary dict, string value, string error)
        {
            Assert.AreEqual(1, dict.Count);
            var errors = dict.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
            var values = dict.Select(x => x.Key).ToList();
            foreach (var cur in values)
                Assert.AreEqual(value, cur);
            foreach (var cur in errors)
                foreach (var cur2 in cur)
                    Assert.AreEqual(error, cur2.ErrorMessage);
        }
    }
}