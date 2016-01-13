using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManahostManager.Domain.Entity
{
    [Table("ResourceConfig")]
    public class ResourceConfig
    {
        [Key]
        public int Id { get; set; }

        public long LimitBase { get; set; }
    }
}