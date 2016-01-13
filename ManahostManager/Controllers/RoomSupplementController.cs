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
    [RoutePrefix("api/Room/Supplement")]
    [Route("")]
    public class RoomSupplementController : AbstractAPIController
    {

        [Dependency]
        public IAbstractService<RoomSupplement, RoomSupplementDTO> RoomSupplementService { get; set; }
        /// <summary>
        /// Post a RoomSupplement
        /// </summary>
        /// <param name="RoomSupplement">RoomSupplementDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a RoomSupplement
        /// </remarks>
        /// <response code="200">RoomSupplementDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public RoomSupplementDTO Post([FromBody] RoomSupplementDTO RoomSupplement)
        {
            return RoomSupplementService.PrePostDTO(ModelState, currentClient, RoomSupplement);
        }

        /// <summary>
        /// Put a RoomSupplement
        /// </summary>
        /// <param name="RoomSupplement">RoomSupplementDTO</param>
        /// <param name="id">id of the RoomSupplement to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a RoomSupplement
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] RoomSupplementDTO RoomSupplement)
        {
            RoomSupplementService.PrePutDTO(ModelState, currentClient, id, RoomSupplement);
            return Ok();
        }

        /// <summary>
        /// Delete a RoomSupplement
        /// </summary>
        /// <param name="id">id of the RoomSupplement to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a RoomSupplement
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [Route("{id:int}")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            RoomSupplementService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}