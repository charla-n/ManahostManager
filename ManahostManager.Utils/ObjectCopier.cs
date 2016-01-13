using Newtonsoft.Json;

namespace ManahostManager.Utils
{
    public static class ObjectCopier
    {
        public static T Clone<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);

            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}