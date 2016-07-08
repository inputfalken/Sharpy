using System.Collections.Generic;

namespace DataGenerator.Types.Name
{
    internal class NameData
    {
        public NameData(List<Region> regions) {
            Regions = regions;
        }

        public List<Region> Regions { get; }
    }
}