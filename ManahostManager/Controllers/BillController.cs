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
    [RoutePrefix("api/Bill")]
    [Route("")]
    public class BillController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<Bill, BillDTO> BillService { get; set; }

        /// <summary>
        /// Post a Bill
        /// </summary>
        /// <param name="Bill">the Bill to Post</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a Bill
        /// </remarks>
        /// <response code="200">BillDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPost]
        public IHttpActionResult Post([FromBody] BillDTO Bill)
        {
            return Ok(BillService.PrePostDTO(ModelState, currentClient, Bill));
        }

        /// <summary>
        /// Put a Bill
        /// </summary>
        /// <param name="id">the id of the Bill to Put</param>
        /// <param name="Bill">the Bill to Put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a Bill
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] BillDTO Bill)
        {
            BillService.PrePutDTO(ModelState, currentClient, id, Bill);
            return Ok();
        }

        /// <summary>
        /// Delete a Bill
        /// </summary>
        /// <param name="id">the id of the Bill to Delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a Bill
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
            BillService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}