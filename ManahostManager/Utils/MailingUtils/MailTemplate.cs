using ManahostManager.Utils;
using System;
using System.Globalization;
using System.Net.Mail;
using System.Threading;

namespace ManahostManager.Mail
{
    public class MailTemplate
    {
        private const string MANAHOST_EMAIL = "contact@manahost.fr";
        private const string BASE_WEB_CLIENT_URL = "https://manager.manahost.fr";
        private const string VALIDATE_ACCOUNT = "/ValidateAccount";
        private const string FORGOT_PASSWORD = "/ForgotPassword";
        private const string ACTIVATION_LINK_FORMAT = BASE_WEB_CLIENT_URL + VALIDATE_ACCOUNT + "/{0}/{1}";
        private const string FORGOT_LINK_FORMAT = BASE_WEB_CLIENT_URL + FORGOT_PASSWORD + "/{0}";

        static public void sendTemplateMailCreationAccount(String locale, String firstname, String lastName, String civility, String email, String tokenActivation)
        {
            InfosMailling mail = new InfosMailling();
            String activationLink = String.Format(ACTIVATION_LINK_FORMAT, email, tokenActivation);
            String forgotPasswordLink = String.Format(FORGOT_LINK_FORMAT, email);

            mail.toPeople.Add(email);
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(locale);
            ManahostManager.Utils.MailingUtils.Resources.MailTemplate.Culture = Thread.CurrentThread.CurrentCulture;
            mail.subject = ManahostManager.Utils.MailingUtils.Resources.MailTemplate.SubjectAccountCreation;
            mail.body = String.Format(ManahostManager.Utils.MailingUtils.Resources.MailTemplate.BodyAccountCreation, civility, firstname, lastName, activationLink, email, forgotPasswordLink);
            mail.body += ManahostManager.Utils.MailingUtils.Resources.MailTemplate.Footer;
            mail.prio = MailPriority.Normal;
            Mailling.sendMail(mail, null);
        }

        static public void sendTemplateMailResetPassword(String locale, String civility, String firstname, String lastName, String email, String urlRestaure)
        {
            InfosMailling mail = new InfosMailling();
            String forgotPasswordLink = String.Format(FORGOT_LINK_FORMAT, email);

            mail.toPeople.Add(email);
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo(locale);
            ManahostManager.Utils.MailingUtils.Resources.MailTemplate.Culture = Thread.CurrentThread.CurrentCulture;
            mail.subject = ManahostManager.Utils.MailingUtils.Resources.MailTemplate.SubjectResetPassword;
            mail.body = String.Format(ManahostManager.Utils.MailingUtils.Resources.MailTemplate.BodyResetPassword, civility, firstname, lastName, forgotPasswordLink);
            mail.body += ManahostManager.Utils.MailingUtils.Resources.MailTemplate.Footer;
            mail.prio = MailPriority.Normal;
            Mailling.sendMail(mail, null);
        }

        static public void sendTemplateMailWarningConnection(String locale, String civility, String firstname, String lastname, String email)
        {
            InfosMailling mail = new InfosMailling();
            String forgotPasswordLink = String.Format(FORGOT_LINK_FORMAT, email);
            ManahostManager.Utils.MailingUtils.Resources.MailTemplate.Culture = Thread.CurrentThread.CurrentCulture;

            mail.toPeople.Add(email);
            mail.subject = ManahostManager.Utils.MailingUtils.Resources.MailTemplate.SubjectConnectionWarning;
            mail.body = String.Format(ManahostManager.Utils.MailingUtils.Resources.MailTemplate.BodyConnectionWarning, civility, firstname, lastname, forgotPasswordLink);
            mail.body += ManahostManager.Utils.MailingUtils.Resources.MailTemplate.Footer;
            mail.prio = MailPriority.Normal;
            Mailling.sendMail(mail, null);
        }

        static public void sendTemplateMailLogError(String stacktrace, String message)
        {
            InfosMailling mail = new InfosMailling();

            mail.toPeople.Add(MANAHOST_EMAIL);
            mail.subject = "Error Manahost";
            mail.body = "An error occured : </br></br>message error : " + message + "</br></br>stacktrace error : " + stacktrace;
            mail.prio = MailPriority.High;
            Mailling.sendMail(mail, null);
        }
    }
}