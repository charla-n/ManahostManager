using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    // On each meal a price
    // It is also possible to set the price for a meal for each peoplecategory (children, adults, seniors) but it is optional

    [Table("MealPrice")]
    public class MealPrice : IEntity
    {
        [Key]
        public int Id { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Meal Meal { get; set; }

        public Decimal PriceHT { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Tax Tax { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public PeopleCategory PeopleCategory { get; set; }

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