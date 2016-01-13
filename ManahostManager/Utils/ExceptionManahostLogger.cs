using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace ManahostManager.Utils
{
    public class ExceptionManahostLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            if (LogTools.Log.ExceptionLogger.IsErrorEnabled)
            {
                LogTools.Log.ExceptionLogger.Error("Error Exception not handled in web Api", context.Exception);
            }
        }

        public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            Log(context);
            return Task.FromResult(0);
        }

        public override bool ShouldLog(ExceptionLoggerContext context)
        {
            return context.CatchBlock.IsTopLevel;
        }
    }
}