using ManahostManager.Model;
using ManahostManager.Utils.IssueTools;
using System.Threading.Tasks;
using System.Web.Http;

namespace ManahostManager.Controllers
{
    public class IssueController : ApiController
    {
        private static readonly IReport report = new JiraReport("https://jira.manahost.fr/", "MF", "Reportuser", "PASSWORD");

        public IssueController()
        {
        }

        public async Task<IHttpActionResult> Post(IssueModel issue)
        {
            if (issue == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            long? idIssue = await report.PostIssue(issue);
            if (idIssue == null)
                return BadRequest();
            return Ok(new
            {
                Id = idIssue
            });
        }
    }
}