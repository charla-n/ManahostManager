using ManahostManager.App_Start;
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
    [RoutePrefix("api/PaymentType")]
    [Route("")]
    public class PaymentTypeController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<PaymentType, PaymentTypeDTO> PaymentTypeService { get; set; }

        /// <summary>
        /// Post a PaymentType
        /// </summary>
        /// <param name="PaymentType">PaymentTypeDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a PaymentType
        /// <br/><para/>
        /// The manager can configure payment method (credit card, cash, ...)
        /// </remarks>
        /// <response code="200">PaymentTypeDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPost]
        public IHttpActionResult Post([FromBody] PaymentTypeDTO PaymentType)
        {
            return Ok(PaymentTypeService.PrePostDTO(ModelState, currentClient, PaymentType));
        }

        /// <summary>
        /// Put a PaymentType
        /// </summary>
        /// <param name="PaymentType">PaymentTypeDTO</param>
        /// <param name="id">id of the PaymentType to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a PaymentType
        /// <br/><para/>
        /// The manager can configure payment method (credit card, cash, ...)
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] PaymentTypeDTO PaymentType)
        {
            PaymentTypeService.PrePutDTO(ModelState, currentClient, id, PaymentType);
            return Ok();
        }

        /// <summary>
        /// Delete a PaymentType
        /// </summary>
        /// <param name="id">id of the PaymentType to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a PaymentType
        /// <br/><para/>
        /// The manager can configure payment method (credit card, cash, ...)
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            PaymentTypeService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}