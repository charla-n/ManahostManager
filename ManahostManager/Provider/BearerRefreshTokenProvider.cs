using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Domain.Tools;
using ManahostManager.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace ManahostManager.Provider
{
    public class BearerRefreshTokenProvider : IAuthenticationTokenProvider
    {
        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientId = context.Ticket.Properties.Dictionary[GenericNames.AUTHENTICATION_CLIENT_ID_KEY];
            if (string.IsNullOrEmpty(clientId))
                return;

            var refreshTokenLifeTime = context.OwinContext.Get<int>(GenericNames.OWIN_CONTEXT_REFRESH_TOKEN_LIFETIME);

            var refreshToken = Guid.NewGuid().ToString("n");

            //var refreshTokenRepository = new RefreshTokenRepository(context.OwinContext.Get<ManahostManagerDAL>());
            /*var ClientManager = ClientUserManager.Create(null, context.OwinContext.Get<ManahostManagerDAL>());*/

            IDependencyScope Scope = context.OwinContext.Get<IDependencyScope>();
            ClientUserManager ClientManager = Scope.GetService(typeof(ClientUserManager)) as ClientUserManager;
            IRefreshTokenRepository refreshTokenRepository = Scope.GetService(typeof(IRefreshTokenRepository)) as IRefreshTokenRepository;

            var ServiceRepository = new ServiceRepository(context.OwinContext.Get<ManahostManagerDAL>());

            var client = await ClientManager.FindByEmailAsync(context.Ticket.Identity.Name);
            var service = ServiceRepository.GetUniq(x => x.Id == clientId);

            var token = new RefreshToken()
            {
                Id = refreshToken,
                Client = client,
                Service = service,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            };

            token.ProtectedTicket = context.SerializeTicket();
            refreshTokenRepository.Add(token);
            await refreshTokenRepository.SaveAsync();
            context.SetToken(refreshToken);
        }

        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var AllowedOrigin = context.OwinContext.Get<string>(GenericNames.OWIN_CONTEXT_CORS);
            context.OwinContext.Response.Headers.Remove(GenericNames.OWIN_CONTEXT_CORS_HEADER);
            context.OwinContext.Response.Headers.Add(GenericNames.OWIN_CONTEXT_CORS_HEADER, new[] { AllowedOrigin });

            IDependencyScope Scope = context.OwinContext.Get<IDependencyScope>();
            IRefreshTokenRepository refreshTokenRepository = Scope.GetService(typeof(IRefreshTokenRepository)) as IRefreshTokenRepository;

            var refreshToken = refreshTokenRepository.GetUniq(x => x.Id == context.Token);
            if (refreshToken != null)
            {
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                refreshTokenRepository.Delete(refreshToken);
            }
            return Task.FromResult<object>(null);
        }
    }
}