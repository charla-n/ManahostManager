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
    [RoutePrefix("api/KeyGenerator")]
    public class KeyGeneratorController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<KeyGenerator, KeyGeneratorDTO> KeyGeneratorService { get; set; }

        /// <summary>
        /// Post a keygenerator
        /// </summary>
        /// <param name="KeyGenerator">KeyGeneratorDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a keygenerator<br/><para/>
        /// It will be used by managers for discount<br/><para/>
        /// It will be used by administrator for creating beta/trial keys<br/><para/>
        /// It will be used by administrator for discount on the price of Manahost
        /// </remarks>
        /// <response code="200">KeyGeneratorDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPost]
        public KeyGeneratorDTO Post([FromBody] KeyGeneratorDTO KeyGenerator)
        {
            return KeyGeneratorService.PrePostDTO(ModelState, currentClient, KeyGenerator);
        }

        /// <summary>
        /// Put a keygenerator
        /// </summary>
        /// <param name="KeyGenerator">KeyGeneratorDTO</param>
        /// <param name="id">id of the KeyGenerator to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a keygenerator<br/><para/>
        /// It will be used by managers for discount<br/><para/>
        /// It will be used by administrator for creating beta/trial keys<br/><para/>
        /// It will be used by administrator for discount on the price of Manahost
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] KeyGeneratorDTO KeyGenerator)
        {
            KeyGeneratorService.PrePutDTO(ModelState, currentClient, id, KeyGenerator);
            return Ok();
        }

        /// <summary>
        /// Delete a keygenerator
        /// </summary>
        /// <param name="id">id of the KeyGenerator to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a keygenerator<br/><para/>
        /// It will be used by managers for discount<br/><para/>
        /// It will be used by administrator for creating beta/trial keys<br/><para/>
        /// It will be used by administrator for discount on the price of Manahost
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
            KeyGeneratorService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}