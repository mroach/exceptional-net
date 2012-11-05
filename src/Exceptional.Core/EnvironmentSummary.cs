using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Exceptional.Core
{
    public class EnvironmentSummary
    {
        [JsonProperty(PropertyName = "framework")]
        public string Framework { get; set; }

        [JsonProperty(PropertyName = "env")]
        public IDictionary<string, string> EnvironmentVariables { get; set; }

        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        [JsonProperty(PropertyName = "language_version")]
        public string LanguageVersion { get; set; }

        [JsonProperty(PropertyName = "application_root_directory")]
        public string ApplicationRootDirectory { get; set; }

        [JsonProperty(PropertyName = "run_as_user")]
        public string RunAsUser { get; set; }

        [JsonProperty(PropertyName = "host")]
        public string Host { get; set; }

        [JsonProperty(PropertyName = "loaded_libraries")]
        public string[] LoadedLibraries { get; set; }

        /// <summary>
        /// Creates a new <see cref="EnvironmentSummary"/> populating all properties with defaults. If you're running
        /// this as a web application you will want to reset the <see cref="ApplicationRootDirectory"/> to be the physical
        /// application path of your web app and <see cref="RunAsUser"/> to be the application's logged-in username
        /// </summary>
        /// <returns></returns>
        public static EnvironmentSummary CreateFromEnvironment(string applicationRootDirectory = null, string username = null)
        {
            var summary = new EnvironmentSummary();

            summary.EnvironmentVariables = Environment.GetEnvironmentVariables().Cast<DictionaryEntry>().ToDictionary(e => e.Key.ToString(), v => v.Value.ToString());
            summary.Language = ".NET"; // there is no way of telling what language the executing assembly is
            summary.LanguageVersion = Environment.Version.Major + "." + Environment.Version.Minor;
            summary.Framework = summary.Language + " " + summary.LanguageVersion;
            summary.ApplicationRootDirectory = applicationRootDirectory ?? DefaultApplicationRootDirectory();
            summary.RunAsUser = username ?? Environment.UserName;
            summary.Host = Environment.MachineName;
            summary.LoadedLibraries = AppDomain.CurrentDomain.GetAssemblies().Select(a => a.FullName).OrderBy(s => s).ToArray();

            return summary;
        }

        public static string DefaultApplicationRootDirectory()
        {
            var asm = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
            return Path.GetDirectoryName(asm.Location);
        }
    }
}