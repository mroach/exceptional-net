using System;
using Exceptional.Core;
using NSpec;

namespace Exceptional.Tests
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
                    specify = () => summary.OccurredAt.should_be_less_or_equal_to(DateTime.UtcNow);
                };
        }

    }
}
