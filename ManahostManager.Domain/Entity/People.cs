using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    [Table("People")]
    public class People : IEntity
    {
        [Key]
        public int Id { get; set; }

        public Boolean AcceptMailing { get; set; }

        public String Addr { get; set; }

        public String City { get; set; }

        public String Civility { get; set; }

        public String Comment { get; set; }

        public String Country { get; set; }

        public DateTime? DateBirth { get; set; }

        public DateTime DateCreation { get; set; }

        public String Email { get; set; }

        public String Firstname { get; set; }

        public String Lastname { get; set; }

        public int? Mark { get; set; }

        public String Phone1 { get; set; }

        public String Phone2 { get; set; }

        public String State { get; set; }

        public String ZipCode { get; set; }

        public int HomeId { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        public Boolean Hide { get; set; }

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