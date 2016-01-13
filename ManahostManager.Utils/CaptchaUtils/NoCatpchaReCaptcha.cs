using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ManahostManager.Utils.CaptchaUtils
{
    public class NoCatpchaReCaptcha : ICaptcha
    {
        private const string CAPTCHA_URL = "https://www.google.com/recaptcha/api/siteverify";

        // in seconds
        private const double TIMEOUT = 10d;

        private const string SECRET_KEY = "6LeOYPoSAAAAANEtzeG9P9QykDsq8_zGBdsayk-j";

        public String Response { get; set; }

        public String Remoteip { get; set; }

        private static Dictionary<string, string> errorCodes = new Dictionary<string, string>()
        {
            {"missing-input-secret", GenericError.CAPTCHA_MISSING_SECRET},
            {"invalid-input-secret", GenericError.CAPTCHA_INVALID_SECRET},
            {"missing-input-response", GenericError.CAPTCHA_MISSING_RESPONSE},
            {"invalid-input-response", GenericError.CAPTCHA_INVALID_RESPONSE},
        };

        public void Validate()
        {
            String mess = null;
            WebResponse response = null;
            try
            {
                String finalUrl = CAPTCHA_URL + "?secret=" + SECRET_KEY + "&response=" + Response + "&remoteip=" + Remoteip;

                var request = WebRequest.Create(CAPTCHA_URL);
                request.Method = "GET";
                var dataStream = request.GetRequestStream();

                response = request.GetResponse();

                using (dataStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(dataStream))
                    {
                        mess = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception)
            {
                throw new ManahostException(GenericError.RESOURCE_UNREACHABLE);
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
            if (mess != null)
            {
                List<string> error_codes = new List<string>();
                JObject obj = JObject.Parse(mess);
                bool successed = (bool)obj["success"];
                JToken errors = obj["error-codes"];

                if (errors != null)
                {
                    foreach (JToken cur in errors)
                    {
                        String val;

                        if (errorCodes.TryGetValue((string)cur, out val))
                            error_codes.Add(val);
                        else
                            error_codes.Add(GenericError.CAPTCHA_UNKNOWN_ERROR);
                    }
                }
                if (successed == false)
                    throw new ManahostException(error_codes.ToArray());
            }
        }

        public void setParams(params string[] values)
        {
            Response = values[0];
            Remoteip = values[1];
        }
    }
}