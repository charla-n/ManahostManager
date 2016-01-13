using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace ManahostManager.Utils.HttpActionResult
{
    public static class ExtensionsHttpActionResult
    {
        public static OkNegotiatedContentResult<T> OkWithHeader<T>(this ApiController baseClass, T Item, IDictionary<string, string> headers) where T : class
        {
            var result = new OkNegotiatedContentResult<T>(Item, baseClass);
            foreach (var item in headers)
            {
                result.Request.Headers.Add(item.Key, item.Value);
            }
            return result;
        }
    }
}