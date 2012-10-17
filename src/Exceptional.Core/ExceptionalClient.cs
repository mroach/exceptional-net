using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace Exceptional.Core
{
    public class ExceptionalClient
    {
        public const string ProtocolVersion = "6";

        public const string EndpointUrlFormat = "http://api.exceptional.io/api/errors?api_key={0}&protocol_version={1}";

        public string ApiKey { get; set; }

        public ExceptionalClient(string apiKey)
        {
            ApiKey = apiKey;
        }

        public string GetEndpointUrl()
        {
            return string.Format(EndpointUrlFormat, ApiKey, ProtocolVersion);
        }
        
        public void Send(Alert alert, Action<string> debug = null)
        {
            Validate();
            alert.Validate();

            if (debug == null)
                debug = message => Trace.WriteLine(message);

            var json = Serializer.Serialize(alert);
            var jsonBytes = Encoding.UTF8.GetBytes(json);

            debug("Generated Exceptional request JSON: " + json);

            var url = GetEndpointUrl();

            debug("POST to " + url);

            var httpRequest = (HttpWebRequest) WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            httpRequest.UserAgent = alert.Client.Name + " " + alert.Client.Version;

            using (var postStream = httpRequest.GetRequestStream())
            using (var gzipStream = new GZipStream(postStream, CompressionMode.Compress))
            {
                gzipStream.Write(jsonBytes, 0, jsonBytes.Length);

                gzipStream.Close();
                postStream.Close();
            }

            var httpResponse = (HttpWebResponse) httpRequest.GetResponse();
            string responseText;

            using (var responseStream = new StreamReader(httpResponse.GetResponseStream()))
            {
                responseText = responseStream.ReadToEnd();
            }

            debug("Got response from Exceptional: " + responseText);
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(ApiKey))
                throw new ExceptionalValidationException("API key has not been set");
        }
    }
}
