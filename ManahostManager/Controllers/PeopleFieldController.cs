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
    [RoutePrefix("api/People/Field")]
    [Route("")]
    public class PeopleFieldController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<PeopleField, PeopleFieldDTO> PeopleFieldService { get; set; }
        /// <summary>
        /// Post a PeopleField
        /// </summary>
        /// <param name="PeopleField">PeopleFieldDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a PeopleField
        /// </remarks>
        /// <response code="200">PeopleFieldDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [HttpPost]
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public PeopleFieldDTO Post([FromBody] PeopleFieldDTO PeopleField)
        {
            return PeopleFieldService.PrePostDTO(ModelState, currentClient, PeopleField);
        }

        /// <summary>
        /// Put a PeopleField
        /// </summary>
        /// <param name="PeopleField">PeopleFieldDTO</param>
        /// <param name="id">id of the PeopleField to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a PeopleField
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [HttpPut]
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] PeopleFieldDTO PeopleField)
        {
            PeopleFieldService.PrePutDTO(ModelState, currentClient, id, PeopleField);
            return Ok();
        }

        /// <summary>
        /// Delete a PeopleField
        /// </summary>
        /// <param name="PeopleField">PeopleFieldDTO</param>
        /// <param name="id">id of the PeopleField to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a PeopleField
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [HttpDelete]
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [Route("{id:int}")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            PeopleFieldService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}