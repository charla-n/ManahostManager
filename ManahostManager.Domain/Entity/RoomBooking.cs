using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    [Table("RoomBooking")]
    public class RoomBooking : IEntity
    {
        public RoomBooking()
        {
            PeopleBookings = new List<PeopleBooking>();
            SupplementRoomBookings = new List<SupplementRoomBooking>();
        }

        [Key]
        public int Id { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Booking Booking { get; set; }

        public Decimal? PriceHT { get; set; }

        public Decimal? PriceTTC { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Room Room { get; set; }

        public DateTime? DateBegin { get; set; }

        public DateTime? DateEnd { get; set; }

        public int NbNights { get; set; }

        public int HomeId { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        public DateTime? DateModification { get; set; }

        public List<PeopleBooking> PeopleBookings { get; set; }
        public List<SupplementRoomBooking> SupplementRoomBookings { get; set; }

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