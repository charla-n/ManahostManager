using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using System.Collections.Generic;

namespace ManahostManager.Domain.Repository
{
    public interface IPeopleFieldRepository : IAbstractRepository<PeopleField>
    {
        PeopleField GetPeopleFieldById(int id, int clientId);

        IEnumerable<PeopleField> GetPeopleFieldsByFieldGroup(int idGrp, int clientId);
    }

    public class PeopleFieldRepository : AbstractRepository<PeopleField>, IPeopleFieldRepository
    {
        public PeopleFieldRepository(ManahostManagerDAL ctx)
            : base(ctx)
        { }

        public PeopleField GetPeopleFieldById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }

        public IEnumerable<PeopleField> GetPeopleFieldsByFieldGroup(int idGrp, int clientId)
        {
            return GetList(p => p.FieldGroup.Id == idGrp && p.Home.ClientId == clientId);
        }
    }
}