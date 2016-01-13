using ManahostManager.Utils.API.Localisation.Model;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ManahostManager.Utils.API.Localisation.ByCountry
{
    public class FranceAutoComplete : ILocalisationAutoComplete<GeoCodeJSONModel, string>
    {
        public static readonly string API_URL_BASE = "http://api-adresse.data.gouv.fr/";
        public static readonly string API_REQUEST = "search/?q={0}";

        public async Task<GeoCodeJSONModel> Search(string obj)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(API_URL_BASE);
                var response = await client.GetAsync(string.Format(API_REQUEST, obj));
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return null;
                }
                var objResult = await response.Content.ReadAsAsync<GeoCodeJSONModel>();
                var newFeatures = objResult.features.Where(x => x.properties.type == "city" || x.properties.type == "village");
                objResult.features = newFeatures;
                return objResult;
            }
        }

        public async Task<GeoCodeJSONModel> Search(string city, string street)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(API_URL_BASE);
                var research = string.Format("{0} {1}", street, city);
                var response = await client.GetAsync(string.Format(API_REQUEST + "&type=street", research));
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return null;
                }
                var objResult = await response.Content.ReadAsAsync<GeoCodeJSONModel>();
                var newFeatures = objResult.features.Where(x => x.properties.city == city);
                objResult.features = newFeatures;
                return objResult;
            }
        }
    }
}