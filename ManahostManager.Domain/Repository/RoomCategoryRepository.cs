using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using System.Collections.Generic;

namespace ManahostManager.Domain.Repository
{
    public interface IRoomCategoryRepository : IAbstractRepository<RoomCategory>
    {
        RoomCategory GetRoomCategoryById(int id, int clientId);
    }

    public class RoomCategoryRepository : AbstractRepository<RoomCategory>, IRoomCategoryRepository
    {
        public RoomCategoryRepository(ManahostManagerDAL ctx)
            : base(ctx)
        { }

        public RoomCategory GetRoomCategoryById(int id, int clientId)
        {
            return GetUniq(p => p.Id == id && p.Home.ClientId == clientId);
        }

        public override void Delete(RoomCategory obj)
        {
            IEnumerable<Room> rooms = GetList<Room>(p => p.RoomCategory.Id == obj.Id);

            foreach (Room cur in rooms)
            {
                cur.RoomCategory = null;
                Update<Room>(cur);
            }
            base.Delete(obj);
        }
    }
}