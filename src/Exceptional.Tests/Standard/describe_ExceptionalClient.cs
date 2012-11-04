using Exceptional.Core;
using NSpec;

namespace Exceptional.Tests.Standard
{
    public class describe_ExceptionalClient : nspec
    {
        public void when_creating_exceptional_client()
        {
            context["static properties"] = () =>
                {
                    it["protocol version should be 6"] = () => ExceptionalClient.ProtocolVersion.should_be("6");
                    it["endpoint format should be set"] = () => ExceptionalClient.EndpointUrlFormat.should_be("http://api.exceptional.io/api/errors?api_key={0}&protocol_version={1}");
                };

            ExceptionalClient client = null;

            context["with api key"] = () =>
                {
                    const string apiKey = "1234567890";

                    before = () => client = new ExceptionalClient(apiKey);

                    it["ApiKey property should match"] = () => client.ApiKey.should_be(apiKey);
                    it["should generate endpoint URL containing API key and protocol version"] = () =>
                        client.GetEndpointUrl().should_be("http://api.exceptional.io/api/errors?api_key=" + apiKey +
                                                          "&protocol_version=" + ExceptionalClient.ProtocolVersion);
                    it["should pass validation"] = () => client.Validate();
                };

            context["with null api key"] = () =>
                {
                    before = () => client = new ExceptionalClient(null);

                    it["should not validate"] = () => expect<ExceptionalValidationException>(() => client.Validate());
                    it["should refuse to send"] = () => expect<ExceptionalValidationException>(() => client.Send(null));
                };
        }
    }
}
