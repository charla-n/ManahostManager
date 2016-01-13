using ManahostManager.App_Start;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Services;
using ManahostManager.Utils;
using ManahostManager.Utils.Attributs;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace ManahostManager.Controllers
{
    [RoutePrefix("api/Home")]
    public class HomeController : AbstractAPIController
    {
        [Dependency]
        public HomeService HomeService { get; set; }

        /// <summary>
        /// Post a Home
        /// </summary>
        /// <param name="Home">HomeDTO</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Post a Home
        /// </remarks>
        /// <response code="200">HomeDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        public HomeDTO Post([FromBody] HomeDTO Home)
        {
            return HomeService.PrePostDTO(ModelState, currentClient, Home);
        }

        /// <summary>
        /// Put a Home
        /// </summary>
        /// <param name="Home">HomeDTO</param>
        /// <param name="id">id of the Home to put</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Put a Home
        /// </remarks>
        /// <response code="200"></response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody] HomeDTO Home)
        {
            HomeService.PrePutDTO(ModelState, currentClient, id, Home);
            return Ok();
        }

        /// <summary>
        /// Get all home
        /// </summary>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Get all home
        /// </remarks>
        /// <response code="200">List of HomeDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED)]
        public IEnumerable<HomeDTO> Get()
        {
            return HomeService.DoGet(currentClient, null);
        }

        /// <summary>
        /// Get home by id
        /// </summary>
        /// <param name="id">id of the Home to get</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Get home by id
        /// </remarks>
        /// <response code="200">HomeDTO</response>
        /// <response code="400">
        /// Take a look in ManahostManager.Domain.DTOs.* for more validation errors
        /// </response>
        /// <response code="500">An error occured we're sorry, a Manahost member has been contacted</response>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED)]
        [Route("{id:int}")]
        public HomeDTO Get([FromUri] int id)
        {
            return HomeService.DoGet(currentClient, id, null);
        }

        /// <summary>
        /// Delete a Home
        /// </summary>
        /// <param name="id">id of the Home to delete</param>
        /// <remarks>
        /// Permission VIP<br/><br/><para/><para/>
        /// Delete a Home
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
            HomeService.SetModelState(this.ModelState);
            HomeService.PreDeleteManual(currentClient, id, null);
            return Ok();
        }


        /// <summary>
        /// Get Default Home
        /// </summary>
        /// <returns></returns>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [Route("Default")]
        public HomeDTO GetDefaultHome()
        {
            HomeService.SetModelState(ModelState);
            return HomeService.DoGetDefaultHome(currentClient);
        }

        /// <summary>
        /// Change Default Home
        /// </summary>
        /// <param name="id">Id of Home</param>
        /// <returns></returns>
        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [Route("Default/{id:int}")]
        [HttpPut]
        public async Task<IHttpActionResult> ChangeDefaultHome([FromUri] int id)
        {
            HomeService.SetModelState(ModelState);
            await HomeService.ChangeDefaultHomeId(currentClient, id);
            return Ok();
        }
    }
}