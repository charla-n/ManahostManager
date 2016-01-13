using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    // Add a supplement to a booking room

    [Table("SupplementRoomBooking")]
    public class SupplementRoomBooking : IEntity
    {
        [Key]
        public int Id { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public RoomBooking RoomBooking { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public RoomSupplement RoomSupplement { get; set; }

        public Decimal? PriceHT { get; set; }

        public Decimal? PriceTTC { get; set; }

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