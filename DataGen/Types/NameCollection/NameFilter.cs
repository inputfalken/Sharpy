using System;
using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types.NameCollection {
    public class NameFilter : Filter<Name, FilterArg> {
        public NameFilter(IEnumerable<Name> enumerable) : base(enumerable) {
        }

        public override Filter<Name, FilterArg> FilterBy(FilterArg filterArg, params string[] args) {
            switch (filterArg) {
                case FilterArg.Male:
                    PopulateCollectionWhere(name => name.Type == 1);
                    break;
                case FilterArg.Female:
                    PopulateCollectionWhere(name => name.Type == 2);
                    break;
                case FilterArg.Lastname:
                    PopulateCollectionWhere(name => name.Type == 3);
                    break;
                case FilterArg.Country:
                    PopulateCollectionWhere(name => args.Contains(name.Country));
                    break;
                case FilterArg.Region:
                    PopulateCollectionWhere(name => args.Contains(name.Region));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterArg), filterArg, null);
            }
            return new NameFilter(Collection);
        }

    }
}