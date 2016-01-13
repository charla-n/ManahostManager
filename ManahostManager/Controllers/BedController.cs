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
    [RoutePrefix("api/Room/Bed")]
    [Route("")]
    public class BedController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<Bed, BedDTO> BedService { get; set; }
        /// <summary>
        /// Post a bed
        /// </summary>
        /// <param name="Bed">BedDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a bed
        /// </remarks>
        /// <response code="200">BedDTO</response>
        /// <response code="400">
        /// KEY=Room VALUE=FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST<br/><br/><para/><para/>
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.VIP)]
        public BedDTO Post([FromBody] BedDTO Bed)
        {
            return BedService.PrePostDTO(ModelState, currentClient, Bed);
        }

        /// <summary>
        /// Put a bed
        /// </summary>
        /// <param name="Bed">BedDTO</param>
        /// <param name="id">id of the bed to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a bed
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// KEY=Bed VALUE=FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST<br/><br/><para/><para/>
        /// KEY=Room VALUE=FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST<br/><br/><para/><para/>
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] BedDTO Bed)
        {
            BedService.PrePutDTO(ModelState, currentClient, id, Bed);
            return Ok();
        }

        /// <summary>
        /// Delete a bed
        /// </summary>
        /// <param name="id">Id of the bed</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a bed
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// KEY=Bed VALUE=FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST<br/><br/><para/><para/>
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.VIP)]
        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            BedService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}