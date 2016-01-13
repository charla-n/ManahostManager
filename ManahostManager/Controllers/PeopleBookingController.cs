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
    [RoutePrefix("api/Booking/People")]
    [Route("")]
    public class PeopleBookingController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<PeopleBooking, PeopleBookingDTO> PeopleBookingService { get; set; }
        /// <summary>
        /// Post a peopleBooking
        /// </summary>
        /// <param name="PeopleBooking">PeopleBookingDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a PeopleBooking
        /// <br/><para/>
        /// How many and which type of people are in a room
        /// </remarks>
        /// <response code="200">PeopleBookingDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public PeopleBookingDTO Post([FromBody] PeopleBookingDTO PeopleBooking)
        {
            return PeopleBookingService.PrePostDTO(ModelState, currentClient, PeopleBooking);
        }

        /// <summary>
        /// Put a peopleBooking
        /// </summary>
        /// <param name="PeopleBooking">PeopleBookingDTO</param>
        /// <param name="id">id of the PeopleBooking to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a PeopleBooking
        /// <br/><para/>
        /// How many and which type of people are in a room
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] PeopleBookingDTO PeopleBooking)
        {
            PeopleBookingService.PrePutDTO(ModelState, currentClient, id, PeopleBooking);
            return Ok();
        }

        /// <summary>
        /// Delete a peopleBooking
        /// </summary>
        /// <param name="id">id of the PeopleBooking to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a PeopleBooking
        /// <br/><para/>
        /// How many and which type of people are in a room
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
            PeopleBookingService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}