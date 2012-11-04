using System;
using Exceptional.Core;
using NSpec;

namespace Exceptional.Tests.Standard
{
    public class describe_Alert : nspec
    {
        public void when_creating_an_alert()
        {
            Alert alert = null;

            context["without setting any values"] = () =>
                {
                    before = () => alert = new Alert();
                    it["ClientSummary should not be null"] = () => alert.Client.should_not_be_null();
                    it["Environment should not be null"] = () => alert.Environment.should_not_be_null();
                    it["should fail validation"] = () => expect<ExceptionalValidationException>(() => alert.Validate());
                };

            context["when created with an exception constructor argument"] = () =>
                {
                    before = () => alert = new Alert(CreateException("A great failure has occurred."));

                    it["should have the Exception populated"] = () => alert.Exception.should_not_be_null();
                    it["should pass validation"] = () => alert.Validate();
                    it["should serialize"] = () => Console.WriteLine(Serializer.Serialize(alert));
                    //Console.WriteLine(Serializer.Serialize(alert));
                };
        }

        protected Exception CreateException(string message)
        {
            try
            {
                throw new ArgumentException(message);
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
