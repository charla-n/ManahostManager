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
    [RoutePrefix("api/Satisfaction/ConfigQuestion")]
    [Route("")]
    public class SatisfactionConfigQuestionController : AbstractAPIController
    {
        [Dependency]
        public IAbstractService<SatisfactionConfigQuestion, SatisfactionConfigQuestionDTO> SatisfactionConfigQuestionService { get; set; }
        /// <summary>
        /// Post a SatisfactionConfigQuestion
        /// </summary>
        /// <param name="SatisfactionConfigQuestion">SatisfactionConfigQuestionDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a SatisfactionConfigQuestion
        /// </remarks>
        /// <response code="200">SatisfactionConfigQuestionDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [HttpPost]
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public SatisfactionConfigQuestionDTO Post([FromBody]SatisfactionConfigQuestionDTO SatisfactionConfigQuestion)
        {
            return SatisfactionConfigQuestionService.PrePostDTO(ModelState, currentClient, SatisfactionConfigQuestion);
        }

        /// <summary>
        /// Put a SatisfactionConfigQuestion
        /// </summary>
        /// <param name="SatisfactionConfigQuestion">SatisfactionConfigQuestionDTO</param>
        /// <param name="id">id of the SatisfactionConfigQuestion to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a SatisfactionConfigQuestion
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [HttpPut]
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] SatisfactionConfigQuestionDTO SatisfactionConfigQuestion)
        {
            SatisfactionConfigQuestionService.PrePutDTO(ModelState, currentClient, id, SatisfactionConfigQuestion);
            return Ok();
        }

        /// <summary>
        /// Delete a SatisfactionConfigQuestion
        /// </summary>
        /// <param name="id">id of the SatisfactionConfigQuestion to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a SatisfactionConfigQuestion
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [HttpDelete]
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [Route("{id:int}")]
        public IHttpActionResult Delete([FromUri] int id)
        {
            SatisfactionConfigQuestionService.PreDelete(ModelState, currentClient, id);
            return Ok();
        }
    }
}