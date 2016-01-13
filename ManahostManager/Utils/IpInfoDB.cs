using Jil;
using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ManahostManager.Utils
{
    public class IpInfoDBModel
    {
        public string timeZone { get; set; }
    }

    public class IpInfoDB
    {
        private static readonly string IP_INFO_DB_URL = "http://api.ipinfodb.com";
        private static readonly string IP_INFO_DB_ROUTE = "/v3/ip-city/";
        private static readonly string IP_INFO_DB_MODEL_ROUTE = "{0}?key={1}&format=json&ip={2}";

        public static async Task<IpInfoDBModel> GetInfoIP(string ip)
        {
            string token = ConfigurationManager.AppSettings["IPINFODBKEY"];

            string requestURI = string.Format(IP_INFO_DB_MODEL_ROUTE, IP_INFO_DB_ROUTE, token, ip);
            using (var httpClient = new HttpClient())
            {
                try
                {
                    httpClient.BaseAddress = new Uri(IP_INFO_DB_URL);
                    httpClient.Timeout = new TimeSpan(0, 0, 2); // 2Sec TimeOut;
                }
                catch (UriFormatException)
                {
                    return null;
                }
                using (var stream = await httpClient.GetStreamAsync(requestURI))
                {
                    using (TextReader tr = new StreamReader(stream))
                    {
                        var result = JSON.Deserialize<IpInfoDBModel>(tr);
                        return result;
                    }
                }
            }
        }
    }
}