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
    [RoutePrefix("api/Period")]
    public class PeriodController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<Period, PeriodDTO> PeriodService { get; set; }
        /// <summary>
        /// Post a Period
        /// </summary>
        /// <param name="Period">PeriodDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a Period
        /// <br/><para/>
        /// Definition of a period (haute saison, basse saison, week-end, vacances, fermetures, ...)
        /// </remarks>
        /// <response code="200">PeriodDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPost]
        public PeriodDTO Post([FromBody] PeriodDTO Period)
        {
            return PeriodService.PrePostDTO(ModelState, currentClient, Period);
        }

        /// <summary>
        /// Put a Period
        /// </summary>
        /// <param name="Period">PeriodDTO</param>
        /// <param name="id">id of the Period to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a Period
        /// <br/><para/>
        /// Definition of a period (haute saison, basse saison, week-end, vacances, fermetures, ...)
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] PeriodDTO Period)
        {
            PeriodService.PrePutDTO(ModelState, currentClient, id, Period);
            return Ok();
        }

        /// <summary>
        /// Delete a Period
        /// </summary>
        /// <param name="id">id of the Period to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a Period
        /// <br/><para/>
        /// Definition of a period (haute saison, basse saison, week-end, vacances, fermetures, ...)
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            PeriodService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}