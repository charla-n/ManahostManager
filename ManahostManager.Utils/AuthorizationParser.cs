using System;
using System.Text;

namespace ManahostManager.Utils
{
    public static class AuthorizationParser
    {
        public static string[] Parse(String cur)
        {
            string parameter = null;
            try
            {
                parameter = Encoding.UTF8.GetString(Convert.FromBase64String(cur));
            }
            catch (Exception e)
            {
                if (e is ArgumentNullException || e is FormatException)
                    return null;
                throw e;
            }
            return parameter.Split(':');
        }
    }
}