using System.Threading.Tasks;

namespace ManahostManager.Utils.API.Localisation
{
    public enum StreetOrCity
    {
        CITY, STREET
    };

    public interface ILocalisationAutoComplete
    {
    }

    public interface ILocalisationAutoComplete<T, U> : ILocalisationAutoComplete
        where T : class
        where U : class
    {
        Task<T> Search(U obj);

        Task<T> Search(U obj, U obj2);
    }
}