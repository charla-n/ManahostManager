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
    [RoutePrefix("api/Booking/Product")]
    [Route("")]
    public class ProductBookingController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<ProductBooking, ProductBookingDTO> ProductBookingService { get; set; }
        /// <summary>
        /// Post a ProductBooking
        /// </summary>
        /// <param name="ProductBooking">ProductBookingDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a ProductBooking
        /// </remarks>
        /// <response code="200">ProductBookingDTO</response>
        /// <response code="400">
        /// KEY=Date VALUE=DOES_NOT_MEET_REQUIREMENTS // in case of Date is outside the interval of the booking (DateArrival/DateDeparture)<br/><para/>
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public ProductBookingDTO Post([FromBody] ProductBookingDTO ProductBooking)
        {
            return ProductBookingService.PrePostDTO(ModelState, currentClient, ProductBooking);
        }

        /// <summary>
        /// Compute a ProductBooking
        /// </summary>
        /// <param name="id">id of the ProductBooking to compute</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Compute a ProductBooking<br/><para/>
        /// Compute PriceHT/PriceTTC/Duration
        /// </remarks>
        /// <response code="200">ProductBookingDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("Compute/{id:int}")]
        public ProductBookingDTO Compute([FromUri] int id)
        {
            var ServiceCast = ProductBookingService as ProductBookingService;
            ServiceCast.SetModelState(ModelState);
            return ServiceCast.Compute(currentClient, id, null);
        }

        /// <summary>
        /// Put a ProductBooking
        /// </summary>
        /// <param name="ProductBooking">ProductBookingDTO</param>
        /// <param name="id">id of the ProductBooking to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a ProductBooking
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// KEY=Date VALUE=DOES_NOT_MEET_REQUIREMENTS // in case of Date is outside the interval of the booking (DateArrival/DateDeparture)<br/><para/>
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] ProductBookingDTO ProductBooking)
        {
            ProductBookingService.PrePutDTO(ModelState, currentClient, id, ProductBooking);
            return Ok();
        }

        /// <summary>
        /// Delete a ProductBooking
        /// </summary>
        /// <param name="id">id of the ProductBooking to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a ProductBooking
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
            ProductBookingService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}