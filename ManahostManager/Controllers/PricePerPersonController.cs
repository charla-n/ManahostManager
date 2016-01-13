using ManahostManager.App_Start;
using ManahostManager.Domain.DAL;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Services;
using ManahostManager.Utils;
using ManahostManager.Utils.Attributs;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System.Web.Http;

namespace ManahostManager.Controllers
{
    [RoutePrefix("api/PricePerPerson")]
    public class PricePerPersonController : AbstractAPIController
    {

        [Dependency]
        public IAbstractService<PricePerPerson, PricePerPersonDTO> PricePerPersonService { get; set; }
        /// <summary>
        /// Post a PricePerPerson
        /// </summary>
        /// <param name="PricePerPerson">PricePerPersonDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a PricePerPerson
        /// <br/><para/>
        /// For a specified period, for a specific room and a specific peoplecategory set a price
        /// </remarks>
        /// <response code="200">PricePerPersonDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPost]
        public PricePerPersonDTO Post([FromBody] PricePerPersonDTO PricePerPerson)
        {
            return PricePerPersonService.PrePostDTO(ModelState, currentClient, PricePerPerson);
        }

        /// <summary>
        /// Put a PricePerPerson
        /// </summary>
        /// <param name="PricePerPerson">PricePerPersonDTO</param>
        /// <param name="id">id of the PricePerPerson to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a PricePerPerson
        /// <br/><para/>
        /// For a specified period, for a specific room and a specific peoplecategory set a price
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] PricePerPersonDTO PricePerPerson)
        {
            PricePerPersonService.PrePutDTO(ModelState, currentClient, id, PricePerPerson);
            return Ok();
        }

        /// <summary>
        /// Delete a PricePerPerson
        /// </summary>
        /// <param name="id">id of the PricePerPerson to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a PricePerPerson
        /// <br/><para/>
        /// For a specified period, for a specific room and a specific peoplecategory set a price
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            PricePerPersonService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}