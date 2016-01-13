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
    [RoutePrefix("api/Mail/Config")]
    [Route("")]
    public class MailConfigController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<MailConfig, MailConfigDTO> MaillConfigService { get; set; }

        /// <summary>
        /// Post a MailConfig
        /// </summary>
        /// <param name="MailConfig">MailConfigDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a MailConfig<br/><para/>
        /// The manager is able to send mail with Manahost<br/><para/>
        /// </remarks>
        /// <response code="200">MailConfigDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public MailConfigDTO Post([FromBody] MailConfigDTO MailConfig)
        {
            return MaillConfigService.PrePostDTO(ModelState, currentClient, MailConfig);
        }

        /// <summary>
        /// Put a MailConfig
        /// </summary>
        /// <param name="MailConfig">MailConfigDTO</param>
        /// <param name="id">id of the MailConfig to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a MailConfig<br/><para/>
        /// The manager is able to send mail with Manahost<br/><para/>
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] MailConfigDTO MailConfig)
        {
            MaillConfigService.PrePutDTO(ModelState, currentClient, id, MailConfig);
            return Ok();
        }

        /// <summary>
        /// Delete a MailConfig
        /// </summary>
        /// <param name="id">id of the MailConfig to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a MailConfig<br/><para/>
        /// The manager is able to send mail with Manahost<br/><para/>
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [Route("{id:int}")]
        public IHttpActionResult Delete([FromUri]int id)
        {
            MaillConfigService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}