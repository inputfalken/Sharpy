using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types.Name {
    public class NameFilter : Filter<NameRepository>, IEqualityComparer<string> {
        public NameFilter(IEnumerable<NameRepository> result) : base(result) {
        }

        private IEnumerable<NameRepository> ByRegion(params string[] regions)
            => regions.SelectMany(region => Result.Where(repository => Equals(region, repository._origin.Region)));

        public NameFilter ByRegions(params string[] regions)
            => new NameFilter(ByRegion(regions));

        private IEnumerable<NameRepository> ByCountry(params string[] countries)
            => countries.SelectMany(country => Result.Where(repository => Equals(country, repository._origin.Country)));

        public NameFilter ByCountries(params string[] countries)
            => new NameFilter(ByCountry(countries));

        public bool Equals(string x, string y) => x == y;

        public int GetHashCode(string obj) => GetHashCode();
    }
}