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
    [RoutePrefix("api/Product")]
    public class ProductController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<Product, ProductDTO> ProductService { get; set; }
        /// <summary>
        /// Post a product
        /// </summary>
        /// <param name="Product">ProductDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a product
        /// </remarks>
        /// <response code="200">ProductDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public ProductDTO Post([FromBody]ProductDTO Product)
        {
            return ProductService.PrePostDTO(ModelState, currentClient, Product);
        }

        /// <summary>
        /// Put a product
        /// </summary>
        /// <param name="Product">ProductDTO</param>
        /// <param name="id">Id of the product to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a product
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] ProductDTO Product)
        {
            ProductService.PrePutDTO(ModelState, currentClient, id, Product);
            return Ok();
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id">Id of the product to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a product
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
            ProductService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}