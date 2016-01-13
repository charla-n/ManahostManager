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
    [RoutePrefix("api/Booking/Meal")]
    [Route("")]
    public class MealBookingController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<MealBooking, MealBookingDTO> MealBookingService { get; set; }
        /// <summary>
        /// Post a mealBooking
        /// </summary>
        /// <param name="MealBooking">MealBookingDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a mealBooking
        /// </remarks>
        /// <response code="200">MealBookingDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public MealBookingDTO Post([FromBody] MealBookingDTO MealBooking)
        {
            return MealBookingService.PrePostDTO(ModelState, currentClient, MealBooking);
        }

        /// <summary>
        /// Put a mealBooking
        /// </summary>
        /// <param name="MealBooking">MealBookingDTO</param>
        /// <param name="id">id of the MealBooking to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a mealBooking
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] MealBookingDTO MealBooking)
        {
            MealBookingService.PrePutDTO(ModelState, currentClient, id, MealBooking);
            return Ok();
        }

        /// <summary>
        /// Delete a mealBooking
        /// </summary>
        /// <param name="id">id of the MealBooking to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a mealBooking
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
            MealBookingService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}