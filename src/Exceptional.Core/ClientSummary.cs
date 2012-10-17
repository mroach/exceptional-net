using Newtonsoft.Json;

namespace Exceptional.Core
{
    public class ClientSummary
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get { return "Exceptional.NET"; } }

        [JsonProperty(PropertyName = "version")]
        public string Version
        {
            get { return typeof(ClientSummary).Assembly.GetName().Version.ToString(); }
        }

        [JsonProperty(PropertyName = "protocol_version")]
        public string ProtocolVersion { get { return ExceptionalClient.ProtocolVersion; } }
    }
}