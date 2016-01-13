using MsgPack.Serialization;
using System;

namespace ManahostManager.Utils.Formatters
{
    public class MsgPackDatetimeSerializer : MessagePackSerializer<DateTime>
    {
        public MsgPackDatetimeSerializer(SerializationContext ctx) : base(ctx)
        {
        }

        protected override void PackToCore(MsgPack.Packer packer, DateTime objectTree)
        {
            packer.Pack(objectTree.Ticks);
        }

        protected override DateTime UnpackFromCore(MsgPack.Unpacker unpacker)
        {
            return new DateTime((long)unpacker.LastReadData);
        }
    }
}