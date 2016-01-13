using ManahostManager.Utils;
using Microsoft.Owin.Security.Google;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ManahostManager.Provider
{
    public class GoogleCustomAuthenticationProvider : IGoogleOAuth2AuthenticationProvider
    {
        public object GenericName { get; private set; }

        public void ApplyRedirect(GoogleOAuth2ApplyRedirectContext context)
        {
            context.Response.Redirect(context.RedirectUri);
        }

        public Task Authenticated(GoogleOAuth2AuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim(GenericNames.AUTHENTICATION_EXTERNAL_LOGIN, context.AccessToken));
            return Task.FromResult<object>(null);
        }

        public Task ReturnEndpoint(GoogleOAuth2ReturnEndpointContext context)
        {
            return Task.FromResult<object>(null);
        }
    }
}