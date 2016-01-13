using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using Microsoft.AspNet.Identity.Owin;
using ManahostManager.Domain.Tools;
using System.Threading;

namespace ManahostManager.Utils.Middlewares
{
    public class GlobalizationAuthenticateMiddleware : OwinMiddleware
    {
        public GlobalizationAuthenticateMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public async override Task Invoke(IOwinContext context)
        {
            if (context.Authentication.User != null && !String.IsNullOrWhiteSpace(context.Authentication.User.Identity.Name) && context.Authentication.User.Identity.IsAuthenticated)
            {
                IDependencyScope Scope = context.Get<IDependencyScope>();
                ClientUserManager UserManager = Scope.GetService(typeof(ClientUserManager)) as ClientUserManager;
                var userClient = await UserManager.FindByEmailAsync(context.Authentication.User.Identity.Name);
                if (userClient.Locale != null && !string.IsNullOrEmpty(userClient.Locale))
                {
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(userClient.Locale);
                }
            }
            await Next.Invoke(context);
        }
    }
}