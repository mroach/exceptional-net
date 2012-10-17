using System;
using System.Runtime.Serialization;

namespace Exceptional.Core
{
    [Serializable]
    public class ExceptionalValidationException : Exception
    {
        public ExceptionalValidationException()
        {
        }

        public ExceptionalValidationException(string message) : base(message)
        {
        }

        public ExceptionalValidationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ExceptionalValidationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
