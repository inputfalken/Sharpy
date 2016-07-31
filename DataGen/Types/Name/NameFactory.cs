using System;
using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types.Name {
    public class NameFactory : FileBasedData<NameRepository, NameFilter> {
        public NameFactory() : base("Data/Types/Name/data.json") {
            Filter = new NameFilter(Source);
        }

        public override IEnumerable<NameRepository> Collection(Func<NameFilter, IEnumerable<NameRepository>> func) {
            return func(Filter);
        }

        public override IEnumerable<NameRepository> Collection() {
            return null;
        }
    }

    public class Filter {
        public static IEnumerable<T> RepeatedData<T>(IEnumerable<T> enumerable)
            => enumerable.GroupBy(s => s)
                .Where(g => g.Any())
                .Select(grouping => grouping.Key);
    }

    public class NameFilter : Filter {
        private IEnumerable<NameRepository> NameRepositories { get; }

        public NameFilter(IEnumerable<NameRepository> nameRepositories) {
            NameRepositories = nameRepositories;
        }

        public IEnumerable<NameRepository> ByRegion(params string[] regions)
            => regions.SelectMany(region => NameRepositories.Where(repository => repository._origin.Region == region));

        public IEnumerable<NameRepository> ByCountry(params string[] countries)
            =>
                countries.SelectMany(
                    country => NameRepositories.Where(repository => repository._origin.Country == country));
    }
}