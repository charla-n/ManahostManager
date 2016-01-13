using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    // A bill is composed by multiple items

    [Table("BillItem")]
    public class BillItem : IEntity
    {
        [Key]
        public int Id { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Bill Bill { get; set; }

        public String Description { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public BillItemCategory BillItemCategory { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public GroupBillItem GroupBillItem { get; set; }

        public String Title { get; set; }

        public Decimal PriceHT { get; set; }

        public int Quantity { get; set; }

        public Decimal PriceTTC { get; set; }

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