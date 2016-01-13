using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    // TODO : EFTest on delete home check if all MailLogs/DocumentLog/... have been deleted

    [Table("MailLog")]
    public class MailLog : IEntity
    {
        [Key]
        public int Id { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        public bool Successful { get; set; }

        public string Reason { get; set; }

        public DateTime? DateModification { get; set; }

        public string To { get; set; }

        public DateTime? DateSended { get; set; }

        public int? HomeId { get; set; }

        public void SetDateModification(DateTime? d)
        {
            DateModification = d;
        }

        public int? GetFK()
        {
            return HomeId;
        }
    }
}