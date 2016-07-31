using System;
using System.Collections.Generic;
using System.IO;
using DataGen.Types.Name;
using Newtonsoft.Json;

namespace DataGen.Types {
    //Todo make abstracter
    public class FileBasedData<TData, TFilter> : Data<TData, TFilter> where TFilter : Filter<TData> {
        public FileBasedData(string filePath, Func<IEnumerable<TData>, TFilter> factory) : base(factory) {
            FilePath = filePath;
        }


        protected sealed override IEnumerable<TData> Datas
            => JsonConvert.DeserializeObject<IEnumerable<TData>>(File.ReadAllText(FilePath));


        private string FilePath { get; }
    }
}