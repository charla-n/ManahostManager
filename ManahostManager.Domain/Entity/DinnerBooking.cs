using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    [Table("DinnerBooking")]
    public class DinnerBooking : IEntity
    {
        public DinnerBooking()
        {
            MealBookings = new List<Entity.MealBooking>();
        }

        [Key]
        public int Id { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Booking Booking { get; set; }

        public DateTime Date { get; set; }

        public int? NumberOfPeople { get; set; }

        public Decimal? PriceHT { get; set; }

        public Decimal? PriceTTC { get; set; }

        public int HomeId { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        public DateTime? DateModification { get; set; }

        public List<MealBooking> MealBookings { get; set; }

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