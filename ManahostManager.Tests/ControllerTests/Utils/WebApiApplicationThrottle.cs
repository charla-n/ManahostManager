using ManahostManager.App_Start;
using ManahostManager.Logger;
using ManahostManager.Utils;
using System.Collections.Generic;
using WebApiThrottle;

namespace ManahostManager.Tests.ControllerTests.Utils
{
    public class WebApiApplicationThrottle : WebApiApplication
    {
        protected override void SetupThrottling(Owin.IAppBuilder app)
        {
            MemoryCacheRepository cache = new MemoryCacheRepository();
            cache.Clear();
            app.Use(typeof(CustomThrottlingMiddleware), new ThrottlePolicy(perHour: 600)
            {
                IpThrottling = false,
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
            }, new PolicyMemoryCacheRepository(), cache, new ThrottlingLogger());
        }
    }
}