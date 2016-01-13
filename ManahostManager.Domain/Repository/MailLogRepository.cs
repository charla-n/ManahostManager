using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IMailLogRepository : IAbstractRepository<MailLog>
    {
        MailLog GetMailLogById(int id, int clientId);
    }

    public class MailLogRepository : AbstractRepository<MailLog>, IMailLogRepository
    {
        public MailLogRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public MailLog GetMailLogById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }
    }
}