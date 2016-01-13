using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IMailConfigRepository : IAbstractRepository<MailConfig>
    {
        MailConfig GetMailConfigById(int id, int clientId);
    }

    public class MailConfigRepository : AbstractRepository<MailConfig>, IMailConfigRepository
    {
        public MailConfigRepository(ManahostManagerDAL ctx) : base(ctx)
        {
        }

        public MailConfig GetMailConfigById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }
    }
}