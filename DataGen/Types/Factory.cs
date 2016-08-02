using System;
using System.Collections.Generic;
using System.IO;
using DataGen.Types.Name;
using Newtonsoft.Json;

namespace DataGen.Types {
    public static class Factory {
        public static NameFilter NameDatas {
            get {
                return Filter(enumerable => new NameFilter(enumerable),
                    JsonConvert.DeserializeObject<IEnumerable<Name.Name>>(
                        File.ReadAllText("Data/Types/Name/newData.json")));
            }
        }

        public static TFilter Filter<TFilter, TData>(Func<IEnumerable<TData>, TFilter> func,
            IEnumerable<TData> collection) where TFilter : Filter<TData>
            => func(collection);
    }
}