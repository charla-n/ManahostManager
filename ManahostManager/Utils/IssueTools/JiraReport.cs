using ManahostManager.LogTools;
using ManahostManager.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ManahostManager.Utils.IssueTools
{
    internal class ProjectJira
    {
        public string key { get; set; }
    }

    internal class IssueTypeJira
    {
        public string name { get; set; }
    }

    internal class FieldsJira
    {
        public FieldsJira()
        {
            project = new ProjectJira();
            issuetype = new IssueTypeJira();
        }

        public ProjectJira project { get; set; }

        public IssueTypeJira issuetype { get; set; }

        public string summary { get; set; }

        public string description { get; set; }
    }

    internal class IssueJiraModel
    {
        public IssueJiraModel()
        {
            fields = new FieldsJira();
        }

        public FieldsJira fields { get; set; }
    }

    internal class JiraValue
    {
        public long id { get; set; }
    }

    public class JiraReport : IReport
    {
        private string URL_API { get; set; }

        private string PROJECT_KEY { get; set; }

        private string USER_API { get; set; }

        private string PASSWD_API { get; set; }

        public JiraReport(string url, string project, string user, string pwd)
        {
            URL_API = url;
            PROJECT_KEY = project;
            USER_API = user;
            PASSWD_API = pwd;
        }

        public async Task<long?> PostIssue(IssueModel issue)
        {
            IssueJiraModel JiraReport = new IssueJiraModel();
            JiraReport.fields.description = issue.Description;
            JiraReport.fields.summary = issue.Summary;
            JiraReport.fields.project.key = PROJECT_KEY;
            switch (issue.Type)
            {
                case IssueType.BUG:
                    JiraReport.fields.issuetype.name = "Bug";
                    break;

                case IssueType.IMPROVEMENT:
                    JiraReport.fields.issuetype.name = "Improvement";
                    break;
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL_API);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(string.Format("{0}:{1}", USER_API, PASSWD_API))));
                var response = await client.PostAsJsonAsync("/rest/api/2/issue/", JiraReport);
                if (response.IsSuccessStatusCode)
                {
                    if (Log.InfoLogger.IsInfoEnabled)
                        Log.InfoLogger.Info("Issue has been created in Jira");
                    JiraValue res = JsonConvert.DeserializeObject<JiraValue>(await response.Content.ReadAsStringAsync());
                    return (res.id);
                }
                else
                {
                    if (Log.WarnLogger.IsWarnEnabled)
                    {
                        Log.WarnLogger.WarnFormat("Tryed to create an jira issue but failed, STATUS CODE : {0}, MSG : {1}", response.StatusCode, await response.Content.ReadAsStringAsync());
                    }
                    return null;
                }
            }
        }
    }
}