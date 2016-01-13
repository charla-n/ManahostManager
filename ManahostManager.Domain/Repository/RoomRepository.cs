using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IRoomRepository : IAbstractRepository<Room>
    {
        Room GetRoomById(int id, int clientId);
    }

    public class RoomRepository : AbstractRepository<Room>, IRoomRepository
    {
        public RoomRepository(ManahostManagerDAL ctx)
            : base(ctx)
        { }

        public Room GetRoomById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }

        public override void Delete(Room obj)
        {
            DeleteRange<Document>(obj.Documents);
            includes.Add("Room");
            DeleteRange<RoomBooking>(GetList<RoomBooking>(p => p.Room.Id == obj.Id));
            includes.Add("Room");
            DeleteRange<Bed>(GetList<Bed>(p => p.Room.Id == obj.Id));
            includes.Add("Room");
            DeleteRange<PricePerPerson>(GetList<PricePerPerson>(p => p.Room.Id == obj.Id));
            base.Delete(obj);
        }
    }
}