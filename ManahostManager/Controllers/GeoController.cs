using ManahostManager.Utils;
using ManahostManager.Utils.API.Localisation;
using ManahostManager.Utils.API.Localisation.Country;
using ManahostManager.Utils.API.Localisation.Model;
using ManahostManager.Utils.Attributs;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace ManahostManager.Controllers
{
    [RoutePrefix("Api/Geo")]
    public class GeoController : ApiController
    {
        public APICountryAutoComplete ApiCountry { get; set; }

        public GeoController(APICountryAutoComplete APICountry)
        {
            ApiCountry = APICountry;
        }

        [HttpGet]
        [ManahostAuthorize(Roles = GenericNames.REGISTERED_VIP)]
        [Route("{nameCountry}")]
        public async Task<IHttpActionResult> Get(string nameCountry = "")
        {
            if (nameCountry == "" || string.IsNullOrWhiteSpace(nameCountry))
            {
                ModelState.AddModelError("nameCountry", GenericError.INVALID_GIVEN_PARAMETER);
                return BadRequest(ModelState);
            }
            var result = await ApiCountry.Search(nameCountry);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("{nameCountry}/{nameCity}")]
        [ManahostAuthorize(Roles = GenericNames.REGISTERED_VIP)]
        public async Task<IHttpActionResult> Get(string nameCountry = "", string nameCity = "")
        {
            if (nameCountry == "" || string.IsNullOrWhiteSpace(nameCountry))
            {
                ModelState.AddModelError("nameCountry", GenericError.INVALID_GIVEN_PARAMETER);
                return BadRequest(ModelState);
            }
            if (nameCity == "" || string.IsNullOrWhiteSpace(nameCity))
            {
                ModelState.AddModelError("nameCity", GenericError.INVALID_GIVEN_PARAMETER);
                return BadRequest(ModelState);
            }
            string className = string.Format("ManahostManager.Utils.API.Localisation.ByCountry.{0}AutoComplete", nameCountry);
            var classObj = Activator.CreateInstance("ManahostManager", className);
            var classUnWrapped = classObj.Unwrap() as ILocalisationAutoComplete<GeoCodeJSONModel, string>;
            var result = await classUnWrapped.Search(nameCity);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

        [HttpGet]
        [Route("{nameCountry}/{nameCity}/{nameStreet}")]
        [ManahostAuthorize(Roles = GenericNames.REGISTERED_VIP)]
        public async Task<IHttpActionResult> Get(string nameCountry = "", string nameCity = "", string nameStreet = "")
        {
            if (nameCountry == "" || string.IsNullOrWhiteSpace(nameCountry))
            {
                ModelState.AddModelError("nameCountry", GenericError.INVALID_GIVEN_PARAMETER);
                return BadRequest(ModelState);
            }
            if (nameCity == "" || string.IsNullOrWhiteSpace(nameCity))
            {
                ModelState.AddModelError("nameCity", GenericError.INVALID_GIVEN_PARAMETER);
                return BadRequest(ModelState);
            }
            if (nameStreet == "" || string.IsNullOrWhiteSpace(nameStreet))
            {
                ModelState.AddModelError("nameStreet", GenericError.INVALID_GIVEN_PARAMETER);
                return BadRequest(ModelState);
            }
            string className = string.Format("ManahostManager.Utils.API.Localisation.ByCountry.{0}AutoComplete", nameCountry);
            var classObj = Activator.CreateInstance("ManahostManager", className);
            var classUnWrapped = classObj.Unwrap() as ILocalisationAutoComplete<GeoCodeJSONModel, string>;
            var result = await classUnWrapped.Search(nameCity, nameStreet);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }
    }
}