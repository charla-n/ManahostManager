using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    //

    [Table("FieldGroup")]
    public class FieldGroup : IEntity
    {
        public FieldGroup()
        {
            PeopleFields = new List<Entity.PeopleField>();
        }

        [Key]
        public int Id { get; set; }

        public int HomeId { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        public String Title { get; set; }

        public DateTime? DateModification { get; set; }

        public List<PeopleField> PeopleFields { get; set; }

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