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
        private static Assembly Assembly
        {
            get { return LazyAssembly.Value; }
        }

        private static readonly Lazy<IReadOnlyList<NameModel>> LazyNames = new Lazy<IReadOnlyList<NameModel>>(() =>
        {
            const string path = DataFolder + ".NamesByOrigin.json";
            const string exceptionMessage = "Could not obtain user names from '" + path + "'.";

            var namesByOriginStream = Assembly.GetManifestResourceStream(path);

            if (namesByOriginStream is null)
                throw new ArgumentNullException(
                    nameof(namesByOriginStream),
                    exceptionMessage
                );

            using var reader = new StreamReader(namesByOriginStream, Encoding.UTF8);
            var jsonSerializerOptions = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            var readOnlyList = JsonSerializer
                .Deserialize<IReadOnlyList<NameModel>>(
                    reader.ReadToEnd(),
                    jsonSerializerOptions
                );
            if (readOnlyList is null)
                throw new NullReferenceException(exceptionMessage);

            return readOnlyList;
        });

        private static readonly Lazy<IReadOnlyList<string>> LazyUsernames = new Lazy<IReadOnlyList<string>>(() =>
        {
            const string path = DataFolder + ".usernames.txt";
            const string exceptionMessage = "Could not obtain user names from '" + path + "'.";
            var userNamesStream = Assembly.GetManifestResourceStream(path);

            if (userNamesStream is null)
                throw new ArgumentNullException(
                    nameof(userNamesStream),
                    exceptionMessage
                );

            using var reader = new StreamReader(userNamesStream, Encoding.UTF8);
            //TODO use source code generator to determine the capacity in the future.
            var list = new List<string>(90000);
            while (!reader.EndOfStream)
            {
                if (reader.ReadLine() is {} line) 
                    list.Add(line);
            }

            if (list.Count == 0)
                throw new ArgumentException(exceptionMessage);

            return list;
        });

        internal static IReadOnlyList<NameModel> GetNames
        {
            get { return LazyNames.Value; }
        }

        internal static IReadOnlyList<string> GetUserNames
        {
            get { return LazyUsernames.Value; }
        }
    }
}