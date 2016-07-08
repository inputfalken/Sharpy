using System.Collections.Generic;

namespace DataGenerator.Types.Name
{
    internal class NameData
    {
        public NameData(IEnumerable<Region> regions) {
            Regions = regions;
        }

        public IEnumerable<Region> Regions { get; }
    }
}