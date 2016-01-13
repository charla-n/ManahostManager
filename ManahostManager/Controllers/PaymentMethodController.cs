using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Services;
using ManahostManager.Utils;
using ManahostManager.Utils.Attributs;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ManahostManager.Controllers
{
    [RoutePrefix("api/Bill/Payment")]
    [Route("")]
    public class PaymentMethodController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<PaymentMethod, PaymentMethodDTO> PaymentMethodService { get; set; }

        /// <summary>
        /// Post a PaymentMethod
        /// </summary>
        /// <param name="PaymentMethod">the PaymentMethod to Post</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a PaymentMethod
        /// </remarks>
        /// <response code="200">PaymentMethodDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPost]
        public IHttpActionResult Post([FromBody] PaymentMethodDTO PaymentMethod)
        {
            return Ok(PaymentMethodService.PrePostDTO(ModelState, currentClient, PaymentMethod));
        }

        /// <summary>
        /// Put a PaymentMethod
        /// </summary>
        /// <param name="id">the id of the PaymentMethod to Put</param>
        /// <param name="PaymentMethod">the PaymentMethod to Put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a PaymentMethod
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] PaymentMethodDTO PaymentMethod)
        {
            PaymentMethodService.PrePutDTO(ModelState, currentClient, id, PaymentMethod);
            return Ok();
        }

        /// <summary>
        /// Delete a PaymentMethod
        /// </summary>
        /// <param name="id">the id of the PaymentMethod to Delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a PaymentMethod
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
            PaymentMethodService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}