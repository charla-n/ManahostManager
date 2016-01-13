using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;

namespace ManahostManager.Domain.Repository
{
    public interface IRefreshTokenRepository : IAbstractRepository<RefreshToken>
    {
    }

    public class RefreshTokenRepository : AbstractRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ManahostManagerDAL context) : base(context, true)
        {
        }
    }
}