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
    [RoutePrefix("api/Meal/Price")]
    [Route("")]
    public class MealPriceController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<MealPrice, MealPriceDTO> MealPriceService { get; set; }
        /// <summary>
        /// Post a MealPrice
        /// </summary>
        /// <param name="MealPrice">MealPriceDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a MealPrice
        /// </remarks>
        /// <response code="200">MealPriceDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public MealPriceDTO Post([FromBody] MealPriceDTO MealPrice)
        {
            return MealPriceService.PrePostDTO(ModelState, currentClient, MealPrice);
        }

        /// <summary>
        /// Put a MealPrice
        /// </summary>
        /// <param name="MealPrice">MealPriceDTO</param>
        /// <param name="id">id of the MealPrice to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a MealPrice
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] MealPriceDTO MealPrice)
        {
            MealPriceService.PrePutDTO(ModelState, currentClient, id, MealPrice);
            return Ok();
        }

        /// <summary>
        /// Delete a MealPrice
        /// </summary>
        /// <param name="id">id of the MealPrice to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a MealPrice
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
            MealPriceService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}