using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace DataGen.Types.Name {
    public abstract class FileBasedData<T> {
        protected FileBasedData(string filePath) {
            FilePath = filePath;
        }

        protected IEnumerable<T> Source
            => JsonConvert.DeserializeObject<IEnumerable<T>>(File.ReadAllText(FilePath));

        private string FilePath { get; }

        public abstract IEnumerable<T> Collection(Func<T, IEnumerable<T>> func);
    }
}