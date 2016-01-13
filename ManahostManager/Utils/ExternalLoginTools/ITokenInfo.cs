using ManahostManager.Model.DTO.Account;
using System.Threading.Tasks;

namespace ManahostManager.Utils.ExternalLoginTools
{
    public interface ITokenInfo
    {
        Task<ParsedExternalAccessToken> Get(string accessToken);

        Task<ParsedExternalAccessTokenUserInfo> GetUserInfo(string accessToken);
    }
}