using System.Globalization;

namespace ManahostManager.Utils
{
    public sealed class SystemCultureSingleton
    {
        private static readonly SystemCultureSingleton INSTANCE = new SystemCultureSingleton();

        public CultureInfo[] Cultures { get; set; }

        static SystemCultureSingleton()
        {
        }

        private SystemCultureSingleton()
        {
            Cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
        }

        public static SystemCultureSingleton Instance
        {
            get
            {
                return INSTANCE;
            }
        }
    }
}