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
    [RoutePrefix("api/Booking/StepConfig")]
    [Route("")]
    public class BookingStepConfigController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<BookingStepConfig, BookingStepConfigDTO> BookingStepConfigService { get; set; }
        /// <summary>
        /// Post a bookingstepconfig
        /// </summary>
        /// <param name="BookingStepConfig">BookingStepConfigDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a bookingstepbooking<br/><para/>
        /// A BookingStepConfig has multiple step<br/><para/>
        /// A manager is able to create multiple step config
        /// </remarks>
        /// <response code="200">BookingStepConfigDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public BookingStepConfigDTO Post([FromBody] BookingStepConfigDTO BookingStepConfig)
        {
            return BookingStepConfigService.PrePostDTO(ModelState, currentClient, BookingStepConfig);
        }

        /// <summary>
        /// Put a bookingstepconfig
        /// </summary>
        /// <param name="BookingStepConfig">BookingStepConfigDTO</param>
        /// <param name="id">id of the bookingstepconfig to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a bookingstepbooking<br/><para/>
        /// A BookingStepConfig has multiple step<br/><para/>
        /// A manager is able to create multiple step config
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] BookingStepConfigDTO BookingStepConfig)
        {
            BookingStepConfigService.PrePutDTO(ModelState, currentClient, id, BookingStepConfig);
            return Ok();
        }

        /// <summary>
        /// Delete a bookingstepconfig
        /// </summary>
        /// <param name="id">id of the bookingstepconfig to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a bookingstepbooking<br/><para/>
        /// A BookingStepConfig has multiple step<br/><para/>
        /// A manager is able to create multiple step config
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
            BookingStepConfigService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}