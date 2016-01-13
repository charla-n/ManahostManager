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
    [RoutePrefix("api/People/FieldGroup")]
    [Route("")]
    public class FieldGroupController : AbstractAPIController
    {

        [Dependency]
        public IAbstractService<FieldGroup, FieldGroupDTO> FieldGroupService { get; set; }
        /// <summary>
        /// Post a fieldGroup
        /// </summary>
        /// <param name="FieldGroup">FieldGroupDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a fieldGroup
        /// <br/><para/>
        /// Allow the manager to add custom field for a people
        /// </remarks>
        /// <response code="200">FieldGroupDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [HttpPost]
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public FieldGroupDTO Post([FromBody] FieldGroupDTO FieldGroup)
        {
            return FieldGroupService.PrePostDTO(ModelState, currentClient, FieldGroup);
        }

        /// <summary>
        /// Put a fieldGroup
        /// </summary>
        /// <param name="FieldGroup">FieldGroupDTO</param>
        /// <param name="id">id of the FieldGroup to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a fieldGroup
        /// <br/><para/>
        /// Allow the manager to add custom field for a people
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [HttpPut]
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] FieldGroupDTO FieldGroup)
        {
            FieldGroupService.PrePutDTO(ModelState, currentClient, id, FieldGroup);
            return Ok();
        }

        /// <summary>
        /// Delete a fieldGroup
        /// </summary>
        /// <param name="id">id of the FieldGroup to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a fieldGroup
        /// <br/><para/>
        /// Allow the manager to add custom field for a people
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
            FieldGroupService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}