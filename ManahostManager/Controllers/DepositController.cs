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
    [RoutePrefix("api/Booking/Deposit")]
    [Route("")]
    public class DepositController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<Deposit, DepositDTO> DepositService { get; set; }
        /// <summary>
        /// Post a deposit
        /// </summary>
        /// <param name="Deposit">DepositDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a deposit
        /// </remarks>
        /// <response code="200">DepositDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public DepositDTO Post([FromBody] DepositDTO Deposit)
        {
            return DepositService.PrePostDTO(ModelState, currentClient, Deposit);
        }

        /// <summary>
        /// Put a deposit
        /// </summary>
        /// <param name="Deposit">DepositDTO</param>
        /// <param name="id">id of the deposit to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a deposit
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] DepositDTO Deposit)
        {
            DepositService.PrePutDTO(ModelState, currentClient, id, Deposit);
            return Ok();
        }

        /// <summary>
        /// Delete a deposit
        /// </summary>
        /// <param name="id">id of the deposit to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a deposit
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
            DepositService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}