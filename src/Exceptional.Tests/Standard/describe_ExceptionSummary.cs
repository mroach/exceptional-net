using System;
using System.Globalization;
using Exceptional.Core;
using NSpec;

namespace Exceptional.Tests.Standard
{
    public class describe_ExceptionSummary : nspec
    {
        public void when_creating_exceptionsummary_from_exception()
        {
            ExceptionSummary summary;
            
            try
            {
                throw new ExceptionalValidationException("Test exception");
            }
            catch (Exception ex)
            {
                summary = ExceptionSummary.CreateFromException(ex);
            }

            context["locally-created exception with message Test exception"] = () =>
                {
                    specify = () => summary.Message.should_be("Test exception");
                    specify = () => summary.Backtrace.Length.should_be_greater_than(0);
                    specify = () => summary.ExceptionClass.should_be("ExceptionalValidationException");
                    specify = () => DateTime.ParseExact(summary.OccurredAt, "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK", CultureInfo.CurrentCulture).should_be_less_or_equal_to(DateTime.Now);
                };
        }

    }
}
