using log4net;

namespace ManahostManager.LogTools
{
    public static class Log
    {
        public static ILog InfoLogger
        {
            get { return LogManager.GetLogger("InfoLogger"); }
        }

        public static ILog ExceptionLogger
        {
            get { return LogManager.GetLogger("ExceptionLogger"); }
        }

        public static ILog WarnLogger
        {
            get { return LogManager.GetLogger("WarnLogger"); }
        }
    }
}