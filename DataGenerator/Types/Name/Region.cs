using System.Collections.Generic;

namespace DataGenerator.Types.Name
{
    internal class Region
    {
        public Region(List<Country> countries, string name) {
            Countries = countries;
            Name = name;
        }

        public string Name { get; }
        public List<Country> Countries { get; }
    }
}