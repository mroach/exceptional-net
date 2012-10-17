using System.Reflection;
using Exceptional.Core;
using NSpec;

namespace Exceptional.Tests
{
    public class describe_ClientSummary : nspec
    {
        public void when_creating_client_summary()
        {
            ClientSummary summary = null;

            context["with default values"] = () =>
                {
                    before = () => summary = new ClientSummary();

                    it["Name should be Exceptional.NET"] = () => summary.Name.should_be("Exceptional.NET");
                    it["ProtocolVersion should be 6"] = () => summary.ProtocolVersion.should_be("6");
                    it["Version should match Exceptional.Core version"] = () => summary.Version.should_be(Assembly.GetAssembly(typeof (ClientSummary)).GetName().Version.ToString());
                };
        }
    }
}
