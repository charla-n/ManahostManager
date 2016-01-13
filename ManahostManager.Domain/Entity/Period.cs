using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    // Definition of a period (haute saison, basse saison, week-end, vacances, fermetures, ...)

    [Table("Period")]
    public class Period : IEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }

        public int Days { get; set; }

        public String Description { get; set; }

        public int HomeId { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        public Boolean IsClosed { get; set; }

        public String Title { get; set; }

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