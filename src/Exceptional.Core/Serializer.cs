using System.Linq;
using Newtonsoft.Json;

namespace Exceptional.Core
{
    public static class Serializer
    {
        public static string Serialize(object obj, bool indented = true)
        {
            var settings = new JsonSerializerSettings {Formatting = indented ? Formatting.Indented : Formatting.None};

            return JsonConvert.SerializeObject(obj, settings);
        }

        public static string SanitizeItem(object obj)
        {
            if (obj == null)
                return null;

            if (obj is string)
                return obj.ToString();

            if (obj.GetType().IsArray)
            {
                var items = (object[]) obj;
                return "[" + string.Join(", ", items.Select(SanitizeItem).ToArray()) + "]";
            }

            return Serialize(obj, false);
        }
    }
}
