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

    public abstract class StringFactory<T> {
        protected StringFactory(string filePath) {
            FilePath = filePath;
        }

        protected IEnumerable<T> Source
            => JsonConvert.DeserializeObject<IEnumerable<T>>(File.ReadAllText(FilePath));

        private string FilePath { get; }

        public abstract IEnumerable<T> Collection(Func<T, IEnumerable<T>> func);
    }

    public class NameFactory : StringFactory<NameRepository> {
        public NameFactory() : base("Data/Types/Name/data.json") {
        }

        public override IEnumerable<NameRepository> Collection(Func<NameRepository, IEnumerable<NameRepository>> func) {
            return Filter.RepeatedData(Source.Select(func)
                .Where(enumerable => enumerable != null)
                .SelectMany(enumerable => enumerable));
        }
    }
}