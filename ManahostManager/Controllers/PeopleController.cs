using ManahostManager.App_Start;
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
    [RoutePrefix("api/People")]
    public class PeopleController : AbstractAPIController
    {

        public PeopleController() : base()
        {

        }

        [Dependency]
        public IAbstractService<People, PeopleDTO> PeopleService { get; set; }

        /// <summary>
        /// Post a people
        /// </summary>
        /// <param name="People">PeopleDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a product
        /// </remarks>
        /// <response code="200">ProductDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public PeopleDTO Post([FromBody] PeopleDTO People)
        {
            return PeopleService.PrePostDTO(ModelState, currentClient, People);
        }

        /// <summary>
        /// Put a people
        /// </summary>
        /// <param name="People">PeopleDTO</param>
        /// <param name="id">id of the people to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a people
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] PeopleDTO People)
        {
            PeopleService.PrePutDTO(ModelState, currentClient, id, People);
            return Ok();
        }

        /// <summary>
        /// Delete a people
        /// </summary>
        /// <param name="id">id of the people to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a people
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            PeopleService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}