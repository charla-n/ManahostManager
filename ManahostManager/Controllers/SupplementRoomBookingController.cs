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
    [RoutePrefix("api/Booking/RoomSupplement")]
    [Route("")]
    public class SupplementRoomBookingController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<SupplementRoomBooking, SupplementRoomBookingDTO> SupplementRoomBookingService { get; set; }

        /// <summary>
        /// Post a SupplementRoomBooking
        /// </summary>
        /// <param name="SupplementRoomBooking">SupplementRoomBookingDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a SupplementRoomBooking
        /// </remarks>
        /// <response code="200">SupplementRoomBookingDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public SupplementRoomBookingDTO Post([FromBody] SupplementRoomBookingDTO SupplementRoomBooking)
        {
            return SupplementRoomBookingService.PrePostDTO(ModelState, currentClient, SupplementRoomBooking);
        }

        /// <summary>
        /// Compute a SupplementRoomBooking
        /// </summary>
        /// <param name="id">id of the SupplementRoomBooking to compute</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Compute a SupplementRoomBooking
        /// </remarks>
        /// <response code="200">SupplementRoomBookingDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("Compute/{id:int}")]
        public SupplementRoomBookingDTO Compute([FromUri] int id)
        {
            var ServiceCast = SupplementRoomBookingService as SupplementRoomBookingService;
            ServiceCast.SetModelState(ModelState);
            return ServiceCast.Compute(currentClient, id, null);
        }

        /// <summary>
        /// Put a SupplementRoomBooking
        /// </summary>
        /// <param name="SupplementRoomBooking">SupplementRoomBookingDTO</param>
        /// <param name="id">id of the SupplementRoomBooking to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a SupplementRoomBooking
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] SupplementRoomBookingDTO SupplementRoomBooking)
        {
            SupplementRoomBookingService.PrePutDTO(ModelState, currentClient, id, SupplementRoomBooking);
            return Ok();
        }

        /// <summary>
        /// Delete a SupplementRoomBooking
        /// </summary>
        /// <param name="id">id of the SupplementRoomBooking to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a SupplementRoomBooking
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
            SupplementRoomBookingService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}