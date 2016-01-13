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
    [RoutePrefix("api/Document/Category")]
    [Route("")]
    public class DocumentCategoryController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<DocumentCategory, DocumentCategoryDTO> DocumentCategoryService { get; set; }
        /// <summary>
        /// Post a documentCategory
        /// </summary>
        /// <param name="DocumentCategory">DocumentCategoryDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a documentCategory
        /// </remarks>
        /// <response code="200">DocumentCategoryDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public DocumentCategoryDTO Post([FromBody] DocumentCategoryDTO DocumentCategory)
        {
            return DocumentCategoryService.PrePostDTO(ModelState, currentClient, DocumentCategory);
        }

        /// <summary>
        /// Put a documentCategory
        /// </summary>
        /// <param name="DocumentCategory">DocumentCategoryDTO</param>
        /// <param name="id">id of the DocumentCategory to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a documentCategory
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] DocumentCategoryDTO DocumentCategory)
        {
            DocumentCategoryService.PrePutDTO(ModelState, currentClient, id, DocumentCategory);
            return Ok();
        }

        /// <summary>
        /// Delete a documentCategory
        /// </summary>
        /// <param name="id">id of the DocumentCategory to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a documentCategory
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
            DocumentCategoryService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}