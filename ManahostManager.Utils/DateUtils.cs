using System;

namespace ManahostManager.Utils
{
    public class DateUtils
    {
        public static double GetElapsedDaysFromDateTimes(DateTime from, DateTime to)
        {
            return (to - from).TotalHours / 24;
        }
    }
}