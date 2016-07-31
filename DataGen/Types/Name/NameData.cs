using System;
using System.Collections.Generic;

namespace DataGen.Types.Name {
    public class NameData : FileBasedData<NameRepository, NameFilter> {
        public NameData(string path) : base(path, enumerable => new NameFilter(enumerable)) {
        }
    }
}