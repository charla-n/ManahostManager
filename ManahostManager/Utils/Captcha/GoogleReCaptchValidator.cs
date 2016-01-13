using ManahostManager.LogTools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ManahostManager.Utils.Captcha
{
    internal class ReCaptchaModelResponse
    {
        public bool success { get; set; }
    }

    public class GoogleReCaptchValidator : ICaptchaTools
    {
        private static readonly string URL_GOOGLE_RECAPTCHA_VERIFICATION = "https://www.google.com/recaptcha/api/siteverify";
        private string CaptchaSecret { get; set; }

        protected GoogleReCaptchValidator()
        {
            CaptchaSecret = ConfigurationManager.AppSettings[GenericNames.GOOGLE_RECAPTCHA_SECRET];
            if (CaptchaSecret == null)
            {
                throw new ArgumentNullException("GoogleReCaptchValidator : Secret not provided in web.config");
            }
        }

        public async Task<bool> VerifyCaptcha(string token, string ip)
        {
            using (var httpClient = new HttpClient())
            {
                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("secret", CaptchaSecret));
                postData.Add(new KeyValuePair<string, string>("response", token));
                HttpContent content = new FormUrlEncodedContent(postData);

                var response = await httpClient.PostAsync(URL_GOOGLE_RECAPTCHA_VERIFICATION, content);
                if (Log.InfoLogger.IsInfoEnabled)
                {
                    Log.InfoLogger.Info("Try Recaptcha Validation with Token : " + token);
                }
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseBody = await response.Content.ReadAsAsync<ReCaptchaModelResponse>();
                    if (Log.InfoLogger.IsInfoEnabled)
                    {
                        Log.InfoLogger.Info("Tried Recaptcha Validation with response : " + await response.Content.ReadAsStringAsync());
                    }
                    return responseBody.success;
                }
                return false;
            }
        }

        public static ICaptchaTools Create()
        {
            return new GoogleReCaptchValidator();
        }
    }
}