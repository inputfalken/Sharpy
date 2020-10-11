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
            const string path = DataFolder + ".NamesByOrigin.json";
            var namesByOriginStream = Assembly.GetManifestResourceStream(path);

            if (namesByOriginStream is null)
                throw new ArgumentNullException(
                    nameof(namesByOriginStream),
                    $"Could not obtain user names from '{path}'."
                );

            using (var reader = new StreamReader(namesByOriginStream, Encoding.UTF8))
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

        private static readonly Lazy<IReadOnlyList<string>> LazyUsernames = new Lazy<IReadOnlyList<string>>(() =>
        {
            const string path = DataFolder + ".usernames.txt";
            var userNamesStream = Assembly.GetManifestResourceStream(path);

            if (userNamesStream is null)
                throw new ArgumentNullException(
                    nameof(userNamesStream),
                    $"Could not obtain user names from '{path}'."
                );

            using (var reader = new StreamReader(userNamesStream, Encoding.UTF8))
            {
                //TODO use source code generator to determine the capacity in the future.
                var list = new List<string>(90000);
                while (!reader.EndOfStream) list.Add(reader.ReadLine());
                return list;
            }
        });

        internal static IReadOnlyList<NameModel> GetNames => LazyNames.Value;
        internal static IReadOnlyList<string> GetUserNames => LazyUsernames.Value;
    }
}