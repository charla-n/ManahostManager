using ManahostManager.LogTools;
using Microsoft.Owin;
using System;
using WebApiThrottle;

namespace ManahostManager.Utils
{
    public class CustomThrottlingMiddleware : ThrottlingMiddleware
    {
        public const string THROTTLE_ENV = "ManahostThrottle";
        public const string IS_AUTHENTICATE = "AUTH";

        public CustomThrottlingMiddleware(OwinMiddleware next)
            : base(next)
        { }

        public CustomThrottlingMiddleware(OwinMiddleware next, ThrottlePolicy policy, IPolicyRepository policyRepository, IThrottleRepository repository, IThrottleLogger logger) :
            base(next, policy, policyRepository, repository, logger)
        { }

        protected override RequestIdentity SetIndentity(IOwinRequest request)
        {
            string clientKey = "NOT_" + IS_AUTHENTICATE;

            if (request.Context.Authentication.User != null && !String.IsNullOrWhiteSpace(request.Context.Authentication.User.Identity.Name) && request.Context.Authentication.User.Identity.IsAuthenticated)
                clientKey = IS_AUTHENTICATE;

#if DEBUG || DEV
            Log.InfoLogger.Info(String.Format("Throttling --- Client IP is {0}", request.RemoteIpAddress));
#endif
            return new RequestIdentity()
            {
                ClientKey = clientKey,
                ClientIp = request.RemoteIpAddress,
                Endpoint = request.Uri.AbsolutePath.ToLowerInvariant()
            };
        }
    }
}