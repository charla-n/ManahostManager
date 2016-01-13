using ManahostManager.Domain.DAL;
using ManahostManager.Logger;
using ManahostManager.LogTools;
using ManahostManager.Provider;
using ManahostManager.Unity;
using ManahostManager.Utils;
using ManahostManager.Utils.ExternalLoginTools;
using ManahostManager.Utils.Middlewares;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using MVCControlsToolkit.Owin.Globalization;
using Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.Tracing;

[assembly: OwinStartup(typeof(ManahostManager.App_Start.WebApiApplication))]

namespace ManahostManager.App_Start
{
    public partial class WebApiApplication
    {
#if DEV || DEBUG
        public static readonly String UPLOAD_FOLDER_ROOT = "C:\\inetpub\\UploadDev\\";
        public static readonly string MANAGER_URL = "http://homedev.manahost.fr/#/";
#elif PROD
        public static readonly String UPLOAD_FOLDER_ROOT = "C:\\inetpub\\Upload\\";
        public static readonly string MANAGER_URL = "https://manager.manahost.fr/#/";
#endif

        public static IUnityContainer container;
        public static OAuthAuthorizationServerOptions OptionsServerOAuth { get; set; }

        static WebApiApplication()
        {
            if (TokenInfoList == null)
            {
                TokenInfoList = new Dictionary<string, Func<ITokenInfo>>();
            }
            TokenInfoList.Clear();
            TokenInfoList.Add(GoogleTokenInfo.NAME_PROVIDER, GoogleTokenInfo.Create);

#if (PROD == false)
            Database.SetInitializer(new ManahostManagerInitializer());
#endif

            #region Unity SignalR

            GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => new UnityHubActivator(container));
            GlobalHost.HubPipeline.AddModule(new ManahostValidationExceptionHubPipeline());
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new Log4NetTraceWriter());

            #endregion Unity SignalR

            OptionsServerOAuth = new OAuthAuthorizationServerOptions()
            {
#if !PROD
                AllowInsecureHttp = true,
#endif
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new BearerAuthorizationServerProvider(),
                RefreshTokenProvider = new BearerRefreshTokenProvider()
            };
        }

        public void Configuration(IAppBuilder app)
        {
            container = SetupUnity();

            //app.Use(typeof(GZipMiddleware));
            app.UseCors(CorsOptions.AllowAll);
            app.Use(typeof(ObjectPerRequest), new UnityResolver(container));
            app.UseGlobalization(new OwinGlobalizationOptions("en-US", true).Add("fr-FR", true).DisableCultureInPath().DisableCookie());
            SetupIdentity(app);
            SetupThrottling(app);
            app.Use(typeof(GlobalizationAuthenticateMiddleware));
            SetupWebApi(app);
            SetupAutoMapper();
            SetupSignalR(app);
            if (Log.InfoLogger.IsInfoEnabled)
                Log.InfoLogger.Info("Application Started");
        }
    }
}