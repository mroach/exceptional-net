using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Exceptional.Core
{
    public static class Serializer
    {
        public static string Serialize(object obj)
        {
            var settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.Indented;

            return JsonConvert.SerializeObject(obj, settings);
        }
    }
}
