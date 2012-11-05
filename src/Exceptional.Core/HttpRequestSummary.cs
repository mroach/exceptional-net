using System.Collections.Generic;
using Newtonsoft.Json;

namespace Exceptional.Core
{
    public class HttpRequestSummary
    {
        [JsonProperty(PropertyName = "session")]
        public IDictionary<string, string> Session { get; set; }

        [JsonProperty(PropertyName = "remote_ip")]
        public string RemoteIp { get; set; }

        [JsonProperty(PropertyName = "parameters")]
        public IDictionary<string, string> Parameters { get; set; }

        [JsonProperty(PropertyName = "action")]
        public string Action { get; set; }

        [JsonProperty(PropertyName = "controller")]
        public string Controller { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "request_method")]
        public string RequestMethod { get; set; }

        [JsonProperty(PropertyName = "headers")]
        public IDictionary<string, string> Headers { get; set; }

        public HttpRequestSummary()
        {
            Session = new Dictionary<string, string>();
            Parameters = new Dictionary<string, string>();
            Headers = new Dictionary<string, string>();
        }   
    }
}