using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Tools;
using ManahostManager.Provider;
using ManahostManager.Unity;
using ManahostManager.Utils;
using ManahostManager.Utils.ExternalLoginTools;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace ManahostManager.App_Start
{
    public class UnityRegisterScope : IDisposable
    {
        public IDependencyScope Scope { get; set; }

        public void Dispose()
        {
        }
    }
    internal class ObjectPerRequest : OwinMiddleware
    {
        private IDependencyResolver Resolver { get; set; }

        public ObjectPerRequest(OwinMiddleware next, IDependencyResolver resolver) : base(next)
        {
            Resolver = resolver;
        }

        public async override Task Invoke(IOwinContext context)
        {
            using (IDependencyScope scope = Resolver.BeginScope())
            {
                context.Set<IDependencyScope>(scope);
                context.Set<ManahostManagerDAL>((ManahostManagerDAL)scope.GetService(typeof(ManahostManagerDAL)));
                var scopeUnity = scope as UnityResolver;
                scopeUnity.RegisterInstance(new UnityRegisterScope() { Scope = scope });
                await Next.Invoke(context);
            }
        }
    }

    public partial class WebApiApplication
    {
        public static Dictionary<string, Func<ITokenInfo>> TokenInfoList = null;

        private void SetupIdentity(IAppBuilder app)
        {
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            var GOOGLE_ID = ConfigurationManager.AppSettings[GenericNames.GOOGLE_CLIENT_ID];
            var GOOGLE_SECRET = ConfigurationManager.AppSettings[GenericNames.GOOGLE_CLIENT_SECRET];
            if (GOOGLE_ID != null && GOOGLE_SECRET != null)
            {
                GoogleOAuth2AuthenticationOptions googleAuthOptions = new GoogleOAuth2AuthenticationOptions()
                {
                    ClientId = ConfigurationManager.AppSettings[GenericNames.GOOGLE_CLIENT_ID],
                    ClientSecret = ConfigurationManager.AppSettings[GenericNames.GOOGLE_CLIENT_SECRET],
                    Provider = new GoogleCustomAuthenticationProvider()
                };
                app.UseGoogleAuthentication(googleAuthOptions);
            }
            else
            {
                TokenInfoList.Remove(GoogleTokenInfo.NAME_PROVIDER);
            }
            // Token Generation
            app.UseOAuthAuthorizationServer(OptionsServerOAuth);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}