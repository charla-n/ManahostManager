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
    [RoutePrefix("api/Booking")]
    public class BookingController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<Booking, BookingDTO> BookingService { get; set; }
        /// <summary>
        /// Post a booking
        /// </summary>
        /// <param name="Booking">BookingDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a booking
        /// </remarks>
        /// <response code="200">BookingDTO</response>
        /// <response code="400">
        /// KEY=Period VALUE=DOES_NOT_MEET_REQUIREMENTS // Raised if a period does not cover all the booking (we can't compute price)<br/><br/><para/><para/>
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public BookingDTO Post([FromBody] BookingDTO Booking)
        {
            return BookingService.PrePostDTO(ModelState, currentClient, Booking);
        }

        /// <summary>
        /// Put a booking
        /// </summary>
        /// <param name="Booking">BookingDTO</param>
        /// <param name="id">id of the booking to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a booking
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// KEY=Period VALUE=DOES_NOT_MEET_REQUIREMENTS // Raised if a period does not cover all the booking (we can't compute price)<br/><br/><para/><para/>
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] BookingDTO Booking)
        {
            BookingService.PrePutDTO(ModelState, currentClient, id, Booking);
            return Ok();
        }

        /// <summary>
        /// Delete a booking
        /// </summary>
        /// <param name="id">id of the booking to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a booking
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
            BookingService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}