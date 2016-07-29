using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace DataGen.Types.Name {
    public static class Factory {
        public static StringFactory<T> StringFactory<T>(StringFactory<T> stringFactory) {
            return stringFactory;
        }
    }

    public class StringFactory<T> {
        protected StringFactory(string filePath) {
            FilePath = filePath;
        }

        protected IEnumerable<T> Source
            => JsonConvert.DeserializeObject<IEnumerable<T>>(File.ReadAllText(FilePath));

        private string FilePath { get; }

        public virtual IEnumerable<string> Collection(Func<T, IEnumerable<string>> func)
            => Filter.RepeatedData(Source.SelectMany(func));
    }

    public class NameFactory : StringFactory<NameRepository> {
        public NameFactory() : base("Data/Types/Name/data.json") {
        }

        public override IEnumerable<string> Collection(Func<NameRepository, IEnumerable<string>> func) {
            var names =
                Source.Select(func)
                    .Where(enumerable => enumerable != null)
                    .SelectMany(enumerable => enumerable);
            return Filter.RepeatedData(names);
        }
    }
}