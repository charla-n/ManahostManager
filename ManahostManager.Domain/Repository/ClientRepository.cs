using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Tools;
using System;
using System.Threading.Tasks;

namespace ManahostManager.Domain.Repository
{
    public interface IClientRepository : IDisposable
    {
        Task<Client> FindUserByMailAsync(string mail);

        Client FindUserByMail(string mail);

        Task<Client> FindUserAsync(string userName, string password);

        Client FindUser(string userName, string password);

        Task<Client> FindUserByIdAsync(int id);

        Client FindUserById(int id);
    }

    public class ClientRepository : IClientRepository
    {
        private ManahostManagerDAL _ctx;

        private ClientUserManager _userManager;

        public ClientRepository(ManahostManagerDAL ctx)
        {
            _ctx = ctx;
            _userManager = new ClientUserManager(new CustomUserStore(_ctx));
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
        }

        public async Task<Client> FindUserByMailAsync(string mail)
        {
            return await _userManager.FindByEmailAsync(mail);
        }

        public async Task<Client> FindUserAsync(string userName, string password)
        {
            return await _userManager.FindAsync(userName, password);
        }

        public Client FindUserByMail(string mail)
        {
            return FindUserByMailAsync(mail).Result;
        }

        public Client FindUser(string userName, string password)
        {
            return FindUserAsync(userName, password).Result;
        }

        public async Task<Client> FindUserByIdAsync(int id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public Client FindUserById(int id)
        {
            return FindUserByIdAsync(id).Result;
        }
    }
}