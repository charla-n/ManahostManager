using ManahostManager.Utils;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ManahostManager.Controllers
{
    public class IpController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var ip = Request.GetOwinContext().Request.RemoteIpAddress;
            if (ip == null)
                throw new Exception("Unable to get IP");
            var ipinfoDB = await IpInfoDB.GetInfoIP(ip);
            return Ok(ipinfoDB);
        }
    }
}