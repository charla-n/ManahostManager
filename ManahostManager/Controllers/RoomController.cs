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
    [RoutePrefix("api/Room")]
    public class RoomController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<Room, RoomDTO> RoomService { get; set; }
        /// <summary>
        /// Post a room
        /// </summary>
        /// <param name="Room">RoomDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a room
        /// </remarks>
        /// <response code="200">RoomDTO</response>
        /// <response code="400">
        /// KEY=RoomCategory VALUE=FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST<br/><br/><para/><para/>
        /// KEY=BillItemCategory VALUE=FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST<br/><br/><para/><para/>
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.VIP)]
        public RoomDTO Post([FromBody] RoomDTO Room)
        {
            return RoomService.PrePostDTO(ModelState, currentClient, Room);
        }

        /// <summary>
        /// Put a Room
        /// </summary>
        /// <param name="id">If of the RoomDTO to modify</param>
        /// <param name="Room">RoomDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a room
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// KEY=Room VALUE=FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST<br/><br/><para/><para/>
        /// KEY=RoomCategory VALUE=FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST<br/><br/><para/><para/>
        /// KEY=BillItemCategory VALUE=FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST<br/><br/><para/><para/>
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] RoomDTO Room)
        {
            RoomService.PrePutDTO(ModelState, currentClient, id, Room);
            return Ok();
        }

        /// <summary>
        /// Delete a Room
        /// </summary>
        /// <param name="id">Id of the room to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a room
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// KEY=Room VALUE=FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST<br/><br/><para/><para/>
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.VIP)]
        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            RoomService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}