using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataGen.Types.Name;
using Newtonsoft.Json;

namespace DataGen.Types {
    public static class Factory {
        public static Data<NameRepository, NameFilter> NameDatas
            =>
                new Data<NameRepository, NameFilter>(
                    new NameFilter(JsonConvert.DeserializeObject<IEnumerable<NameRepository>>(
                        File.ReadAllText("Data/Types/Name/data.json"))));
    }
}