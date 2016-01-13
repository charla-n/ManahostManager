using ManahostManager.Provider;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManahostManager.App_Start
{
    public partial class WebApiApplication
    {
        private void SetupSignalR(IAppBuilder app)
        {
            OAuthBearerAuthenticationOptions Options = new OAuthBearerAuthenticationOptions()
            {
                Provider = new QueryStringBearerTokenProvider()
            };
            HubConfiguration config = new HubConfiguration()
            {
            };

            app.Map("/ws", (AppBuilderWebSockets) =>
            {
                AppBuilderWebSockets.UseCors(CorsOptions.AllowAll);
                AppBuilderWebSockets.UseOAuthBearerAuthentication(Options);
                AppBuilderWebSockets.RunSignalR(config);
            });
        }
    }
}