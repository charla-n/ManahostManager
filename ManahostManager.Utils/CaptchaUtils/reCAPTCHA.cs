using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ManahostManager.Utils.CaptchaUtils
{
    public class reCAPTCHA : ICaptcha
    {
        private const string PRIV_KEY_RECAPTCHA = "6LeOYPoSAAAAANEtzeG9P9QykDsq8_zGBdsayk-j";
        private const string CAPTCHA_URL = "http://www.google.com/recaptcha/api/verify";

        // in seconds
        private const double TIMEOUT = 10d;

        public String Remoteip { get; set; }

        public String Response { get; set; }

        public String Challenge { get; set; }

        private static Dictionary<string, string> errorCodes = new Dictionary<string, string>()
        {
            {"invalid-site-private-key", GenericError.CAPTCHA_INVALID_PRIV_KEY},
            {"invalid-request-cookie", GenericError.CAPTCHA_INVALID_CHALLENGE},
            {"incorrect-captcha-sol", GenericError.CAPTCHA_INVALID_SOLUTION},
            {"captcha-timeout", GenericError.CAPTCHA_TIMEOUT},
            {"recaptcha-not-reachable", GenericError.CAPTCHA_NOT_REACHABLE}
        };

        public void Validate()
        {
            String[] mess = null;
            try
            {
                String parameters = "&privatekey=" + PRIV_KEY_RECAPTCHA + "&remoteip=" + Remoteip + "&challenge=" + Challenge + "&response=" + Response;

                byte[] encodingPost = Encoding.UTF8.GetBytes(parameters);

                var request = WebRequest.Create(CAPTCHA_URL);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = encodingPost.Length;
                var dataStream = request.GetRequestStream();
                dataStream.Write(encodingPost, 0, encodingPost.Length);
                dataStream.Close();

                var response = request.GetResponse();

                using (dataStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(dataStream))
                    {
                        mess = reader.ReadToEnd().Split('\n');
                    }
                }
            }
            catch (Exception)
            {
                throw new ManahostException(GenericError.RESOURCE_UNREACHABLE);
            }
            if (mess == null || mess[0] == "false")
            {
                String err = null;
                errorCodes.TryGetValue(mess[1], out err);

                throw new ManahostException(err);
            }
        }

        public void setParams(params string[] values)
        {
            Remoteip = values[0];
            Response = values[1];
            Challenge = values[2];
        }
    }
}