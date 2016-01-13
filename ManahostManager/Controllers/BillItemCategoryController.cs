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
    [RoutePrefix("api/Bill/Category")]
    [Route("")]
    public class BillItemCategoryController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<BillItemCategory, BillItemCategoryDTO> BillItemCategoryService { get; set; }

        /// <summary>
        /// Post a BillItemCategory
        /// </summary>
        /// <param name="BillItemCategory">the BillItemCategory to Post</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a BillItemCategory
        /// </remarks>
        /// <response code="200">BillItemCategoryDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPost]
        public IHttpActionResult Post([FromBody] BillItemCategoryDTO BillItemCategory)
        {
            return Ok(BillItemCategoryService.PrePostDTO(ModelState, currentClient, BillItemCategory));
        }

        /// <summary>
        /// Put a BillItemCategory
        /// </summary>
        /// <param name="id">the id of the BillItemCategory to Put</param>
        /// <param name="BillItemCategory">the BillItemCategory to Put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a BillItemCategory
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] BillItemCategoryDTO BillItemCategory)
        {
            BillItemCategoryService.PrePutDTO(ModelState, currentClient, id, BillItemCategory);
            return Ok();
        }

        /// <summary>
        /// Delete a BillItemCategory
        /// </summary>
        /// <param name="id">the id of the BillItemCategory to Delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a BillItemCategory
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
            BillItemCategoryService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}