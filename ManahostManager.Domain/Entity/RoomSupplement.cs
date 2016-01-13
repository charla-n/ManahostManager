using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    // Any supplement that can be added in a room

    [Table("RoomSupplement")]
    public class RoomSupplement : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int HomeId { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        public String Title { get; set; }

        public Decimal PriceHT { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Tax Tax { get; set; }

        public Boolean Hide { get; set; }

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