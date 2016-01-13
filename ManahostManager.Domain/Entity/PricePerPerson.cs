using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    // For a specified period, for a specific room and a specific peoplecategory set a price

    [Table("PricePerPerson")]
    public class PricePerPerson : IEntity
    {
        [Key]
        public int Id { get; set; }

        public String Title { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public PeopleCategory PeopleCategory { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Period Period { get; set; }

        public Decimal PriceHT { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Room Room { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Tax Tax { get; set; }

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