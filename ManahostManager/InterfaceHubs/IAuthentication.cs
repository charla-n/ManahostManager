using System.Threading.Tasks;

namespace ManahostManager.InterfaceHubs
{
    public interface IAuthentication
    {
        Task<bool> Authenticate(string AuthorizeHeader);
    }
}