using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    // Log for each client the size of the total of his documents and the additional stockage he bought.
    // The purpose of this entity is to limit the total size that he can upload.
    // All size are in bytes.
    // You can get this entity with the advanced research, but you cant POST/PUT/DELETE
    // Set by the API can't be modified by the manager

    [Table("DocumentLog")]
    public class DocumentLog : IEntity
    {
        [Key]
        [IgnoreDataMember]
        [MessagePackIgnore]
        public int Id { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Client Client { get; set; }

        public long CurrentSize { get; set; }

        public long BuySize { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public ResourceConfig ResourceConfig { get; set; }

        public DateTime? DateModification { get; set; }

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