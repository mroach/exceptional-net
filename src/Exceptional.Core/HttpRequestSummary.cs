using System.Collections.Generic;
using Newtonsoft.Json;

namespace Exceptional.Core
{
    public class HttpRequestSummary
    {
        [JsonProperty(PropertyName = "session")]
        public IDictionary<string, object> Session { get; set; }

        [JsonProperty(PropertyName = "remote_ip")]
        public string RemoteIp { get; set; }

        [JsonProperty(PropertyName = "parameters")]
        public IDictionary<string, object> Parameters { get; set; }

        [JsonProperty(PropertyName = "action")]
        public string Action { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "request_method")]
        public string RequestMethod { get; set; }

        [JsonProperty(PropertyName = "headers")]
        public IDictionary<string, string> Headers { get; set; }

        public HttpRequestSummary()
        {
            Session = new Dictionary<string, object>();
            Parameters = new Dictionary<string, object>();
            Headers = new Dictionary<string, string>();
        }   
    }
}