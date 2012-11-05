using System;
using System.Text;
using Newtonsoft.Json;

namespace Exceptional.Core
{
    public class Alert
    {
        [JsonProperty(PropertyName = "request")]
        public HttpRequestSummary Request { get; set; }

        [JsonProperty(PropertyName = "application_environment")]
        public EnvironmentSummary Environment { get; set; }

        [JsonProperty(PropertyName = "exception")]
        public ExceptionSummary Exception { get; set; }

        [JsonProperty(PropertyName = "client")]
        public ClientSummary Client { get; protected set; }

        public Alert()
        {
            Client = new ClientSummary();
            Environment = EnvironmentSummary.CreateFromEnvironment();
        }

        public Alert(Exception ex) : this()
        {
            Exception = ExceptionSummary.CreateFromException(ex);
        }
        
        public string UniquenessHash()
        {
            var data = Encoding.UTF8.GetBytes(string.Join("", Exception.Backtrace));

            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var hashBytes = md5.ComputeHash(data);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public void Validate()
        {
            if (Exception == null)
                throw new ExceptionalValidationException("Exception summary is missing.");

            if (Environment == null)
                throw new ExceptionalValidationException("Environment summary is missing");
            
            if (Exception.Backtrace == null)
                throw new ExceptionalValidationException("Exception is missing the backtrace.");

            if (string.IsNullOrEmpty(Exception.ExceptionClass))
                throw new ExceptionalValidationException("Exception is missing the exception class.");

            if (string.IsNullOrEmpty(Exception.Message))
                throw new ExceptionalValidationException("Exception message is missing.");

            if (string.IsNullOrEmpty(Environment.ApplicationRootDirectory))
                throw new ExceptionalValidationException("Application root directory is missing.");
        }
    }
}
