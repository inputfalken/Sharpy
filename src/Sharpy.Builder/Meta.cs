﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;
using Sharpy.Builder.Implementation;
using Sharpy.Builder.Implementation.DataObjects;

[assembly: InternalsVisibleTo("Tests")]

namespace Sharpy.Builder {
    internal static class Data {
        private const string DataFolder = "Sharpy.Builder.Data";

        private static readonly Lazy<IEnumerable<Name>> LazyNames = new Lazy<IEnumerable<Name>>(() => {
            var assembly = typeof(NameByOrigin).Assembly;
            var resourceStream = assembly.GetManifestResourceStream($"{DataFolder}.NamesByOrigin.json");
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8)) {
                return JsonConvert.DeserializeObject<IEnumerable<Name>>(reader.ReadToEnd());
            }
        });

        private static readonly Lazy<IReadOnlyList<PostalCode>> LazyPostalCodes = new Lazy<IReadOnlyList<PostalCode>>(
            () => {
                var assembly = typeof(SwePostalCodeRandomizer).Assembly;
                var resourceStream = assembly.GetManifestResourceStream($"{DataFolder}.swedishPostalCodes.json");
                using (var reader = new StreamReader(resourceStream, Encoding.UTF8)) {
                    return JsonConvert.DeserializeObject<IEnumerable<PostalCode>>(reader.ReadToEnd()).ToArray();
                }
            });

        private static readonly Lazy<string[]> LazyUsernames = new Lazy<string[]>(() => {
            var assembly = typeof(Builder).Assembly;
            var resourceStream = assembly.GetManifestResourceStream($"{DataFolder}.usernames.txt");
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8)) {
                return reader.ReadToEnd().Split(new[] {"\r\n", "\n"}, StringSplitOptions.None);
            }
        });

        internal static IEnumerable<Name> GetNames => LazyNames.Value;
        internal static IReadOnlyList<PostalCode> GetPostalcodes => LazyPostalCodes.Value;

        internal static string[] GetUserNames => LazyUsernames.Value;
    }
}