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
    [RoutePrefix("api/Booking/Dinner")]
    [Route("")]
    public class DinnerBookingController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<DinnerBooking, DinnerBookingDTO> DinnerBookingService { get; set; }
        /// <summary>
        /// Post a dinnerBooking
        /// </summary>
        /// <param name="DinnerBooking">DinnerBookingDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a dinnerBooking
        /// </remarks>
        /// <response code="200">DinnerBookingDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public DinnerBookingDTO Post([FromBody] DinnerBookingDTO DinnerBooking)
        {
            return DinnerBookingService.PrePostDTO(ModelState, currentClient, DinnerBooking);
        }

        /// <summary>
        /// Compute a dinnerBooking
        /// </summary>
        /// <param name="id">id of the dinnerBooking to compute</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Compute a dinnerBooking<br/><para/>
        /// Compute PriceHT/PriceTTC
        /// </remarks>
        /// <response code="200">DinnerBookingDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("Compute/{id:int}")]
        public DinnerBookingDTO Compute([FromUri] int id)
        {
            var ServiceCast = DinnerBookingService as DinnerBookingService;
            ServiceCast.SetModelState(ModelState);
            return ServiceCast.Compute(currentClient, id, null);
        }

        /// <summary>
        /// Put a dinnerBooking
        /// </summary>
        /// <param name="id">id of the dinnerBooking to put</param>
        /// <param name="DinnerBooking">DinnerBookingDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a dinnerBooking
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] DinnerBookingDTO DinnerBooking)
        {
            DinnerBookingService.PrePutDTO(ModelState, currentClient, id, DinnerBooking);
            return Ok();
        }

        /// <summary>
        /// Delete a dinnerBooking
        /// </summary>
        /// <param name="id">id of the dinnerBooking to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a dinnerBooking
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
            DinnerBookingService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}