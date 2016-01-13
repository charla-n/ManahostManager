using ManahostManager.Domain.Entity;
using ManahostManager.Mail;
using ManahostManager.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using netDumbster.smtp;
using System.Net.Mail;

namespace ManahostManager.Tests
{
    [TestClass]
    public class MaillingTest
    {
        static private SimpleSmtpServer _Server = null;

        [ClassInitialize]
        public static void InitClass(TestContext testContext)
        {
            _Server = SimpleSmtpServer.Start(10025);
        }

        [ClassCleanup]
        public static void CleanupClass()
        {
            _Server.Stop();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _Server.ClearReceivedEmail();
        }

        [TestMethod]
        public void SendOneMailServerWithoutPasswordShouldWork()
        {
            InfosMailling info = new InfosMailling();
            info.toPeople.Add("quentin.balland@epitech.eu");
            info.subject = "subject";
            info.prio = MailPriority.High;
            info.ssl = false;
            info.modifyTimeOut(200000);
            MailLog log = new MailLog();

            Mailling.sendMail(info, log);
            Assert.AreEqual(1, _Server.ReceivedEmailCount);
        }

        [TestMethod]
        public void SendMailWithMailTemplate()
        {
            MailTemplate.sendTemplateMailCreationAccount("fr-FR", "test", "lolilol", "Monseigneur", "contact@manahost.fr", "token");
            MailTemplate.sendTemplateMailWarningConnection("en-US", "Monsieur", "Quentin", "Balland", "contact@manahost.fr");
            MailTemplate.sendTemplateMailWarningConnection("fr-FR", "Monsieur", "Quentin", "Balland", "contact@manahost.fr");
            Assert.AreEqual(3, _Server.ReceivedEmailCount);
        }
    }
}