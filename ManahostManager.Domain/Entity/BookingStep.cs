using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    // Each step of a booking
    // WAITING / VALIDATED / ARCHIVED / ...

    [Table("BookingStep")]
    public class BookingStep : IEntity
    {
        public BookingStep()
        {
            Documents = new List<Entity.Document>();
        }

        [Key]
        public int Id { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public BookingStepConfig BookingStepConfig { get; set; }

        public String Title { get; set; }

        public int? BookingStepIdNext { get; set; }

        public int? BookingStepIdPrevious { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public BookingStep BookingStepNext { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public BookingStep BookingStepPrevious { get; set; }

        public int HomeId { get; set; }

        public Boolean BookingValidated { get; set; }

        public Boolean BookingArchived { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Document MailTemplate { get; set; }

        public string MailSubject { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        public DateTime? DateModification { get; set; }

        public List<Document> Documents { get; set; }

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