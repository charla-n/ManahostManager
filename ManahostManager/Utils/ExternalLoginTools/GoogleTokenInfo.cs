using ManahostManager.Model.DTO.Account;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace ManahostManager.Utils.ExternalLoginTools
{
    public class GoogleTokenInfo : ITokenInfo
    {
        public static readonly string URL_TOKEN_INFO = "https://www.googleapis.com/oauth2/v1/tokeninfo?access_token={0}";
        public static readonly string URL_USER_INFO = "https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token={0}";
        public static readonly string NAME_PROVIDER = "Google";

        public async Task<ParsedExternalAccessToken> Get(string accessToken)
        {
            var urlTokenEndPoint = string.Format(URL_TOKEN_INFO, accessToken);

            var client = new HttpClient();

            var response = await client.GetAsync(new Uri(urlTokenEndPoint));

            var ExternalToken = await response.Content.ReadAsAsync<ParsedExternalAccessToken>();

            if (!string.Equals(ExternalToken.app_id, ConfigurationManager.AppSettings[GenericNames.GOOGLE_CLIENT_ID], StringComparison.OrdinalIgnoreCase))
                return null;
            return ExternalToken;
        }

        public async Task<ParsedExternalAccessTokenUserInfo> GetUserInfo(string accessToken)
        {
            var urlTokenEndPoint = string.Format(URL_USER_INFO, accessToken);

            var client = new HttpClient();

            var response = await client.GetAsync(new Uri(urlTokenEndPoint));

            var ExternalToken = await response.Content.ReadAsAsync<ParsedExternalAccessTokenUserInfo>();

            return ExternalToken;
        }

        public static ITokenInfo Create()
        {
            return new GoogleTokenInfo();
        }
    }
}