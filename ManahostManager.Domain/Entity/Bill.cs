using ManahostManager.Utils;
using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    [Table("Bill")]
    public class Bill : IEntity
    {
        public Bill()
        {
            BillItems = new List<Entity.BillItem>();
            PaymentMethods = new List<Entity.PaymentMethod>();
        }

        [Key]
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Document Document { get; set; }

        public int HomeId { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        public Boolean IsPayed { get; set; }

        [MaxLength(64, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Reference { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Supplier Supplier { get; set; }

        public Decimal TotalTTC { get; set; }

        public Decimal TotalHT { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Booking Booking { get; set; }

        public DateTime? DateModification { get; set; }

        public List<BillItem> BillItems { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; }

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