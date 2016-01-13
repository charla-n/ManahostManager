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
    [RoutePrefix("api/Booking/Additional")]
    [Route("")]
    public class AdditionalBookingController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<AdditionalBooking, AdditionalBookingDTO> AdditionalBookingService { get; set; }
        /// <summary>
        /// Post an additionalBooking
        /// </summary>
        /// <param name="AdditionalBooking">AdditionalBookingDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post an additionalBooking
        /// <br/><para/>
        /// Allow the manager to add what he wants in the booking
        /// </remarks>
        /// <response code="200">AdditionalBookingDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public AdditionalBookingDTO Post([FromBody] AdditionalBookingDTO AdditionalBooking)
        {
            return AdditionalBookingService.PrePostDTO(ModelState, currentClient, AdditionalBooking);
        }

        /// <summary>
        /// Compute an additionalBooking
        /// </summary>
        /// <param name="id">id of the additionalBooking to compute</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Compute PriceTTC of the additionalBooking
        /// <br/><para/>
        /// Allow the manager to add what he wants in the booking
        /// </remarks>
        /// <response code="200">AdditionalBookingDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("Compute/{id:int}")]
        public AdditionalBookingDTO Compute([FromUri] int id)
        {
            var ServiceCast = AdditionalBookingService as AdditionalBookingService;
            ServiceCast.SetModelState(ModelState);
            return ServiceCast.Compute(currentClient, id, null);
        }

        /// <summary>
        /// Put an additionalBooking
        /// </summary>
        /// <param name="id">id of the additionalBooking to put</param>
        /// <param name="AdditionalBooking">AdditionalBookingDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put an additionalBooking
        /// <br/><para/>
        /// Allow the manager to add what he wants in the booking
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] AdditionalBookingDTO AdditionalBooking)
        {
            AdditionalBookingService.PrePutDTO(ModelState, currentClient, id, AdditionalBooking);
            return Ok();
        }

        /// <summary>
        /// Delete an additionalBooking
        /// </summary>
        /// <param name="id">id of the additionalBooking to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete an additionalBooking
        /// <br/><para/>
        /// Allow the manager to add what he wants in the booking
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
            AdditionalBookingService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}