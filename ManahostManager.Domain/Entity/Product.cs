using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    [Table("Product")]
    public class Product : IEntity
    {
        public Product()
        {
            Documents = new List<Document>();
        }

        [Key]
        public int Id { get; set; }

        public int HomeId { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        public String Title { get; set; }

        public String ShortDescription { get; set; }

        public Decimal PriceHT { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public ProductCategory ProductCategory { get; set; }

        public Boolean? RefHide { get; set; }

        public int? Stock { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Supplier Supplier { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Tax Tax { get; set; }

        public int? Threshold { get; set; }

        public Int64? Duration { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public BillItemCategory BillItemCategory { get; set; }

        public Boolean Hide { get; set; }

        public DateTime? DateModification { get; set; }

        public bool IsUnderThreshold { get; set; }

        public List<Document> Documents { get; set; }

        public bool IsService { get; set; }

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