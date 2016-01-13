using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using System.Collections.Generic;

namespace ManahostManager.Domain.Repository
{
    public interface IDocumentRepository : IAbstractRepository<Document>
    {
        Document GetDocumentById(int id, int? clientId);

        // Only used in DocumentController for the download
        Document GetDocumentById(int id);
    }

    public class DocumentRepository : AbstractRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public Document GetDocumentById(int id, int? clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }

        public Document GetDocumentById(int id)
        {
            return GetUniq(p => p.Id == id);
        }

        public override void Delete(Document obj)
        {
            IEnumerable<Bill> listBills = GetList<Bill>(p => p.Document.Id == obj.Id);

            foreach (Bill cur in listBills)
                cur.Booking = null;
            base.Delete(obj);
        }
    }
}