using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IPricePerPersonRepository : IAbstractRepository<PricePerPerson>
    {
        PricePerPerson GetPricePerPersonById(int id, int clientId);

        PricePerPerson GetPricePerPersonByRoomIdAndPeriod(int idRoom, int idPeriod);
    }

    public class PricePerPersonRepository : AbstractRepository<PricePerPerson>, IPricePerPersonRepository
    {
        public PricePerPersonRepository(ManahostManagerDAL ctx) : base(ctx)
        { }

        public PricePerPerson GetPricePerPersonById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }

        public PricePerPerson GetPricePerPersonByRoomIdAndPeriod(int idRoom, int idPeriod)
        {
            includes.Add("Period");
            includes.Add("Room");
            return GetUniq(p => p.Period.Id == idPeriod && p.Room.Id == idRoom);
        }
    }
}