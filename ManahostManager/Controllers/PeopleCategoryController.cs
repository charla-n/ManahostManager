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
    [RoutePrefix("api/People/Category")]
    [Route("")]
    public class PeopleCategoryController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<PeopleCategory, PeopleCategoryDTO> PeopleCategoryService { get; set; }

        /// <summary>
        /// Post a PeopleCategory
        /// </summary>
        /// <param name="PeopleCategory">PeopleCategoryDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a PeopleCategory
        /// </remarks>
        /// <response code="200">PeopleCategoryDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPost]
        public PeopleCategoryDTO Post([FromBody] PeopleCategoryDTO PeopleCategory)
        {
            return PeopleCategoryService.PrePostDTO(ModelState, currentClient, PeopleCategory);
        }

        /// <summary>
        /// Put a PeopleCategory
        /// </summary>
        /// <param name="PeopleCategory">PeopleCategoryDTO</param>
        /// <param name="id">id of the PeopleCategory to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a PeopleCategory
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] PeopleCategoryDTO PeopleCategory)
        {
            PeopleCategoryService.PrePutDTO(ModelState, currentClient, id, PeopleCategory);
            return Ok();
        }

        /// <summary>
        /// Delete a PeopleCategory
        /// </summary>
        /// <param name="id">id of the PeopleCategory to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a PeopleCategory
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri]int id)
        {
            PeopleCategoryService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}