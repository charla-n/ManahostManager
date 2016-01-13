using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    [Table("SatisfactionClientAnswer")]
    public class SatisfactionClientAnswer : IEntity
    {
        [Key]
        public int Id { get; set; }

        public String Answer { get; set; }

        public EAnswerType AnswerType { get; set; }

        public String Question { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public SatisfactionClient SatisfactionClient { get; set; }

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