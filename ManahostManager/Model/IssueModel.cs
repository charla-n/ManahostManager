using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Model
{
    public enum IssueType
    {
        BUG,
        IMPROVEMENT
    };

    public class IssueModel
    {
        [Required]
        public IssueType Type { get; set; }

        [Required]
        public string Summary { get; set; }

        public string Description { get; set; }
    }
}