using System;
using DataGen.Types.Name;

namespace DataGen.Types {
    public static class Factory {
        public static FileBasedData<NameRepository, NameFilter> NameData
            => new FileBasedData<NameRepository, NameFilter>("", enumerable => new NameFilter(enumerable));

    }
}