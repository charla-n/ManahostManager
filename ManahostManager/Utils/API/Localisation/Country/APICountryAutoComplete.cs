using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ManahostManager.Utils.API.Localisation.Country
{
    public class CountryViewModel
    {
        public string name { get; set; }
    }

    public class APICountryAutoComplete : ILocalisationAutoComplete<IEnumerable<CountryViewModel>, string>
    {
        public static readonly string API_URL_BASE = "https://restcountries.eu/";
        public static readonly string API_REQUEST = "/rest/v1/name/{0}";

        public async Task<IEnumerable<CountryViewModel>> Search(string nameCountry)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(API_URL_BASE);

                var response = await client.GetAsync(string.Format(API_REQUEST, nameCountry));
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return null;
                }
                return await response.Content.ReadAsAsync<IEnumerable<CountryViewModel>>();
            }
        }

        public Task<IEnumerable<CountryViewModel>> Search(string obj, string obj2)
        {
            throw new NotImplementedException();
        }
    }
}