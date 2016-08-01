using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataGen.Types.Name;
using Newtonsoft.Json;

namespace DataGen.Types {
    public static class Factory {
        public static NameFilter NameDatas
            =>
                FilterConstructor(enumerable => new NameFilter(enumerable),
                    JsonConvert.DeserializeObject<IEnumerable<NameRepository>>(
                        File.ReadAllText("Data/Types/Name/collection.json")));

        public static TFilter FilterConstructor<TFilter, TData>(Func<IEnumerable<TData>, TFilter> func,
            IEnumerable<TData> collection) where TFilter : Filter<TData>
            => func(collection);
    }
}