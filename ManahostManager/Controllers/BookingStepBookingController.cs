using ManahostManager.App_Start;
using ManahostManager.Domain.DAL;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Model;
using ManahostManager.Services;
using ManahostManager.Utils;
using ManahostManager.Utils.Attributs;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System.Web.Http;

namespace ManahostManager.Controllers
{
    [RoutePrefix("api/Booking/StepBooking")]
    [Route("")]
    public class BookingStepBookingController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<BookingStepBooking, BookingStepBookingDTO> BookingStepBookingService { get; set; }
        /// <summary>
        /// Post a bookingstepbooking
        /// </summary>
        /// <param name="BookingStepBooking">BookingStepBookingDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a bookingstepbooking<br/><para/>
        /// A booking needs to have steps for going through each steps of the booking from the creation to the validation
        /// </remarks>
        /// <response code="200">BookingStepBookingDTO</response>
        /// <response code="400">
        /// KEY=BookingStepBooking VALUE=ALREADY_EXISTS    // raised if a bookingstepbooking is already associated with this booking<br/>
        /// KEY=CurrentStep VALUE=CANNOT_BE_NULL_OR_EMPTY  // raised if no current step are configured <br/>
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public BookingStepBookingDTO Post([FromBody] BookingStepBookingDTO BookingStepBooking)
        {
            return BookingStepBookingService.PrePostDTO(ModelState, currentClient, BookingStepBooking);
        }

        /// <summary>
        /// Put a bookingstepbooking
        /// </summary>
        /// <param name="id">id of the bookingstepbooking to put</param>
        /// <param name="BookingStepBooking">BookingStepBookingDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a bookingstepbooking<br/><para/>
        /// A booking needs to have steps for going through each steps of the booking from the creation to the validation
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] BookingStepBookingDTO BookingStepBooking)
        {
            BookingStepBookingService.PrePutDTO(ModelState, currentClient, id, BookingStepBooking);
            return Ok();
        }

        /// <summary>
        /// Delete a bookingstepbooking
        /// </summary>
        /// <param name="id">id of the bookingstepbooking to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a bookingstepbooking<br/><para/>
        /// A booking needs to have steps for going through each steps of the booking from the creation to the validation
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
            BookingStepBookingService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }

        /// <summary>
        /// Send an email
        /// </summary>
        /// <param name="mbm">MailBookingModel</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Send an email (/api/Booking/Mail)<br/><para/><br/><para/>
        /// For each step of the booking, the manager will be able to send an email<br/><para/>
        /// 2 send MAX per people<br/><para/>
        /// The manager needs to configure the templateMailId in bookingstep entity for being able to send an email and also the MailSubject<br/><para/>
        /// In document entity, you'll find a bookingstepid which is a foreign key on BookingStep. This FK will be used for any attachments the manager wants to send for each step.
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// KEY=Password VALUE=CANNOT_BE_NULL_OR_EMPTY     // raised if a manager wants to use its own account but didn't send a password<br/>
        /// KEY=Password VALUE=INVALID_GIVEN_PARAMETER     // raised if the password is not base64 encoded<br/>
        /// KEY=MailSent VALUE=DOES_NOT_MEET_REQUIREMENTS  // raised if MAX_MAIL(2) is reached<br/>
        /// KEY=CurrentStep VALUE=CANNOT_BE_NULL_OR_EMPTY  // raised if CurrentStep is null<br/>
        /// KEY=MailTemplate VALUE=CANNOT_BE_NULL_OR_EMPTY // raised if MailTemplate is null<br/>
        /// KEY=People VALUE=CANNOT_BE_NULL_OR_EMPTY       // raised if people is null
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [Route("~/api/Booking/Mail")]
        [HttpPost]
        public MailLog Post([FromBody] MailBookingModel mbm)
        {
            var ServiceCast = BookingStepBookingService as BookingStepBookingService;
            ServiceCast.SetModelState(ModelState);
            return ServiceCast.MailSteps(currentClient, mbm, null);
        }
    }
}