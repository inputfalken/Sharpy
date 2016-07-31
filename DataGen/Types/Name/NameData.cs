using System;
using System.Collections.Generic;

namespace DataGen.Types.Name {
    public class NameData : FileBasedData<NameRepository, NameFilter> {
        public NameData() : base("Data/Types/Name/data.json") {
            Filter = new NameFilter(Source);
        }

        public override IEnumerable<NameRepository> Collection(Func<NameFilter, IEnumerable<NameRepository>> func)
            => func(Filter);


        public override IEnumerable<NameRepository> Collection()
            => Source;
    }
}