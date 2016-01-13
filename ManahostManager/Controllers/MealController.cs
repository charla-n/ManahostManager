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
    [RoutePrefix("api/Meal")]
    [Route("")]
    public class MealController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<Meal, MealDTO> MealService { get; set; }
        /// <summary>
        /// Post a meal
        /// </summary>
        /// <param name="Meal">MealDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a Meal
        /// </remarks>
        /// <response code="200">MealDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public MealDTO Post([FromBody] MealDTO Meal)
        {
            return MealService.PrePostDTO(ModelState, currentClient, Meal);
        }

        /// <summary>
        /// Put a meal
        /// </summary>
        /// <param name="Meal">MealDTO</param>
        /// <param name="id">id of the Meal to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a Meal
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] MealDTO Meal)
        {
            MealService.PrePutDTO(ModelState, currentClient, id, Meal);
            return Ok();
        }

        /// <summary>
        /// Delete a meal
        /// </summary>
        /// <param name="id">id of the Meal to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a Meal
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
            MealService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}