using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types.Name {
    public class NameFilter : Filter<Name> {
        public NameFilter(IEnumerable<Name> result) : base(result) {
        }

        private IEnumerable<Name> ByRegion(params string[] regions)
            => regions.SelectMany(region => Result.Where(name => name.Region.Equals(region)));

        public NameFilter ByRegions(params string[] regions)
            => new NameFilter(ByRegion(regions));

        private IEnumerable<Name> ByCountry(params string[] countries)
            => countries.SelectMany(country => Result.Where(name => name.Country.Equals(country)));

        public NameFilter ByCountries(params string[] countries)
            => new NameFilter(ByCountry(countries));
    }
}