using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    //Please note there is no home here, so it can't be accessed through the Advanced Search

    //TODO Check when a room is created if the numberofroom is not exceeded

    [Table("Subscription")]
    public class Subscription
    {
        [Key]
        public int Id { get; set; }

        public DateTime ExpirationDate { get; set; }

        //The user will only be able to create a fixed amount of rooms for what he payed
        public int NumberOfRoom { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Client Client { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public List<Payment> Payments { get; set; }
    }
}