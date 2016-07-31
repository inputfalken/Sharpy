using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types.Name {
    public class NameFilter : Filter {
        private IEnumerable<NameRepository> NameRepositories { get; }

        public NameFilter(IEnumerable<NameRepository> nameRepositories) {
            NameRepositories = nameRepositories;
        }

        public IEnumerable<NameRepository> ByRegion(params string[] regions)
            => SelectByString(regions);

        public IEnumerable<NameRepository> ByCountry(params string[] countries)
            => SelectByString(countries);

        private IEnumerable<NameRepository> SelectByString(IEnumerable<string> querys)
            => querys.SelectMany(query => NameRepositories.Where(repository => repository._origin.Country == query));
    }
}