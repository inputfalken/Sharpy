using System;
using DataGen.Types.Name;

namespace DataGen.Types {
    public static class Factory {
        public static FileBasedData<NameRepository, NameFilter> NameData
            => new FileBasedData<NameRepository, NameFilter>("Data/Types/Name/data.json", enumerable => new NameFilter(enumerable));

    }
}