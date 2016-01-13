using MsgPack.Serialization;
using System;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    public class Test
    {
        [IgnoreDataMember]
        [MessagePackIgnore]
        public int Id { get; set; }

        public String TestStr { get; set; }

        public String CannotSet { get; set; }
    }
}