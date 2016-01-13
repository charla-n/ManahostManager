//using ManahostManager.Controllers;
//using ManahostManager.Model;
//using ManahostManager.Services;
//using ManahostManager.Tests.Repository;
//using ManahostManager.Utils;
//using ManahostManager.Validation;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Text;
//using System.Web.Http.ModelBinding;

//namespace ManahostManager.Tests
//{
//    [TestClass]
//    public class MailTest
//    {
//        private MailModel model;

//        [TestInitialize]
//        public void Init()
//        {
//            model = new MailModel()
//            {
//                Body = "MailTest",
//                MailConfigId = 10,
//                Password = Convert.ToBase64String(Encoding.UTF8.GetBytes("yz262bCB")),
//                Subject = "MailTest",
//                To = new System.Collections.Generic.List<int>() { 10, 10 }
//            };
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void ShouldntPostMailBecauseOfNull()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            MailService s = new MailService(dict, repo, new MailValidation());

//            s.SendMail(new ClientRepositoryTest().FindUserByMail("testnodefaulthomeid@test.com"), null, null);
//        }

//        [TestMethod]
//        public void PostMailShouldPass()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            MailService s = new MailService(dict, repo, new MailValidation());

//            MailController.AdditionalRepositories addRepo = new MailController.AdditionalRepositories(null)
//            {
//                HomeRepo = new HomeRepositoryTest(),
//                PeopleRepo = new PeopleRepositoryTest(),
//                DocumentRepo = new DocumentRepositoryTest()
//            };

//            s.SendMail(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<MailModel>(model), addRepo);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void PostMailShouldnPassBecauseOfNullBody()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            MailService s = new MailService(dict, repo, new MailValidation());

//            model.Body = null;

//            MailController.AdditionalRepositories addRepo = new MailController.AdditionalRepositories(null)
//            {
//                HomeRepo = new HomeRepositoryTest(),
//                PeopleRepo = new PeopleRepositoryTest(),
//                DocumentRepo = new DocumentRepositoryTest()
//            };

//            s.SendMail(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<MailModel>(model), addRepo);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void PostMailShouldnPassBecauseOfEmptyBody()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            MailService s = new MailService(dict, repo, new MailValidation());

//            model.Body = "";

//            MailController.AdditionalRepositories addRepo = new MailController.AdditionalRepositories(null)
//            {
//                HomeRepo = new HomeRepositoryTest(),
//                PeopleRepo = new PeopleRepositoryTest(),
//                DocumentRepo = new DocumentRepositoryTest()
//            };

//            s.SendMail(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<MailModel>(model), addRepo);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void PostMailShouldnPassBecauseOfNullSubject()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            MailService s = new MailService(dict, repo, new MailValidation());

//            model.Subject = null;

//            MailController.AdditionalRepositories addRepo = new MailController.AdditionalRepositories(null)
//            {
//                HomeRepo = new HomeRepositoryTest(),
//                PeopleRepo = new PeopleRepositoryTest(),
//                DocumentRepo = new DocumentRepositoryTest()
//            };

//            s.SendMail(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<MailModel>(model), addRepo);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void PostMailShouldnPassBecauseOfNullPassword()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            MailService s = new MailService(dict, repo, new MailValidation());

//            model.Password = null;

//            MailController.AdditionalRepositories addRepo = new MailController.AdditionalRepositories(null)
//            {
//                HomeRepo = new HomeRepositoryTest(),
//                PeopleRepo = new PeopleRepositoryTest(),
//                DocumentRepo = new DocumentRepositoryTest()
//            };

//            s.SendMail(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<MailModel>(model), addRepo);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void PostMailShouldnPassBecauseOfNonExistentResource()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            MailService s = new MailService(dict, repo, new MailValidation());

//            model.MailConfigId = -1;

//            MailController.AdditionalRepositories addRepo = new MailController.AdditionalRepositories(null)
//            {
//                HomeRepo = new HomeRepositoryTest(),
//                PeopleRepo = new PeopleRepositoryTest(),
//                DocumentRepo = new DocumentRepositoryTest()
//            };

//            s.SendMail(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<MailModel>(model), addRepo);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void PostMailShouldnPassBecauseOfEmptyRecipients()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            MailService s = new MailService(dict, repo, new MailValidation());

//            model.To = new System.Collections.Generic.List<int>() { };

//            MailController.AdditionalRepositories addRepo = new MailController.AdditionalRepositories(null)
//            {
//                HomeRepo = new HomeRepositoryTest(),
//                PeopleRepo = new PeopleRepositoryTest(),
//                DocumentRepo = new DocumentRepositoryTest()
//            };

//            s.SendMail(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<MailModel>(model), addRepo);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void PostMailShouldnPassBecauseOfInvalidBase64()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            MailService s = new MailService(dict, repo, new MailValidation());

//            model.Password = "notabase64";

//            MailController.AdditionalRepositories addRepo = new MailController.AdditionalRepositories(null)
//            {
//                HomeRepo = new HomeRepositoryTest(),
//                PeopleRepo = new PeopleRepositoryTest(),
//                DocumentRepo = new DocumentRepositoryTest()
//            };

//            s.SendMail(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<MailModel>(model), addRepo);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void PostMailShouldnPassBecauseOfInvalidRecipient()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            MailService s = new MailService(dict, repo, new MailValidation());

//            model.To = new System.Collections.Generic.List<int>() { -1 };

//            MailController.AdditionalRepositories addRepo = new MailController.AdditionalRepositories(null)
//            {
//                HomeRepo = new HomeRepositoryTest(),
//                PeopleRepo = new PeopleRepositoryTest(),
//                DocumentRepo = new DocumentRepositoryTest()
//            };

//            s.SendMail(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<MailModel>(model), addRepo);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void PostMailShouldnPassBecauseOfInvalidAttachment()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary dict = new ModelStateDictionary();
//            MailService s = new MailService(dict, repo, new MailValidation());

//            model.Attachments = new System.Collections.Generic.List<int>() { 1, -1 };

//            MailController.AdditionalRepositories addRepo = new MailController.AdditionalRepositories(null)
//            {
//                HomeRepo = new HomeRepositoryTest(),
//                PeopleRepo = new PeopleRepositoryTest(),
//                DocumentRepo = new DocumentRepositoryTest()
//            };

//            s.SendMail(new ClientRepositoryTest().FindUserByMail("test@test.com"), ObjectCopier.Clone<MailModel>(model), addRepo);
//        }
//    }
//}