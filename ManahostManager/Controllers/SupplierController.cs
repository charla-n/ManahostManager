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
    [RoutePrefix("api/Product/Supplier")]
    [Route("")]
    public class SupplierController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<Supplier, SupplierDTO> SupplierService { get; set; }

        /// <summary>
        /// Post a supplier
        /// </summary>
        /// <param name="Supplier">SupplierDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a supplier
        /// </remarks>
        /// <response code="200">SupplierDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public IHttpActionResult Post([FromBody] SupplierDTO Supplier)
        {
            return Ok(SupplierService.PrePostDTO(ModelState, currentClient, Supplier));
        }

        /// <summary>
        /// Put a supplier
        /// </summary>
        /// <param name="Supplier">SupplierDTO</param>
        /// <param name="id">id of the supplier to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a supplier
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] SupplierDTO Supplier)
        {
            SupplierService.PrePutDTO(ModelState, currentClient, id, Supplier);
            return Ok();
        }

        /// <summary>
        /// Delete a supplier
        /// </summary>
        /// <param name="id">id of the supplier to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a supplier
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
            SupplierService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}