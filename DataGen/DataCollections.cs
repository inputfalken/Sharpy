using System;
using System.Collections.Generic;
using System.IO;
using DataGen.Types.Name;
using DataGen.Types.String;
using Newtonsoft.Json;

namespace DataGen {
    public static class DataCollections {
        public static Lazy<NameFilter> NamesByOrigin
            => new Lazy<NameFilter>(() => new NameFilter(JsonConvert.DeserializeObject<IEnumerable<Name>>(
                File.ReadAllText("Data/Types/Name/newData.json"))));

        public static Lazy<StringFilter> UserNames
            => new Lazy<StringFilter>(() => new StringFilter(File.ReadAllLines("Data/Types/Name/usernames.txt")));
    }
}