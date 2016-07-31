using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace DataGen.Types {
    //Todo make abstracter
    public abstract class FileBasedData<TData, TFilter> {
        protected FileBasedData(string filePath) {
            FilePath = filePath;
        }

        protected IEnumerable<TData> Source
            => JsonConvert.DeserializeObject<IEnumerable<TData>>(File.ReadAllText(FilePath));

        private string FilePath { get; }
        protected TFilter Filter { get; set; }

        public abstract IEnumerable<TData> Collection(Func<TFilter, IEnumerable<TData>> func);
    }
}