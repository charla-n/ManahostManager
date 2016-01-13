using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace ManahostManager.Provider
{
    public class QueryStringBearerTokenProvider : OAuthBearerAuthenticationProvider
    {
        public override Task RequestToken(OAuthRequestTokenContext context)
        {
            var value = context.Request.Query.Get("access_token");

            if (!string.IsNullOrEmpty(value))
            {
                context.Token = value;
            }
            return Task.FromResult<object>(null);
        }
    }
}