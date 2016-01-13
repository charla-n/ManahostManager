using ManahostManager.Logger;
using ManahostManager.Utils;
using Owin;
using System.Collections.Generic;
using WebApiThrottle;

namespace ManahostManager.App_Start
{
    public partial class WebApiApplication
    {
        protected virtual void SetupThrottling(IAppBuilder app)
        {
            app.Use(typeof(CustomThrottlingMiddleware), new ThrottlePolicy(perHour: 600)
            {
                IpThrottling = true,
                ClientThrottling = true,
                ClientRules = new Dictionary<string, RateLimits>
                {
                    { CustomThrottlingMiddleware.IS_AUTHENTICATE, new RateLimits {PerHour = 4000}}
                },
                EndpointThrottling = true,
                EndpointRules = new Dictionary<string, RateLimits>()
                {
                    { "api/ip", new RateLimits { PerMinute = 2 } }
                }
            }, new PolicyMemoryCacheRepository(), new MemoryCacheRepository(), new ThrottlingLogger());
        }
    }
}