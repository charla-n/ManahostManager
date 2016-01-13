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
    [RoutePrefix("api/Home/Config")]
    [Route("")]
    public class HomeConfigController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<HomeConfig, HomeConfigDTO> HomeConfigService { get; set; }
        /// <summary>
        /// Put an homeconfig
        /// </summary>
        /// <param name="HomeConfig">HomeConfigDTO</param>
        /// <param name="id">id of the HomeConfig to Put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put an HomeConfig
        /// </remarks>
        /// <response code="200">HomeConfigDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public IHttpActionResult Put([FromUri] int id, [FromBody] HomeConfigDTO HomeConfig)
        {
            HomeConfigService.PrePutDTO(ModelState, currentClient, id, HomeConfig);
            return Ok();
        }
    }
}