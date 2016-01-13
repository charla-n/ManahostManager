using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Tools;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;

namespace ManahostManager.Utils.Attributs
{
    public sealed class ManahostAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException("actionContext");
            }

            IPrincipal user = actionContext.ControllerContext.RequestContext.Principal;
            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return false;
            }
            if (Users.Length > 0 && !(Users.IndexOf(user.Identity.Name, StringComparison.OrdinalIgnoreCase) >= 0))
                return false;
            if (Roles.Length > 0)
            {
                IDependencyScope Scope = actionContext.Request.GetOwinContext().Get<IDependencyScope>();
                ClientUserManager UserManager = Scope.GetService(typeof(ClientUserManager)) as ClientUserManager;
                var client = UserManager.FindByEmailAsync(user.Identity.Name).Result;
                var GroupList = UserManager.GetRolesAsync(client.Id).Result;
                var listRoles = Roles.Split(',');
                if (listRoles.Where(x => GroupList.Any(y => y.Equals(x))).Count() == 0)
                    return false;
            }
            return true;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);

            var dbContext = actionContext.Request.GetOwinContext().Get<ManahostManagerDAL>();
            IPrincipal user = Thread.CurrentPrincipal;
            var claimsIdentity = user.Identity as ClaimsIdentity;
            var actor = claimsIdentity.Claims.Where(x => x.Type == ClaimTypes.Actor).FirstOrDefault();
            if (actor != null)
            {
                var service = dbContext.ServiceTest.Where(x => x.Id == actor.Value).FirstOrDefault();
                if (service != null && !service.Active)
                    HandleDisabledService(actionContext);
            }
        }

        private void HandleDisabledService(HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException("actionContext");
            }
            actionContext.ModelState.AddModelError("client_id", GenericError.CLIENT_DISABLED);
            actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, actionContext.ModelState);
        }
    }
}