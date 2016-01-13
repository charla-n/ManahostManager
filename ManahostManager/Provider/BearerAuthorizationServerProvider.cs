using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Tools;
using ManahostManager.Utils;
using ManahostManager.Utils.Captcha;
using ManahostManager.Utils.ExternalLoginTools;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace ManahostManager.Provider
{
    /// <summary>
    ///  Cette classe sert de provider pour la route "/Token", elle permet une authentification de type FORM.
    /// et renvoie les différentes informations dont access_token
    /// </summary>
    public class BearerAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// Methode qui permet la validation du Service AKA Client web ou mobile, via le client_id et optionnellement le client_secret.
        /// </summary>
        /// <param name="context">Le context de la requête et d'autre information utiles à la gestion du service</param>
        /// <returns></returns>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string client_id = string.Empty;
            string client_secret = string.Empty;

            if (!context.TryGetBasicCredentials(out client_id, out client_secret))
            {
                context.TryGetFormCredentials(out client_id, out client_secret);
            }
            Service service = null;
            try
            {
                service = await AuthenticationTools.GetServiceActiveAndExists(context.OwinContext.Get<ManahostManagerDAL>(), context.ClientId, client_secret);
            }
            catch (AuthenticationToolsException e)
            {
                context.SetError(e.Key, e.Value);
                return;
            }
            context.OwinContext.Set<string>(GenericNames.OWIN_CONTEXT_CORS, service.AllowedOrigin);
            context.OwinContext.Set<int>(GenericNames.OWIN_CONTEXT_REFRESH_TOKEN_LIFETIME, service.RefreshTokenLifeTime);
            context.Validated();
        }

        /// <summary>
        /// Methode qui sert à la vérification de l'authentification du client
        /// </summary>
        /// <param name="context">Le context de la requête et d'autre information utiles à la gestions du service</param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            IDependencyScope Scope = context.OwinContext.Get<IDependencyScope>();
            ClientUserManager manager = Scope.GetService(typeof(ClientUserManager)) as ClientUserManager;
            ClientRoleManager managerRoles = Scope.GetService(typeof(ClientRoleManager)) as ClientRoleManager;

            /*ClientUserManager manager = context.OwinContext.GetUserManager<ClientUserManager>();
            ClientRoleManager managerRoles = context.OwinContext.Get<ClientRoleManager>();*/

            var AllowOriginCORS = context.OwinContext.Get<string>(GenericNames.OWIN_CONTEXT_CORS);
            var attempt = Convert.ToInt32(ConfigurationManager.AppSettings[GenericNames.CAPTCHA_FAILED_COUNT]);
            int RefreshTokenLifeTime = context.OwinContext.Get<int>(GenericNames.OWIN_CONTEXT_REFRESH_TOKEN_LIFETIME);

            if (AllowOriginCORS == null)
                AllowOriginCORS = "*";

            context.OwinContext.Response.Headers.Remove(GenericNames.OWIN_CONTEXT_CORS_HEADER);
            context.OwinContext.Response.Headers.Add(GenericNames.OWIN_CONTEXT_CORS_HEADER, new[] { AllowOriginCORS });

            Client user = await manager.FindAsync(context.UserName, context.Password);
            if (user == null)
            {
                Client FindByEmail = await manager.FindByEmailAsync(context.UserName);
                if (FindByEmail != null)
                {
                    FindByEmail.AccessFailedCount++;
                    FindByEmail.LastAttemptConnexion = DateTime.UtcNow;
                    await manager.UpdateAsync(FindByEmail);
                    if (FindByEmail.AccessFailedCount > attempt)
                    {
                        context.SetError("captcha", GenericError.NEED_CAPTCHA);
                        return;
                    }
                }
                context.SetError("invalid_grant", GenericError.INVALID_GIVEN_PARAMETER);
                return;
            }
            if (!user.EmailConfirmed)
            {
                context.SetError("email_confirmation", GenericError.EMAIL_NOT_CONFIRMED);
                return;
            }
            if (user.LockoutEnabled)
            {
                context.SetError("client", GenericError.ACCOUNT_DISABLED);
                return;
            }

            AuthenticationTicket ticket = null;

            if (user.AccessFailedCount > attempt)
            {
                var data = await context.Request.ReadFormAsync();
                var Code = data[GenericNames.GOOGLE_RECAPTCHA_FORM];
                if (Code == null)
                {
                    context.SetError("captcha", GenericError.CAPTCHA_MISSING_RESPONSE);
                    return;
                }
                else
                {
                    ICaptchaTools tools = GoogleReCaptchValidator.Create();
                    var testCaptcha = await tools.VerifyCaptcha(Code, context.Request.RemoteIpAddress);
                    if (testCaptcha)
                    {
                        user.AccessFailedCount = 0;
                        await manager.UpdateAsync(user);
                        ticket = AuthenticationTools.GenerateTicket(context.Options.AuthenticationType, context.ClientId, user, RefreshTokenLifeTime);
                    }
                    else
                    {
                        context.SetError("captcha", GenericError.CAPTCHA_INVALID_SOLUTION);
                        return;
                    }
                }
            }
            else
                ticket = AuthenticationTools.GenerateTicket(context.Options.AuthenticationType, context.ClientId, user, RefreshTokenLifeTime);

            context.Validated(ticket);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            /*ClientUserManager manager = context.OwinContext.GetUserManager<ClientUserManager>();
            ClientRoleManager managerRoles = context.OwinContext.Get<ClientRoleManager>();*/

            IDependencyScope Scope = context.OwinContext.Get<IDependencyScope>();
            ClientUserManager manager = Scope.GetService(typeof(ClientUserManager)) as ClientUserManager;
            ClientRoleManager managerRoles = Scope.GetService(typeof(ClientRoleManager)) as ClientRoleManager;

            var originalClient = context.Ticket.Properties.Dictionary[GenericNames.AUTHENTICATION_CLIENT_ID_KEY];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("client_id", GenericError.INVALID_GIVEN_PARAMETER);
                return Task.FromResult<object>(null);
            }

            var identity = new ClaimsIdentity(context.Ticket.Identity);
            var newTicket = new AuthenticationTicket(identity, context.Ticket.Properties);
            context.Validated(newTicket);
            return Task.FromResult<object>(null);
        }
    }
}