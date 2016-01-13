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
    [RoutePrefix("api/Bill/Item/Group")]
    [Route("")]
    public class GroupBillItemController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<GroupBillItem, GroupBillItemDTO> GroupBillItemService { get; set; }

        /// <summary>
        /// Post a GroupBillItem
        /// </summary>
        /// <param name="GroupBillItem">the GroupBillItem to Post</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a GroupBillItem
        /// </remarks>
        /// <response code="200">GroupBillItemDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPost]
        public IHttpActionResult Post([FromBody] GroupBillItemDTO GroupBillItem)
        {
            return Ok(GroupBillItemService.PrePostDTO(ModelState, currentClient, GroupBillItem));
        }

        /// <summary>
        /// Put a GroupBillItem
        /// </summary>
        /// <param name="id">the id of the GroupBillItem to Put</param>
        /// <param name="GroupBillItem">the GroupBillItem to Put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a GroupBillItem
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] GroupBillItemDTO GroupBillItem)
        {
            GroupBillItemService.PrePutDTO(ModelState, currentClient, id, GroupBillItem);
            return Ok();
        }

        /// <summary>
        /// Delete a GroupBillItem
        /// </summary>
        /// <param name="id">the id of the GroupBillItem to Delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a GroupBillItem
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
            GroupBillItemService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}