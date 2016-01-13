using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using System.Collections.Generic;

namespace ManahostManager.Domain.Repository
{
    public interface IDocumentCategoryRepository : IAbstractRepository<DocumentCategory>
    {
        DocumentCategory GetDocumentCategoryById(int id, int homeId);
    }

    public class DocumentCategoryRepository : AbstractRepository<DocumentCategory>, IDocumentCategoryRepository
    {
        public DocumentCategoryRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public DocumentCategory GetDocumentCategoryById(int id, int homeId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == homeId);
        }

        public override void Delete(DocumentCategory obj)
        {
            includes.Add("DocumentCategory");
            IEnumerable<Document> listBills = GetList<Document>(p => p.DocumentCategory.Id == obj.Id);

            foreach (Document cur in listBills)
                cur.DocumentCategory = null;
            base.Delete(obj);
        }
    }
}