using ManahostManager.Utils;
using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    // Payment for accessing all Manahost features => VIP
    // No HomeId so this table won't be accessible through the advanced search

    [Table("Payment")]
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String AccountName { get; set; }

        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Label { get; set; }

        public String RemoteIP { get; set; }

        public Decimal? Price { get; set; }

        public int SubscriptionId { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Subscription Subscription { get; set; }
    }
}