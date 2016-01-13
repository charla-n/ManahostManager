using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    [Table("Room")]
    public class Room : IEntity
    {
        public Room()
        {
            Beds = new List<Bed>();
            Documents = new List<Document>();
        }

        [Key]
        public int Id { get; set; }

        public Decimal? Caution { get; set; }

        public String Classification { get; set; }

        public int? Color { get; set; }

        public String Description { get; set; }

        public String ShortDescription { get; set; }

        public int HomeId { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        public Boolean IsClosed { get; set; }

        public String Title { get; set; }

        public Boolean? RefHide { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public RoomCategory RoomCategory { get; set; }

        public String RoomState { get; set; }

        public int? Size { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public BillItemCategory BillItemCategory { get; set; }

        public Boolean Hide { get; set; }

        public int Capacity { get; set; }

        public DateTime? DateModification { get; set; }

        public List<Bed> Beds { get; set; }
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