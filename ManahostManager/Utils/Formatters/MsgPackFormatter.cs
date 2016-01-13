using MsgPack;
using MsgPack.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ManahostManager.Utils.Formatters
{
    public class MsgPackFormatter : MediaTypeFormatter
    {
        private const string mime = GenericNames.APP_MSGPACK;
        public static readonly SerializationContext sContext = new SerializationContext();

        private Func<Type, bool> IsAllowedType = (t) =>
         {
             if (!t.IsAbstract && !t.IsInterface && t != null && !t.IsNotPublic)
                 return true;

             if (typeof(IEnumerable).IsAssignableFrom(t))
                 return true;

             return false;
         };

        public MsgPackFormatter()
        {
            sContext.Serializers.RegisterOverride(new MsgPackDatetimeSerializer(sContext));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(mime));
        }

        public override bool CanReadType(Type type)
        {
            if (type == null) throw new ArgumentNullException("type is null");
            return IsAllowedType(type);
        }

        public override bool CanWriteType(Type type)
        {
            if (type == null) throw new ArgumentNullException("Type is null");
            return IsAllowedType(type);
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream stream, HttpContent content, TransportContext transportContext)
        {
            if (type == null) throw new ArgumentNullException("type is null");
            if (stream == null) throw new ArgumentNullException("Write stream is null");

            var tcs = new TaskCompletionSource<object>();

            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                value = (value as IEnumerable<object>).ToList();
            }

            var serializer = MessagePackSerializer.Get(type, sContext);
            serializer.Pack(stream, value);

            tcs.SetResult(null);
            return tcs.Task;
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream stream, HttpContent content, IFormatterLogger formatterLogger)
        {
            var tcs = new TaskCompletionSource<object>();
            if (content.Headers != null && content.Headers.ContentLength == 0) return null;
            try
            {
                var serializer = MessagePackSerializer.Get(type, sContext);
                object result;

                using (var unpacker = Unpacker.Create(stream))
                {
                    unpacker.Read();
                    result = serializer.UnpackFrom(unpacker);
                }
                tcs.SetResult(result);
            }
            catch (Exception e)
            {
                if (formatterLogger == null) throw;
                formatterLogger.LogError(String.Empty, e.Message);
                tcs.SetResult(GetDefaultValueForType(type));
            }

            return tcs.Task;
        }
    }
}