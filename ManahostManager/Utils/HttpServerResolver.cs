using Microsoft.AspNet.Identity.Owin;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using System.Web.Http.Hosting;

namespace ManahostManager.Utils
{
    public class HttpServerResolver : HttpServer
    {
        public HttpServerResolver(HttpConfiguration configuration)
            : base(configuration)
        {
        }

        public HttpServerResolver(HttpConfiguration configuration, HttpMessageHandler dispatcher)
            : base(configuration, dispatcher)
        {
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var DependencyScope = request.GetOwinContext().Get<IDependencyScope>();
            if (DependencyScope != null)
            {
                request.Properties[HttpPropertyKeys.DependencyScope] = DependencyScope;
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}