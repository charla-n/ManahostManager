using ManahostManager.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManahostManager.Utils
{
    public class PeriodUtils
    {
        private static readonly int[] days = { 64, 1, 2, 4, 8, 16, 32 };

        public static bool AllCovered(List<Period> periodList, DateTime begin, DateTime end)
        {
            int diff = (int)(end.Date - begin.Date).TotalDays;

            if (diff <= 0)
                return true;
            bool[] covered = new bool[diff];

            for (int i = 0; i < diff; i++)
            {
                // Get the list of periods for that day and only that day
                // It's a list because multiple periods can be at the same begin-end datetimes but with different days
                IEnumerable<Period> periods = periodList.Where(p => begin >= p.Begin && begin <= p.End && p.IsClosed == false);
                foreach (Period cur in periods)
                {
                    // Check if that day is covered
                    if (((int)cur.Days & days[(int)(begin.DayOfWeek)]) == days[(int)(begin.DayOfWeek)])
                        covered[i] = true;
                }
                begin = begin.AddDays(1);
            }
            return !covered.Any(p => p == false);
        }

        public static bool IsDaysCross(List<Period> periodList, Period toCheck)
        {
            int diff = (int)(((DateTime)toCheck.End).Date - ((DateTime)toCheck.Begin).Date).TotalDays;
            DateTime currentDayCheck = (DateTime)toCheck.Begin;

            for (int i = 0; i < diff; i++)
            {
                IEnumerable<Period> periods = periodList.Where(p => currentDayCheck >= p.Begin && currentDayCheck <= p.End && p.IsClosed == false);
                foreach (Period cur in periods)
                {
                    if (((int)cur.Days & days[(int)(currentDayCheck.DayOfWeek)]) != 0 &&
                        (((int)cur.Days & days[(int)(currentDayCheck.DayOfWeek)]) ==
                         ((int)toCheck.Days & days[(int)(currentDayCheck.DayOfWeek)])))
                        return true;
                }
                currentDayCheck = currentDayCheck.AddDays(1);
            }
            return false;
        }

        public static Period GetCorrecPeriod(List<Period> periods, DateTime d)
        {
            return periods.FirstOrDefault(p => ((int)p.Days & days[(int)(d.DayOfWeek)]) == days[(int)(d.DayOfWeek)]);
        }
    }
}