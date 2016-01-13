using Jil;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ManahostManager.Utils
{
    internal static class TypedDeserializers
    {
        private static readonly ConcurrentDictionary<Type, Func<TextReader, Options, object>> _methods;
        private static readonly MethodInfo _method = typeof(JSON).GetMethod("Deserialize", new[] { typeof(TextReader), typeof(Options) });

        static TypedDeserializers()
        {
            _methods = new ConcurrentDictionary<Type, Func<TextReader, Options, object>>();
        }

        public static Func<TextReader, Options, object> GetTyped(Type type)
        {
            return _methods.GetOrAdd(type, CreateDelegate);
        }

        private static Func<TextReader, Options, object> CreateDelegate(Type type)
        {
            return (Func<TextReader, Options, object>)_method
                .MakeGenericMethod(type)
                .CreateDelegate(typeof(Func<TextReader, Options, object>));
        }
    }

    public class JilFormatter : MediaTypeFormatter
    {
        public static readonly Options _jilOptions = new Options(unspecifiedDateTimeKindBehavior: UnspecifiedDateTimeKindBehavior.IsUTC, dateFormat: DateTimeFormat.ISO8601);

        public JilFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(GenericNames.APP_JSON));

            SupportedEncodings.Add(new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true));
            SupportedEncodings.Add(new UnicodeEncoding(bigEndian: false, byteOrderMark: true, throwOnInvalidBytes: true));
        }

        public override bool CanReadType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            return true;
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, System.Net.Http.HttpContent content, IFormatterLogger formatterLogger)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (readStream == null)
            {
                throw new ArgumentNullException("readStream");
            }

            return Task.FromResult<object>(DeSerialize(type, readStream, content, formatterLogger));
        }

        private object DeSerialize(Type type, Stream readStream, System.Net.Http.HttpContent content, IFormatterLogger formatterLogger)
        {
            HttpContentHeaders contentHeaders = content == null ? null : content.Headers;

            // If content length is 0 then return default value for this type
            if (contentHeaders != null && contentHeaders.ContentLength == 0)
            {
                return GetDefaultValueForType(type);
            }

            // Get the character encoding for the content
            Encoding effectiveEncoding = SelectCharacterEncoding(contentHeaders);

            try
            {
                using (var reader = new StreamReader(readStream, effectiveEncoding, false, 512, true))
                {
                    var deserialize = TypedDeserializers.GetTyped(type);
                    return deserialize(reader, _jilOptions);
                }
            }
            catch (Exception e)
            {
                if (formatterLogger == null)
                {
                    throw;
                }
                formatterLogger.LogError(String.Empty, e);
                return GetDefaultValueForType(type);
            }
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, System.Net.Http.HttpContent content, TransportContext transportContext)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (writeStream == null)
            {
                throw new ArgumentNullException("writeStream");
            }

            return Task.FromResult(Serialize(type, value, writeStream, content, transportContext));
        }

        private object Serialize(Type type, object value, Stream writeStream, System.Net.Http.HttpContent content, TransportContext transportContext)
        {
            Encoding effectiveEncoding = SelectCharacterEncoding(content == null ? null : content.Headers);

            using (var writer = new StreamWriter(writeStream, effectiveEncoding, 512, true))
            {
                JSON.Serialize(value, writer, _jilOptions);
                writer.Flush();
            }
            return null;
        }
    }
}