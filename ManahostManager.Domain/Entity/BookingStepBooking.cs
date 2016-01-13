using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    // Relationship one to one
    // A booking needs to have steps for going through each steps of the booking from the creation to the validation

    [Table("BookingStepBooking")]
    public class BookingStepBooking : IEntity
    {
        [Key]
        public int Id { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public BookingStepConfig BookingStepConfig { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public BookingStep CurrentStep { get; set; }

        public int MailSent { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public MailLog MailLog { get; set; }

        public int HomeId { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Booking Booking { get; set; }

        public DateTime? DateCurrentStepChanged { get; set; }

        public DateTime? DateModification { get; set; }

        public bool Canceled { get; set; }

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