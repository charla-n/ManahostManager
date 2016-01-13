using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    // The manager is able to send mail with Manahost

    [Table("MailConfig")]
    public class MailConfig : IEntity
    {
        [Key]
        public int Id { get; set; }

        public String Email { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        public int HomeId { get; set; }

        public String Title { get; set; }

        public String Smtp { get; set; }

        public int SmtpPort { get; set; }

        public bool? IsSSL { get; set; }

        public DateTime? DateModification { get; set; }

        public int? GetFK()
        {
            return HomeId;
        }

        public void SetDateModification(DateTime? d)
        {
            DateModification = d;
        }
    }
}