using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    // Satisfaction of a client is a one shot, he can't modify its responses after, neither the manager (he's just able to delete)

    [Table("SatisfactionClient")]
    public class SatisfactionClient : IEntity
    {
        public SatisfactionClient()
        {
            SatisfactionClientAnswers = new List<SatisfactionClientAnswer>();
        }

        [Key]
        public int Id { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Client ClientDest { get; set; }

        public String Code { get; set; }

        public DateTime? DateAnswered { get; set; }

        public int HomeId { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public People PeopleDest { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Booking Booking { get; set; }

        public DateTime? DateModification { get; set; }

        public List<SatisfactionClientAnswer> SatisfactionClientAnswers { get; set; }

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