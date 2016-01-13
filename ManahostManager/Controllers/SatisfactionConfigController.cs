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
    [RoutePrefix("api/Satisfaction/Config")]
    [Route("")]
    public class SatisfactionConfigController : AbstractAPIController
    {

        [Dependency]
        public IAbstractService<SatisfactionConfig, SatisfactionConfigDTO> SatisfactionConfigService { get; set; }

        /// <summary>
        /// Put a SatisfactionConfig
        /// </summary>
        /// <param name="SatisfactionConfig">SatisfactionConfigDTO</param>
        /// <param name="id">id of the SatisfactionConfig to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a SatisfactionConfig
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [HttpPut]
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] SatisfactionConfigDTO SatisfactionConfig)
        {
            SatisfactionConfigService.PrePutDTO(ModelState, currentClient, id, SatisfactionConfig);
            return Ok();
        }
    }
}