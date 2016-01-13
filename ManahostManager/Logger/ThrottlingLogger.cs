using System.Web.Http;
using System.Web.Http.Tracing;
using WebApiThrottle;

namespace ManahostManager.Logger
{
    public class ThrottlingLogger : IThrottleLogger
    {
        private static readonly ITraceWriter log = GlobalConfiguration.Configuration.Services.GetTraceWriter();

        public void Log(ThrottleLogEntry entry)
        {
            if (entry.Request != null)
            {
                log.Warn(entry.Request, "CustomThrottlingHandler",
                    "{0} Request {1} from {2} has been throttled (blocked), quota {3}/{4} exceeded by {5}",
                    entry.LogDate, entry.RequestId, entry.ClientIp, entry.RateLimit, entry.RateLimitPeriod, entry.TotalRequests);
            }
        }
    }
}