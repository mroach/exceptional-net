using System.Linq;
using System.Reflection;
using NSpec;
using NSpec.Domain;
using NUnit.Framework;

[TestFixture]
public class DebuggerShim
{
    [Test]
    public void debug()
    {
        /*
        var tagOrClassName = "describe_ExceptionSummary";

        var invocation = new RunnerInvocation(Assembly.GetExecutingAssembly().Location, tagOrClassName);
        
        var contexts = invocation.Run();

        //assert that there aren't any failures
        contexts.Failures().Count().should_be(0);
        */
        var describers = GetType().Assembly.GetTypes().Where(t => typeof (nspec).IsAssignableFrom(t)).Select(t => t.Name).ToList();

        foreach (var d in describers)
        {
            var invocation = new RunnerInvocation(Assembly.GetExecutingAssembly().Location, d);
            var contexts = invocation.Run();
            contexts.Failures().Count().should_be(0);
        }
    }
}
