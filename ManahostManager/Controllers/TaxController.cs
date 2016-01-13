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
    [RoutePrefix("api/Tax")]
    public class TaxController : AbstractAPIController
    {

        [Dependency]
        public IAbstractService<Tax, TaxDTO> TaxService { get; set; }

        /// <summary>
        /// Post a tax
        /// </summary>
        /// <param name="Tax">TaxDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a tax
        /// </remarks>
        /// <response code="200">TaxDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.VIP)]
        public TaxDTO Post([FromBody] TaxDTO Tax)
        {
            return TaxService.PrePostDTO(ModelState, currentClient, Tax);
        }

        /// <summary>
        /// Put a tax
        /// </summary>
        /// <param name="id">If of the TaxDTO</param>
        /// <param name="Tax">TaxDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a tax
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// KEY=Tax VALUE=FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST<br/><br/><para/><para/>
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] TaxDTO Tax)
        {
            TaxService.PrePutDTO(ModelState, currentClient, id, Tax);
            return Ok();
        }

        /// <summary>
        /// Delete a tax
        /// </summary>
        /// <param name="id">Id of the tax to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a tax
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// KEY=Tax VALUE=FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST<br/><br/><para/><para/>
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.VIP)]
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            TaxService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}