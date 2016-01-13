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
    [RoutePrefix("api/Meal/Category")]
    [Route("")]
    public class MealCategoryController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<MealCategory, MealCategoryDTO> MealCategoryService { get; set; }

        /// <summary>
        /// Post a mealcategory
        /// </summary>
        /// <param name="MealCategory">MealCategoryDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a mealCategory
        /// </remarks>
        /// <response code="200">MealCategoryDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public MealCategoryDTO Post([FromBody] MealCategoryDTO MealCategory)
        {
            return MealCategoryService.PrePostDTO(ModelState, currentClient, MealCategory);
        }

        /// <summary>
        /// Put a mealCategory
        /// </summary>
        /// <param name="MealCategory">MealCategoryDTO</param>
        /// <param name="id">id of the MealCategory to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a mealCategory
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] MealCategoryDTO MealCategory)
        {
            MealCategoryService.PrePutDTO(ModelState, currentClient, id, MealCategory);
            return Ok();
        }

        /// <summary>
        /// Delete a mealCategory
        /// </summary>
        /// <param name="id">id of the MealCategory to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a mealCategory
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
            MealCategoryService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}