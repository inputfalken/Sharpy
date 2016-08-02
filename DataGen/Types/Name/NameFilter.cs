using System;
using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types.Name {
    public class NameFilter : Filter<Name> {
        public NameFilter(IEnumerable<Name> result) : base(result) {
        }

        public NameFilter FilterBy(FilterType filterType, params string[] args) {
            switch (filterType) {
                case FilterType.Country:
                    return new NameFilter(args.SelectMany(country => Result.Where(name => name.Country.Equals(country))));
                case FilterType.Region:
                    return new NameFilter(args.SelectMany(region => Result.Where(name => name.Region.Equals(region))));
                case FilterType.Female:
                    return new NameFilter(Result.Where(name => name.NameType == NameType.Female));
                case FilterType.Lastname:
                    return new NameFilter(Result.Where(name => name.NameType == NameType.LastName));
                case FilterType.Male:
                    return new NameFilter(Result.Where(name => name.NameType == NameType.Male));
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterType), filterType, null);
            }
        }
    }
}