using System;
using Exceptional.Core;
using NSpec;

namespace Exceptional.Tests
{
    public class describe_EnvironmentSummary : nspec
    {
        public void when_creating_environment_summaries()
        {
            EnvironmentSummary env = null;

            context["default/auto environment"] = () =>
                {
                    before = () => env = EnvironmentSummary.CreateFromEnvironment();
                    
                    it["language should be .NET"] = () => env.Language.should_be(".NET");
                    it["language version should be CLR version (major.minor)"] = () => env.LanguageVersion.should_be(Environment.Version.Major + "." + Environment.Version.Minor);
                    it["framework should be language + framework"] = () => env.Framework.should_be(".NET " + Environment.Version.Major + "." + Environment.Version.Minor);
                    it["number of env vars should match ENV"] = () => env.EnvironmentVariables.Count.should_be(Environment.GetEnvironmentVariables().Count);
                    it["RunAsUser should match logged-on user's name"] = () => env.RunAsUser.should_be(Environment.UserName);
                    it["Host should match machine name"] = () => env.Host.should_be(Environment.MachineName);
                    it["LoadedLibraries should be populated"] = () => env.LoadedLibraries.Length.should_be_greater_than(0);

                    // all the dynamic stuff going on here makes it hard if not impossible to predict the path
                    specify = () => env.ApplicationRootDirectory.should_not_be_empty();
                };

            context["with specified app path"] = () =>
                {
                    const string fixedPath = @"C:\inetpub\myapplication";
                    before = () => env = EnvironmentSummary.CreateFromEnvironment(fixedPath);

                    it["specified path overrides default"] = () => env.ApplicationRootDirectory.should_be(fixedPath);
                };

            context["with specified app path and username"] = () =>
            {
                const string fixedPath = @"C:\inetpub\myapplication";
                const string username = "customer_abc";

                before = () => env = EnvironmentSummary.CreateFromEnvironment(fixedPath, username);

                it["specified username overrides default"] = () => env.RunAsUser.should_be(username);
            };
        }
    }
}
