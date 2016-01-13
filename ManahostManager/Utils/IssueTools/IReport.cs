using ManahostManager.Model;
using System.Threading.Tasks;

namespace ManahostManager.Utils.IssueTools
{
    public interface IReport
    {
        //NULL if error else Id of new Issue
        Task<long?> PostIssue(IssueModel issue);
    }
}