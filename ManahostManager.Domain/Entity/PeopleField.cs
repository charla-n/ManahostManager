using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    // See explanation in FieldGroup Entity

    [Table("PeopleField")]
    public class PeopleField : IEntity
    {
        [Key]
        public int Id { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public FieldGroup FieldGroup { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public People People { get; set; }

        public String Key { get; set; }

        public String Value { get; set; }

        public int HomeId { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

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