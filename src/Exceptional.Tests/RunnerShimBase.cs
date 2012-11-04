using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NSpec;
using NSpec.Domain;
using NUnit.Framework;

namespace Exceptional.Tests
{
    [TestFixture]
    public abstract class RunnerShimBase
    {
        public abstract string Type { get; }

        protected IList<string> FindFixtures()
        {
            return GetType().Assembly.GetTypes()
                .Where(t => typeof(nspec).IsAssignableFrom(t))
                .Where(t => t.Namespace.EndsWith("." + Type))
                .Select(t => t.Name)
                .ToList();
        }

        [Test]
        public void Run()
        {
            var fixtures = FindFixtures();

            if (!fixtures.Any())
                throw new Exception("No fixtures found in the " + Type + " namespace");

            foreach (var d in fixtures)
            {
                var invocation = new RunnerInvocation(Assembly.GetExecutingAssembly().Location, d);
                var contexts = invocation.Run();
                contexts.Failures().Count().should_be(0);
            }
        }
    }

    public class StandardRunnerShim : RunnerShimBase
    {
        public override string Type
        {
            get { return "Standard"; }
        }
    }

    public class IntegrationRunnerShim : RunnerShimBase
    {
        public override string Type
        {
            get { return "Integration"; }
        }
    }
}
