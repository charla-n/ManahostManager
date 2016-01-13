using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    // Configuration of the satisfaction survey by the manager

    [Table("SatisfactionConfig")]
    public class SatisfactionConfig : IEntity
    {
        public SatisfactionConfig()
        {
            SatisfactionConfigQuestions = new List<SatisfactionConfigQuestion>();
        }

        [Key]
        public int Id { get; set; }

        public String AdditionalInfo { get; set; }

        public String Description { get; set; }

        public String Title { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        public DateTime? DateModification { get; set; }

        public List<SatisfactionConfigQuestion> SatisfactionConfigQuestions { get; set; }

        public int? GetFK()
        {
            return Id;
        }

        public void SetDateModification(DateTime? d)
        {
            DateModification = d;
        }
    }
}