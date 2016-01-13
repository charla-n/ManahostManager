using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Services;
using ManahostManager.Utils;
using ManahostManager.Utils.Attributs;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ManahostManager.Controllers
{
    [RoutePrefix("api/Bill/Item")]
    [Route("")]
    public class BillItemController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<BillItem, BillItemDTO> BillItemService { get; set; }

        /// <summary>
        /// Post a BillItem
        /// </summary>
        /// <param name="BillItem">the BillItem to Post</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a BillItem
        /// <br/><para/>
        /// Each Item of the bill (rooms, products, additionalbooking, gifts, ...)
        /// </remarks>
        /// <response code="200">BillItemDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPost]
        public IHttpActionResult Post([FromBody] BillItemDTO BillItem)
        {
            return Ok(BillItemService.PrePostDTO(ModelState, currentClient, BillItem));
        }

        /// <summary>
        /// Put a BillItem
        /// </summary>
        /// <param name="id">the id of the BillItem to Put</param>
        /// <param name="BillItem">the BillItem to Put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a BillItem
        /// <br/><para/>
        /// Each Item of the bill (rooms, products, additionalbooking, gifts, ...)
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] BillItemDTO BillItem)
        {
            BillItemService.PrePutDTO(ModelState, currentClient, id, BillItem);
            return Ok();
        }

        /// <summary>
        /// Delete a BillItem
        /// </summary>
        /// <param name="id">the id of the BillItem to Delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a BillItem
        /// <br/><para/>
        /// Each Item of the bill (rooms, products, additionalbooking, gifts, ...)
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
            BillItemService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}