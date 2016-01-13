using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Tools;
using Microsoft.AspNet.SignalR;
using Microsoft.Practices.Unity;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Web.Http.ModelBinding;
using System.Linq;

namespace ManahostManager.Hubs
{
    public abstract class AbstractHub : Hub
    {
        [Dependency]
        protected ClientUserManager Manager { get; set; }

        protected Client CurrentClient
        {
            get
            {
                int idClient = Context.User.Identity.GetUserId<int>();
                return Manager.FindById(idClient);
            }
            private set
            {
            }
        }

        protected bool ValidateModel<T>(T item, ModelStateDictionary validationDictionnary) where T : class
        {
            List<ValidationResult> results = new List<ValidationResult>();
            var ret = Validator.TryValidateObject(item, new ValidationContext(item), results, true);

            results.ForEach(result =>
            {
                foreach (string membername in result.MemberNames)
                {
                    validationDictionnary.AddModelError(membername, result.ErrorMessage);
                }
            });
            return ret;
        }

        protected bool AuthorizationCheck(string roles)
        {
            if (Context.User != null && Context.User.Identity.IsAuthenticated)
            {
                IEnumerable<string> rolesUserConnected = Manager.GetRoles(CurrentClient.Id);
                IEnumerable<string> RolesSplitted = roles.Split(',');
                if (RolesSplitted.Where(x => rolesUserConnected.Any(y => y.Equals(x))).Count() != 0)
                    return true;
            }
            return false;
        }
    }
}