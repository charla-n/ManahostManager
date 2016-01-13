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
    [RoutePrefix("api/Booking/Step")]
    [Route("")]
    public class BookingStepController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<BookingStep, BookingStepDTO> BookingStepService { get; set; }
        /// <summary>
        /// Post a BookingStep
        /// </summary>
        /// <param name="BookingStep">BookingStepDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a BookingStep<br/><para/>
        /// Each step of a booking<br/><para/>
        /// WAITING / VALIDATED / ARCHIVED / ...
        /// </remarks>
        /// <response code="200">BookingStepDTO</response>
        /// <response code="400">
        /// KEY=BookingStepNext VALUE=INVALID_GIVEN_PARAMETER                 // raised if bookingstepnext configId is not the same as bookingstep configId<br/>
        /// KEY=BookingStepNext.BookingArchived VALUE=INVALID_GIVEN_PARAMETER // raised if bookingstepnext is not archived and bookingstep is archived<br/>
        /// KEY=BookingStepPrevious VALUE=INVALID_GIVEN_PARAMETER             // raised if bookingsteppreivous configId is not the same as bookingstep configId
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public BookingStepDTO Post([FromBody] BookingStepDTO BookingStep)
        {
            return BookingStepService.PrePostDTO(ModelState, currentClient, BookingStep);
        }

        /// <summary>
        /// Put a BookingStep
        /// </summary>
        /// <param name="BookingStep">BookingStepDTO</param>
        /// <param name="id">id of the BookingStep to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a BookingStep<br/><para/>
        /// Each step of a booking<br/><para/>
        /// WAITING / VALIDATED / ARCHIVED / ...
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// KEY=BookingStepNext VALUE=INVALID_GIVEN_PARAMETER                 // raised if bookingstepnext configId is not the same as bookingstep configId<br/>
        /// KEY=BookingStepNext.BookingArchived VALUE=INVALID_GIVEN_PARAMETER // raised if bookingstepnext is not archived and bookingstep is archived<br/>
        /// KEY=BookingStepPrevious VALUE=INVALID_GIVEN_PARAMETER             // raised if bookingsteppreivous configId is not the same as bookingstep configId
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] BookingStepDTO BookingStep)
        {
            BookingStepService.PrePutDTO(ModelState, currentClient, id, BookingStep);
            return Ok();
        }

        /// <summary>
        /// Delete a BookingStep
        /// </summary>
        /// <param name="id">id of the BookingStep to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a BookingStep<br/><para/>
        /// Each step of a booking<br/><para/>
        /// WAITING / VALIDATED / ARCHIVED / ...
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
            BookingStepService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}