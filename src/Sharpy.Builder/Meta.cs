using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Sharpy.Builder.Implementation.DataObjects;
using static Newtonsoft.Json.JsonConvert;

[assembly: InternalsVisibleTo("Sharpy.Builder.Tests")]

namespace Sharpy.Builder {
    internal static class Data {
        private const string DataFolder = "Sharpy.Builder.Data";
        private static readonly Lazy<Assembly> LazyAssembly = new Lazy<Assembly>(() => typeof(Builder).Assembly);
        private static Assembly Assembly => LazyAssembly.Value;

        private static readonly Lazy<IEnumerable<Name>> LazyNames = new Lazy<IEnumerable<Name>>(() => {
            var resourceStream = Assembly.GetManifestResourceStream($"{DataFolder}.NamesByOrigin.json");
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8)) {
                return DeserializeObject<IEnumerable<Name>>(reader.ReadToEnd());
            }
        });

        private static readonly Lazy<IReadOnlyList<PostalCode>> LazyPostalCodes = new Lazy<IReadOnlyList<PostalCode>>(
            () => {
                var resourceStream = Assembly.GetManifestResourceStream($"{DataFolder}.swedishPostalCodes.json");
                using (var reader = new StreamReader(resourceStream, Encoding.UTF8)) {
                    return DeserializeObject<IEnumerable<PostalCode>>(reader.ReadToEnd()).ToArray();
                }
            });

        private static readonly Lazy<string[]> LazyUsernames = new Lazy<string[]>(() => {
            var resourceStream = Assembly.GetManifestResourceStream($"{DataFolder}.usernames.txt");
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8)) {
                return reader.ReadToEnd().Split(new[] {"\r\n", "\n"}, StringSplitOptions.None);
            }
        });

        internal static IEnumerable<Name> GetNames => LazyNames.Value;
        internal static IReadOnlyList<PostalCode> GetPostalcodes => LazyPostalCodes.Value;

        internal static string[] GetUserNames => LazyUsernames.Value;
    }
}