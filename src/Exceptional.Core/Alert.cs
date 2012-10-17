using System;
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
        
        public void Validate()
        {
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
