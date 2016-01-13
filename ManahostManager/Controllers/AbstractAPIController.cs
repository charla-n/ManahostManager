using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Domain.Tools;
using ManahostManager.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Practices.Unity;
using System.Net.Http;
using System.Web.Http;

namespace ManahostManager.Controllers
{
    public class AbstractAPIController : ApiController
    {
        protected ParameterOverride paramDictionnary { get; private set; }

        protected ParameterOverride paramRepo { get; private set; }

        protected ParameterOverride paramValidation { get; private set; }

        private IAbstractRepository Repo
        {
            get; set;
        }

        [Dependency]
        public ClientUserManager Manager { get; set; }

        public Client currentClient
        {
            get
            {
                int Id = User.Identity.GetUserId<int>();
                return Manager.FindById(Id);
            }
            set
            {
            }
        }

        public AbstractAPIController()
        {
        }

        public AbstractAPIController(IAbstractRepository repo, IValidation validation)
        {
            paramDictionnary = new ParameterOverride("validationDictionnary", this.ModelState);
            paramRepo = new ParameterOverride("repo", repo);
            paramValidation = new ParameterOverride("validation", validation);
            Repo = repo;
        }

        public AbstractAPIController(IAbstractRepository repo)
        {
            paramDictionnary = new ParameterOverride("validationDictionnary", this.ModelState);
            paramRepo = new ParameterOverride("repo", repo);
            Repo = repo;
        }
    }
}