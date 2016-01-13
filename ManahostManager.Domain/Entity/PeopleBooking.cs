using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    // How many and which type of people are in a room

    [Table("PeopleBooking")]
    public class PeopleBooking : IEntity
    {
        [Key]
        public int Id { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public RoomBooking RoomBooking { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public PeopleCategory PeopleCategory { get; set; }

        public int NumberOfPeople { get; set; }

        public DateTime? DateBegin { get; set; }

        public DateTime? DateEnd { get; set; }

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