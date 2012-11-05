using System;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;

namespace Exceptional.Core
{
    public class ExceptionSummary
    {
        [JsonProperty(PropertyName = "occurred_at")]
        public string OccurredAt { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "backtrace")]
        public string[] Backtrace { get; set; }

        [JsonProperty(PropertyName = "exception_class")]
        public string ExceptionClass { get; set; }

        public ExceptionSummary()
        {
            OccurredAt = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture);
        }

        public static ExceptionSummary CreateFromException(Exception ex)
        {
            var summary = new ExceptionSummary();
            summary.Message = ex.Message;
            summary.Backtrace = ex.StackTrace.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries).Select(line => line.Trim()).ToArray();
            summary.ExceptionClass = ex.GetType().Name;
            return summary;
        }
    }
}