using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    // A BookingStepConfig has multiple step
    // A manager is able to create multiple step config

    [Table("BookingStepConfig")]
    public class BookingStepConfig : IEntity
    {
        public BookingStepConfig()
        {
            BookingSteps = new List<Entity.BookingStep>();
        }

        [Key]
        public int Id { get; set; }

        public String Title { get; set; }

        public int HomeId { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        public DateTime? DateModification { get; set; }

        public List<BookingStep> BookingSteps { get; set; }

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