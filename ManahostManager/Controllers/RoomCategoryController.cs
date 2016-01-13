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
    [RoutePrefix("api/Room/Category")]
    [Route("")]
    public class RoomCategoryController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<RoomCategory, RoomCategoryDTO> RoomCategoryService { get; set; }

        /// <summary>
        /// Post a roomCategory
        /// </summary>
        /// <param name="RoomCategory">RoomCategoryDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a roomCategory
        /// </remarks>
        /// <response code="200">RoomCategoryDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.VIP)]
        public RoomCategoryDTO Post([FromBody] RoomCategoryDTO RoomCategory)
        {
            return RoomCategoryService.PrePostDTO(ModelState, currentClient, RoomCategory);
        }

        /// <summary>
        /// Put a roomCategory
        /// </summary>
        /// <param name="RoomCategory">RoomCategoryDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a roomCategory
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// KEY=RoomCategory VALUE=FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST<br/><br/><para/><para/>
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] RoomCategoryDTO RoomCategory)
        {
            RoomCategoryService.PrePutDTO(ModelState, currentClient, id, RoomCategory);
            return Ok();
        }

        /// <summary>
        /// Delete a roomCategory
        /// </summary>
        /// <param name="id">Id of the RoomCategory</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a roomCategory
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// KEY=RoomCategory VALUE=FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST<br/><br/><para/><para/>
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.VIP)]
        [Route("{id}")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            RoomCategoryService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}