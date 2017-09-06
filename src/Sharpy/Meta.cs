using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;
using Sharpy.Implementation;
using Sharpy.Implementation.DataObjects;

[assembly: InternalsVisibleTo("Tests")]

namespace Sharpy {
    internal static class Data {
        private static readonly Lazy<IEnumerable<Name>> LazyNames = new Lazy<IEnumerable<Name>>(() => {
            var assembly = typeof(NameByOrigin).Assembly;
            var resourceStream = assembly.GetManifestResourceStream("Sharpy.Data.NamesByOrigin.json");
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8)) {
                return JsonConvert.DeserializeObject<IEnumerable<Name>>(reader.ReadToEnd());
            }
        });

        private static readonly Lazy<string[]> LazyUsernames = new Lazy<string[]>(() => {
            var assembly = typeof(Builder).Assembly;
            var resourceStream = assembly.GetManifestResourceStream("Sharpy.Data.usernames.txt");
            using (var reader = new StreamReader(resourceStream, Encoding.UTF8)) {
                return reader.ReadToEnd().Split(new[] {"\r\n", "\n"}, StringSplitOptions.None);
            }
        });

        internal static IEnumerable<Name> GetNames => LazyNames.Value;

        internal static string[] GetUserNames => LazyUsernames.Value;
    }
}