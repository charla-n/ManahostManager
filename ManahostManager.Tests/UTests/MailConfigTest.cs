//using ManahostManager.Domain.Entity;
//using ManahostManager.Domain.Repository;
//using ManahostManager.Services;
//using ManahostManager.Tests.Repository;
//using ManahostManager.Tests.UTests.UTsUtils;
//using ManahostManager.Utils;
//using ManahostManager.Validation;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Web.Http.ModelBinding;

//namespace ManahostManager.Tests
//{
//    [TestClass]
//    public class MailConfigTest
//    {
//        private Client client0;

//        public MailConfigTest()
//        {
//            client0 = new Client();
//            client0.Id = 0;
//            client0.DefaultHomeId = 0;
//        }

//        [TestMethod]
//        public void doPostTest()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MailConfigService c = new MailConfigService(wrap, repo, new MailConfigValidation());

//            {
//                MailConfig mailc = repo.GetMailConfigById(0, 0);
//                c.PrePost(client0, ObjectCopier.Clone<MailConfig>(mailc), null);
//                Assert.IsTrue(true);
//                Assert.AreEqual(null, repo.Entity.DateModification);
//            }
//        }

//        [TestMethod]
//        public void doPostTestShouldntPassBecauseOfForbiddenHomeId()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MailConfigService c = new MailConfigService(wrap, repo, new MailConfigValidation());

//            {
//                MailConfig mailc = repo.GetMailConfigById(0, 0);
//                repo.Invalid = true;
//                try
//                {
//                    c.PrePost(client0, ObjectCopier.Clone<MailConfig>(mailc), null);
//                    Assert.IsTrue(false);
//                }
//                catch (ManahostValidationException)
//                {
//                    DictionnaryAssert.DictionnaryHasValueAndError(wrap, "HomeId",
//    GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//                }
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPostTestMailNullShouldntWork()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MailConfigService c = new MailConfigService(wrap, repo, new MailConfigValidation());

//            {
//                MailConfig mailc = repo.GetMailConfigById(0, 0);
//                mailc.Email = null;
//                c.PrePost(client0, ObjectCopier.Clone<MailConfig>(mailc), null);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPostTestSmtpNullShouldntWork()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MailConfigService c = new MailConfigService(wrap, repo, new MailConfigValidation());

//            {
//                MailConfig mailc = repo.GetMailConfigById(0, 0);
//                mailc.Smtp = null;
//                c.PrePost(client0, ObjectCopier.Clone<MailConfig>(mailc), null);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPostTestEntityNullShouldntWork()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MailConfigService c = new MailConfigService(wrap, repo, new MailConfigValidation());

//            {
//                c.PrePost(client0, null, null);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPostTestSmtpPortNullShouldntWork()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MailConfigService c = new MailConfigService(wrap, repo, new MailConfigValidation());

//            {
//                MailConfig mailc = repo.GetMailConfigById(0, 0);
//                mailc.SmtpPort = null;
//                c.PrePost(client0, ObjectCopier.Clone<MailConfig>(mailc), null);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPostTestSmtpPortNegShouldntWork()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MailConfigService c = new MailConfigService(wrap, repo, new MailConfigValidation());

//            {
//                MailConfig mailc = repo.GetMailConfigById(0, 0);
//                mailc.SmtpPort = -1;
//                c.PrePost(client0, ObjectCopier.Clone<MailConfig>(mailc), null);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPostTestSmtpPortPosShouldntWork()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MailConfigService c = new MailConfigService(wrap, repo, new MailConfigValidation());

//            {
//                MailConfig mailc = repo.GetMailConfigById(0, 0);
//                mailc.SmtpPort = 1000000;
//                c.PrePost(client0, ObjectCopier.Clone<MailConfig>(mailc), null);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPostTestTitleNullShouldntWork()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MailConfigService c = new MailConfigService(wrap, repo, new MailConfigValidation());

//            {
//                MailConfig mailc = repo.GetMailConfigById(0, 0);
//                mailc.Title = null;
//                c.PrePost(client0, ObjectCopier.Clone<MailConfig>(mailc), null);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPostTestMailFalseShouldntWork()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MailConfigService c = new MailConfigService(wrap, repo, new MailConfigValidation());

//            {
//                MailConfig mailc = repo.GetMailConfigById(0, 0);
//                mailc.Email = "MAUVAIS MAIL";
//                c.PrePost(client0, ObjectCopier.Clone<MailConfig>(mailc), null);
//            }
//        }

//        [TestMethod]
//        public void doPutTest()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MailConfigService c = new MailConfigService(wrap, repo, new MailConfigValidation());

//            {
//                MailConfig mailc = repo.GetMailConfigById(0, 0);
//                repo.Entity = mailc;
//                mailc.Email = "test@test.com";
//                c.PrePut(client0, ObjectCopier.Clone<MailConfig>(mailc), null);
//                Assert.IsTrue(true);
//                Assert.AreEqual<String>("test@test.com", repo.Entity.Email);
//                Assert.IsNotNull(repo.Entity.DateModification);
//            }
//        }

//        [TestMethod]
//        public void doPutTestShouldntPassBecauseOfForbiddenHomeId()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MailConfigService c = new MailConfigService(wrap, repo, new MailConfigValidation());

//            {
//                MailConfig mailc = repo.GetMailConfigById(0, 0);
//                repo.Entity = mailc;
//                mailc.Email = "test@test.com";
//                repo.Invalid = true;
//                try
//                {
//                    c.PrePut(client0, ObjectCopier.Clone<MailConfig>(mailc), null);
//                    Assert.IsTrue(false);
//                }
//                catch (ManahostValidationException)
//                {
//                    DictionnaryAssert.DictionnaryHasValueAndError(wrap, "HomeId",
//                        GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
//                }
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPutTestMailFalseShouldntWork()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MailConfigService c = new MailConfigService(wrap, repo, new MailConfigValidation());

//            {
//                MailConfig mailc = repo.GetMailConfigById(0, 0);
//                repo.Entity = mailc;
//                mailc.Email = "test";
//                c.PrePut(client0, ObjectCopier.Clone<MailConfig>(mailc), null);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPutTestEntityNullShouldntWork()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MailConfigService c = new MailConfigService(wrap, repo, new MailConfigValidation());

//            {
//                c.PrePut(client0, null, null);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPutTestSmtpPortNegShouldntWork()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MailConfigService c = new MailConfigService(wrap, repo, new MailConfigValidation());

//            {
//                MailConfig mailc = repo.GetMailConfigById(0, 0);
//                mailc.SmtpPort = -1;
//                c.PrePut(client0, ObjectCopier.Clone<MailConfig>(mailc), null);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doPutTestSmtpPortPosShouldntWork()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MailConfigService c = new MailConfigService(wrap, repo, new MailConfigValidation());

//            {
//                MailConfig mailc = repo.GetMailConfigById(0, 0);
//                mailc.SmtpPort = 1000000;
//                c.PrePut(client0, ObjectCopier.Clone<MailConfig>(mailc), null);
//            }
//        }

//        [TestMethod]
//        public void doDeleteTest()
//        {
//            IMailConfigRepository repo = new MailConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MailConfigService c = new MailConfigService(wrap, repo, new MailConfigValidation());

//            {
//                c.PreDelete(client0, 0, null);
//                Assert.IsTrue(true);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doDeleteTestUnauthorizedShouldntWork()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MailConfigService c = new MailConfigService(wrap, repo, new MailConfigValidation());

//            {
//                client0.DefaultHomeId = 1;
//                c.PreDelete(client0, -1, null);
//            }
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ManahostValidationException))]
//        public void doDeleteTestMailConfigDoesntExistShouldntWork()
//        {
//            MailConfigRepositoryTest repo = new MailConfigRepositoryTest();

//            ModelStateDictionary wrap = new ModelStateDictionary();
//            MailConfigService c = new MailConfigService(wrap, repo, new MailConfigValidation());

//            {
//                c.PreDelete(client0, 99, null);
//            }
//        }
//    }
//}