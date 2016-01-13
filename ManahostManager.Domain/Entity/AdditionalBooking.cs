using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    // Allow the manager to add what he wants in the booking

    [Table("AdditionalBooking")]
    public class AdditionalBooking : IEntity
    {
        [Key]
        public int Id { get; set; }

        public String Title { get; set; }

        public Decimal PriceHT { get; set; }

        public Decimal? PriceTTC { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Booking Booking { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public BillItemCategory BillItemCategory { get; set; }

        public int HomeId { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        public DateTime? DateModification { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Tax Tax { get; set; }

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