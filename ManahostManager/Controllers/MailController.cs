using ManahostManager.App_Start;
using ManahostManager.Domain.DAL;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Repository;
using ManahostManager.Model;
using ManahostManager.Services;
using ManahostManager.Utils;
using ManahostManager.Utils.Attributs;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System.Web.Http;

namespace ManahostManager.Controllers
{
    public class MailController : AbstractAPIController
    {
        private IMailConfigRepository repo;

        public MailController(IMailConfigRepository repo, IValidation<MailModel> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        public class AdditionalRepositories
        {
            public AdditionalRepositories(ManahostManagerDAL ctx)
            {
                PeopleRepo = new PeopleRepository(ctx);
                HomeRepo = new HomeRepository(ctx);
                DocumentRepo = new DocumentRepository(ctx);
            }

            public IPeopleRepository PeopleRepo { get; set; }

            public IHomeRepository HomeRepo { get; set; }

            public IDocumentRepository DocumentRepo { get; set; }
        }

        /// <summary>
        /// Send an email
        /// </summary>
        /// <param name="model">MailModel</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Send an email<br/><para/>
        /// Send emails to people ! You can only use this if you have an account configured<br/><para/>
        /// You have access to the log of the email sent if it was successful or not by looking looking at MailLog entity with the advanced search
        /// </remarks>
        /// <response code="200">MailLog</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public MailLogDTO Post([FromBody] MailModel model)
        {
            AdditionalRepositories addRepo = new AdditionalRepositories(repo.RetrieveContext());
            return WebApiApplication.container.Resolve<MailService>(new ResolverOverride[]
                {
                    paramDictionnary, paramRepo, paramValidation
                }).SendMail(currentClient, model, addRepo);
        }
    }
}