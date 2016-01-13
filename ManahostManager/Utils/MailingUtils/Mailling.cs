using ManahostManager.Domain.Entity;
using ManahostManager.LogTools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using System.Text;

namespace ManahostManager.Utils
{
    public class InfosMailling
    {
        public InfosMailling(string addrSmtp, int port, string email, string displayName, string password, bool requireCredential = true)
        {
            this.smtpClient = new SmtpClient(addrSmtp, port);
            if (requireCredential)
                smtpClient.Credentials = new System.Net.NetworkCredential(email, password);
            else
                smtpClient.UseDefaultCredentials = true;
            this.attachments = new List<Attachment>();
            this.toPeople = new List<String>();
            this.from = email;
            MailMessage = new MailMessage();
            MailMessage.From = new MailAddress(from, displayName, Encoding.UTF8);
        }

        public InfosMailling(string displayName)
            : this(
                ConfigurationManager.AppSettings["MAIL_SERVER"],
                int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]),
                ConfigurationManager.AppSettings["ADDR"],
                displayName + " via " + ConfigurationManager.AppSettings["NAME"],
                null,
                false)
        { }

        public InfosMailling()
            : this(
                ConfigurationManager.AppSettings["MAIL_SERVER"],
                int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]),
                ConfigurationManager.AppSettings["ADDR"],
                ConfigurationManager.AppSettings["NAME"],
                null,
                false)
        { }

        public void modifyTimeOut(int timeout)
        {
            this.smtpClient.Timeout = timeout;
        }

        public bool ssl { get; set; }

        public SmtpClient smtpClient { get; set; }

        public MailPriority prio { get; set; }

        public string from { get; set; }

        public string subject { get; set; }

        public string body { get; set; }

        public string displayName { get; set; }

        public List<String> toPeople { get; set; }

        public List<Attachment> attachments { get; set; }

        public MailMessage MailMessage { get; private set; }
    }

    public class Mailling
    {
        private static void Send(InfosMailling mail, MailLog log)
        {
            if (log != null)
            {
                log.DateSended = DateTime.UtcNow;
                log.Successful = true;
                log.To = String.Join(",", mail.toPeople.ToArray());
            }
            foreach (Attachment attach in mail.attachments)
                mail.MailMessage.Attachments.Add(attach);
            mail.MailMessage.Body = mail.body;
            mail.MailMessage.Subject = mail.subject;
            mail.MailMessage.IsBodyHtml = true;
            mail.MailMessage.Priority = mail.prio;
            mail.MailMessage.BodyEncoding = Encoding.UTF8;
            mail.MailMessage.SubjectEncoding = Encoding.UTF8;
            mail.smtpClient.EnableSsl = mail.ssl;
            try
            {
                mail.smtpClient.Send(mail.MailMessage);
                mail.MailMessage.Dispose();
            }
            catch (Exception e)
            {
                if (e is SmtpException || e is SmtpFailedRecipientsException)
                {
                    SmtpException ex = (SmtpException)e;

                    if (log == null)
                    {
                        if (Log.ExceptionLogger.IsErrorEnabled)
                            Log.ExceptionLogger.Error(e);
                        return;
                    }
                    log.Successful = false;
                    log.Reason = ex.Message;
                }
                else
                    throw;
            }
        }

        public static void SendMailBcc(InfosMailling mail, MailLog log)
        {
#if (PROD == true || DEV == true)

            foreach (String people in mail.toPeople)
                mail.MailMessage.Bcc.Add(new MailAddress(people));
            Send(mail, log);
#endif
        }

        public static void sendMail(InfosMailling mail, MailLog log)
        {
#if (PROD == true || DEV == true)
            foreach (String people in mail.toPeople)
                mail.MailMessage.To.Add(new MailAddress(people));
            Send(mail, log);
#endif
        }
    }
}