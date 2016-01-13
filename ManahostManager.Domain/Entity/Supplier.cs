using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    [Table("Supplier")]
    public class Supplier : IEntity
    {
        [Key]
        public int Id { get; set; }

        public String SocietyName { get; set; }

        public String Addr { get; set; }

        public String Contact { get; set; }

        public String Comment { get; set; }

        public DateTime DateCreation { get; set; }

        public String Email { get; set; }

        public String Phone1 { get; set; }

        public String Phone2 { get; set; }

        public int HomeId { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        public Boolean Hide { get; set; }

        // Set by the API can't be modified by the manager
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