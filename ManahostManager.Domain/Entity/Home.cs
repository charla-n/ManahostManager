using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    [Table("Home")]
    public class Home : IEntity
    {
        [Key]
        public int Id { get; set; }

        public EEstablishmentType EstablishmentType { get; set; }

        public String Title { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Client Client { get; set; }

        public int ClientId { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public String EncryptionPassword { get; set; }

        public String Address { get; set; }

        public DateTime? DateModification { get; set; }

        public Boolean isDefault { get; set; }

        public int? GetFK()
        {
            return null;
        }

        public void SetDateModification(DateTime? d)
        {
            DateModification = d;
        }
    }
}