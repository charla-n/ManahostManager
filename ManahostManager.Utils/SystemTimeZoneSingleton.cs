using System;
using System.Collections.Generic;

namespace ManahostManager.Utils
{
    public sealed class SystemTimeZoneSingleton
    {
        private static readonly SystemTimeZoneSingleton INSTANCE = new SystemTimeZoneSingleton();

        public IReadOnlyCollection<TimeZoneInfo> TimeZones { get; set; }

        static SystemTimeZoneSingleton()
        {
        }

        private SystemTimeZoneSingleton()
        {
            TimeZones = TimeZoneInfo.GetSystemTimeZones();
        }

        public static SystemTimeZoneSingleton Instance
        {
            get
            {
                return INSTANCE;
            }
        }
    }
}