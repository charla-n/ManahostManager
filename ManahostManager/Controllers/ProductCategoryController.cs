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
    [RoutePrefix("api/Product/Category")]
    [Route("")]
    public class ProductCategoryController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<ProductCategory, ProductCategoryDTO> ProductCategoryService { get; set; }

        /// <summary>
        /// Post a productcategory
        /// </summary>
        /// <param name="ProductCategory">ProductCategoryDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a productcategory
        /// </remarks>
        /// <response code="200">ProductCategoryDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public ProductCategoryDTO Post([FromBody] ProductCategoryDTO ProductCategory)
        {
            return ProductCategoryService.PrePostDTO(ModelState, currentClient, ProductCategory);
        }

        /// <summary>
        /// Put a productcategory
        /// </summary>
        /// <param name="ProductCategory">ProductCategoryDTO</param>
        /// <param name="id">Id of the productcategory to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a productcategory
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] ProductCategoryDTO ProductCategory)
        {
            ProductCategoryService.PrePutDTO(ModelState, currentClient, id, ProductCategory);
            return Ok();
        }

        /// <summary>
        /// Delete a productcategory
        /// </summary>
        /// <param name="id">Id of the productcategory to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a productcategory
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
            ProductCategoryService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}