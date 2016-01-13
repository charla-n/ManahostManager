using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IKeyGeneratorRepository : IAbstractRepository<KeyGenerator>
    {
        KeyGenerator GetKeyGeneratorByClientId(int id, int idClient);

        KeyGenerator GetKeyGeneratorByHomeId(int id, int idHome);

        KeyGenerator GetKeyGeneratorByIdForManager(int id);
    }

    public class KeyGeneratorRepository : AbstractRepository<KeyGenerator>, IKeyGeneratorRepository
    {
        public KeyGeneratorRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public KeyGenerator GetKeyGeneratorByClientId(int id, int idClient)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == idClient);
        }

        public KeyGenerator GetKeyGeneratorByHomeId(int id, int idHome)
        {
            return GetUniq(p => p.Id == id && p.Home.Id == idHome);
        }

        public KeyGenerator GetKeyGeneratorByIdForManager(int id)
        {
            return GetUniq(p => p.Id == id);
        }
    }
}