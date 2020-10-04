using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Sharpy.Builder.Implementation.DataObjects;

[assembly: InternalsVisibleTo("Sharpy.Builder.Tests")]

namespace Sharpy.Builder
{
    internal static class Data
    {
        private const string DataFolder = "Sharpy.Builder.Data";
        private static readonly Lazy<Assembly> LazyAssembly = new Lazy<Assembly>(() => typeof(Builder).Assembly);
        private static Assembly Assembly => LazyAssembly.Value;

        private static readonly Lazy<IReadOnlyList<NameModel>> LazyNames = new Lazy<IReadOnlyList<NameModel>>(() =>
        {
            var resourceStream = Assembly.GetManifestResourceStream($"{DataFolder}.NamesByOrigin.json");
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                var jsonSerializerOptions = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
                jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                return JsonSerializer
                    .Deserialize<IReadOnlyList<NameModel>>(
                        reader.ReadToEnd(),
                        jsonSerializerOptions
                    );
            }
        });

        private static readonly Lazy<string[]> LazyUsernames = new Lazy<string[]>(() =>
        {
            var resourceStream = Assembly.GetManifestResourceStream($"{DataFolder}.usernames.txt");
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                return reader.ReadToEnd().Split(new[] {"\r\n", "\n"}, StringSplitOptions.None);
            }
        });

        internal static IEnumerable<NameModel> GetNames => LazyNames.Value;
        internal static string[] GetUserNames => LazyUsernames.Value;
    }
}