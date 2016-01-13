using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    // This table has multiple purpose
    // It will be used by managers for discount
    // It will be used by administrator for creating beta/trial keys
    // It will be used by administrator for discount on the price of Manahost

    [Table("KeyGenerator")]
    public class KeyGenerator : IEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateExp { get; set; }

        public EKeyType KeyType { get; set; }

        public EValueType ValueType { get; set; }

        public String Key { get; set; }

        public Decimal? Price { get; set; }

        public String Description { get; set; }

        public int? HomeId { get; set; }

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