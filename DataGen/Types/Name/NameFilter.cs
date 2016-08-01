using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types.Name {
    public class NameFilter : Filter<NameRepository> {
        public NameFilter(IEnumerable<NameRepository> result) : base(result) {
        }

        private IEnumerable<NameRepository> ByRegion(params string[] regions)
            => regions.SelectMany(region => Result.Where(repository => repository.Origin.Region.Equals(region)));

        public NameFilter ByRegions(params string[] regions)
            => new NameFilter(ByRegion(regions));

        private IEnumerable<NameRepository> ByCountry(params string[] countries)
            => countries.SelectMany(country => Result.Where(repository => repository.Origin.Country.Equals(country)));

        public NameFilter ByCountries(params string[] countries)
            => new NameFilter(ByCountry(countries));
    }
}