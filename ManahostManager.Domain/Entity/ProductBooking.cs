using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    [Table("ProductBooking")]
    public class ProductBooking : IEntity
    {
        [Key]
        public int Id { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Booking Booking { get; set; }

        public DateTime? Date { get; set; }

        public Int64? Duration { get; set; }

        public Decimal? PriceHT { get; set; }

        public Decimal? PriceTTC { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Product Product { get; set; }

        public int Quantity { get; set; }

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

        [NotMapped]
        [IgnoreDataMember]
        [MessagePackIgnore]
        public TimeSpan TimeSpanValue
        {
            get { return TimeSpan.FromSeconds((double)Duration); }
        }
    }
}