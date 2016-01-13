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
    [RoutePrefix("api/Booking/Room")]
    [Route("")]
    public class RoomBookingController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<RoomBooking, RoomBookingDTO> RoomBookingService { get; set; }

        /// <summary>
        /// Post a RoomBooking
        /// </summary>
        /// <param name="RoomBooking">RoomBookingDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a RoomBooking
        /// </remarks>
        /// <response code="200">RoomBookingDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public RoomBookingDTO Post([FromBody] RoomBookingDTO RoomBooking)
        {
            return RoomBookingService.PrePostDTO(ModelState, currentClient, RoomBooking);
        }

        /// <summary>
        /// Compute a RoomBooking
        /// </summary>
        /// <param name="id">id of the RoomBooking to compute</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Compute a RoomBooking
        /// </remarks>
        /// <response code="200">RoomBookingDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("Compute/{id:int}")]
        public RoomBookingDTO Compute([FromUri] int id)
        {
            var ServiceCast = RoomBookingService as RoomBookingService;
            ServiceCast.SetModelState(ModelState);
            return ServiceCast.Compute(currentClient, id, null);
        }

        /// <summary>
        /// Put a RoomBooking
        /// </summary>
        /// <param name="RoomBooking">RoomBookingDTO</param>
        /// <param name="id">id of the RoomBooking to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a RoomBooking
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] RoomBookingDTO RoomBooking)
        {
            RoomBookingService.PrePutDTO(ModelState, currentClient, id, RoomBooking);
            return Ok();
        }

        /// <summary>
        /// Delete a RoomBooking
        /// </summary>
        /// <param name="id">id of the RoomBooking to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a RoomBooking
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
            RoomBookingService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}