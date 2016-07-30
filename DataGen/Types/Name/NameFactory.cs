using System;
using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types.Name {
    public class NameFactory : FileBasedData<NameRepository> {
        public NameFactory() : base("Data/Types/Name/data.json") {
        }

        public override IEnumerable<NameRepository> Collection(Func<NameRepository, IEnumerable<NameRepository>> func) {
            return Filter.RepeatedData(Source.Select(func)
                .Where(enumerable => enumerable != null)
                .SelectMany(enumerable => enumerable));
        }
    }
}