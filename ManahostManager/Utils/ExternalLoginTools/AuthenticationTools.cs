using ManahostManager.App_Start;
using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Domain.Tools;
using ManahostManager.Model.DTO.Account;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ManahostManager.Utils.ExternalLoginTools
{
    public class AuthenticationTools
    {
        public static readonly TimeSpan TicketExpirationTime = TimeSpan.FromDays(1);

        public static ExternalLocalAccessTokenModel GenerateToken(AuthenticationTicket ticket, Client User)
        {
            var accessToken = WebApiApplication.OptionsServerOAuth.AccessTokenFormat.Protect(ticket);

            return new ExternalLocalAccessTokenModel()
            {
                access_token = accessToken,
                expires_in = TicketExpirationTime.TotalSeconds.ToString(),
                userName = User.Email,
                expires = ticket.Properties.ExpiresUtc.ToString(),
                issued = ticket.Properties.IssuedUtc.ToString(),
                refresh_token = null,
                token_type = "Bearer"
            };
        }

        public static async Task<Service> GetServiceActiveAndExists(ManahostManagerDAL ctx, string client_id, string client_secret)
        {
            if (client_id == null)
            {
                throw new AuthenticationToolsException("client_id", GenericError.INVALID_GIVEN_PARAMETER);
            }

            var ServiceRepo = new ServiceRepository(ctx);
            var service = await ServiceRepo.GetUniqAsync(x => x.Id == client_id);
            if (service == null)
            {
                throw new AuthenticationToolsException("client_id", GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
            }
            if (service.ApplicationType == ApplicationTypes.NATIVE_CLIENT)
            {
                if (string.IsNullOrWhiteSpace(client_secret))
                {
                    throw new AuthenticationToolsException("client_secret", GenericError.INVALID_GIVEN_PARAMETER);
                }
                if (new BcryptPasswordHasher().VerifyHashedPassword(service.Secret, client_secret) == PasswordVerificationResult.Failed)
                {
                    throw new AuthenticationToolsException("client_secret", GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
                }
            }
            if (!service.Active)
            {
                throw new AuthenticationToolsException("client_id", GenericError.CLIENT_DISABLED);
            }
            return service;
        }

        public static AuthenticationTicket GenerateTicket(string OAuthAuthenticationType, string client_id, Client user, int refresh_token_expires = 0)
        {
            var tokenExpiration = TimeSpan.FromDays(1);
            var identity = new ClaimsIdentity(OAuthAuthenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Actor, client_id));

            var dataDico = new Dictionary<string, string>() {
                {
                    GenericNames.AUTHENTICATION_CLIENT_ID_KEY , client_id
                },
                {
                    GenericNames.AUTHENTICATION_REFRESH_TOKEN_LIFETIME, Convert.ToString(refresh_token_expires)
                }
            };

            var props = new AuthenticationProperties(dataDico)
            {
                IssuedUtc = DateTime.Now,
                ExpiresUtc = DateTime.Now.Add(tokenExpiration)
            };
            return new AuthenticationTicket(identity, props);
        }
    }

    public class AuthenticationToolsException : Exception
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public AuthenticationToolsException(string key, string value) : base()
        {
            Key = key;
            Value = value;
        }
    }
}