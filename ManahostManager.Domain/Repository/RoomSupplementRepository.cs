using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IRoomSupplementRepository : IAbstractRepository<RoomSupplement>
    {
        RoomSupplement GetRoomSupplementById(int id, int clientId);
    }

    public class RoomSupplementRepository : AbstractRepository<RoomSupplement>, IRoomSupplementRepository
    {
        public RoomSupplementRepository(ManahostManagerDAL ctx) : base(ctx)
        {
        }

        public RoomSupplement GetRoomSupplementById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }

        public override void Delete(RoomSupplement obj)
        {
            includes.Add("RoomSupplement");
            DeleteRange<SupplementRoomBooking>(GetList<SupplementRoomBooking>(p => p.RoomSupplement.Id == obj.Id));
            base.Delete(obj);
        }
    }
}