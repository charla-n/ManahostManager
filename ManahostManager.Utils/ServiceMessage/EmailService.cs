using ManahostManager.LogTools;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ManahostManager.Utils.ServiceMessage
{
    /// <summary>
    /// base class for send email for Manahost
    /// </summary>
    public class EmailService : IIdentityMessageService
    {
        private readonly NameValueCollection _EmailServiceConfig = null;

        private string SMTP_IP
        {
            get
            {
                if (_EmailServiceConfig == null)
                    return null;
                return _EmailServiceConfig["MAIL_SERVER"];
            }
        }

        private string SMTP_EMAIL
        {
            get
            {
                if (_EmailServiceConfig == null)
                    return null;
                return _EmailServiceConfig["ADDR"];
            }
        }

        private int SMTP_PORT
        {
            get
            {
                if (_EmailServiceConfig == null)
                    return -1;
                return int.Parse(_EmailServiceConfig["SMTP_PORT"]);
            }
        }

        private bool SMTP_AUTH
        {
            get
            {
                if (_EmailServiceConfig == null)
                    return false;
                return bool.Parse(_EmailServiceConfig["HAS_AUTH"]);
            }
        }

        private string SMTP_AUTH_USERNAME
        {
            get
            {
                if (_EmailServiceConfig == null)
                    return null;
                return _EmailServiceConfig["USERNAME"];
            }
        }

        private string SMTP_AUTH_PASSWD
        {
            get
            {
                if (_EmailServiceConfig == null)
                    return null;
                return _EmailServiceConfig["PASSWORD"];
            }
        }

        public EmailService()
        {
            _EmailServiceConfig = ConfigurationManager.GetSection("MailService") as NameValueCollection;
            if (Log.InfoLogger.IsInfoEnabled)
            {
                Log.InfoLogger.Info(String.Format("EmailService is configuring with {0}:{1}", SMTP_IP, SMTP_PORT));
            }
        }

        public async Task SendAsync(IdentityMessage message)
        {
            if (Log.InfoLogger.IsInfoEnabled)
            {
                Log.InfoLogger.Info(String.Format("Try to send a mail to {0}", message.Destination));
            }

            var mailMessage = new MailMessage(SMTP_EMAIL, message.Destination);
            mailMessage.Subject = message.Subject;
            mailMessage.Body = message.Body;
            mailMessage.IsBodyHtml = true;

            using (SmtpClient clientSmtp = new SmtpClient(SMTP_IP, SMTP_PORT))
            {
                clientSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                clientSmtp.EnableSsl = false;

                if (SMTP_AUTH)
                {
                    clientSmtp.Credentials = new NetworkCredential(SMTP_AUTH_USERNAME, SMTP_AUTH_PASSWD);
                }
                try
                {
                    await clientSmtp.SendMailAsync(mailMessage);
                }
                catch (SmtpException e)
                {
                    if (Log.ExceptionLogger.IsErrorEnabled)
                    {
                        Log.ExceptionLogger.Error(String.Format("SmtpException on EmailService - {0}", message.Destination), e);
                    }
                }
            }
        }
    }
}