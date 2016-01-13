using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManahostManager.Domain.Repository
{
    public interface IDocumentLogRepository : IAbstractRepository<DocumentLog>
    {
        DocumentLog GetDocumentLogById(int clientId);

        Task<DocumentLog> GetDocumentLogByIdAsync(int clientId);
    }

    public class DocumentLogRepository : AbstractRepository<DocumentLog>, IDocumentLogRepository
    {
        public DocumentLogRepository(ManahostManagerDAL ctx)
            : base(ctx)
        {
            includes = new List<string>();
        }

        public DocumentLog GetDocumentLogById(int clientId)
        {
            return GetUniq(p => p.Id == clientId);
        }

        public Task<DocumentLog> GetDocumentLogByIdAsync(int clientId)
        {
            return GetUniqAsync(p => p.Id == clientId);
        }
    }
}