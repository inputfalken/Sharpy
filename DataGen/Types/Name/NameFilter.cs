using System;
using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types.Name {
    public class NameFilter : Filter<Name> {
        public NameFilter(IEnumerable<Name> result) : base(result) {
        }

        public NameFilter FilterBy(FilterArg filterArg, params string[] args) {
            switch (filterArg) {
                case FilterArg.Country:
                    return new NameFilter(args.SelectMany(country => Result.Where(name => name.Country.Equals(country))));
                case FilterArg.Region:
                    return new NameFilter(args.SelectMany(region => Result.Where(name => name.Region.Equals(region))));
                case FilterArg.Female:
                    return new NameFilter(Result.Where(name => name.Type == 1));
                case FilterArg.Lastname:
                    return new NameFilter(Result.Where(name => name.Type == 3));
                case FilterArg.Male:
                    return new NameFilter(Result.Where(name => name.Type == 2));
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterArg), filterArg, null);
            }
        }
    }
}