using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    [Table("Meal")]
    public class Meal : IEntity
    {
        public Meal()
        {
            Documents = new List<Document>();
            MealPrices = new List<MealPrice>();
        }

        [Key]
        public int Id { get; set; }

        public String Description { get; set; }

        public String ShortDescription { get; set; }

        public int HomeId { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        public String Title { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public MealCategory MealCategory { get; set; }

        public Boolean? RefHide { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public BillItemCategory BillItemCategory { get; set; }

        public Boolean Hide { get; set; }

        public DateTime? DateModification { get; set; }

        public List<Document> Documents { get; set; }
        public List<MealPrice> MealPrices { get; set; }

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