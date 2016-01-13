using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    // The bill can be split in multiple payment method
    // For example you can pay 50% of the price by cash and the other 50% by credit card

    [Table("PaymentMethod")]
    public class PaymentMethod : IEntity
    {
        [Key]
        public int Id { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Bill Bill { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public PaymentType PaymentType { get; set; }

        public Decimal Price { get; set; }

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