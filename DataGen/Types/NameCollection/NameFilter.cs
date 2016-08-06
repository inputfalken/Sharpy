using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DataGen.Types.NameCollection {
    public sealed class NameFilter : Filter<Name, FilterArg> {
        public NameFilter(IEnumerable<Name> enumerable) : base(enumerable) {
        }


        public override Filter<Name, FilterArg> FilterBy(FilterArg filterArg, params string[] args) {
            switch (filterArg) {
                case FilterArg.Male:
                    return new NameFilter(this.Where(name => name.Type == 1));
                case FilterArg.Female:
                    return new NameFilter(this.Where(name => name.Type == 2));
                case FilterArg.Lastname:
                    return new NameFilter(this.Where(name => name.Type == 3));
                case FilterArg.Country:
                    return new NameFilter(this.Where(name => args.Contains(name.Country)));
                case FilterArg.Region:
                    return new NameFilter(this.Where(name => args.Contains(name.Region)));
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterArg), filterArg, null);
            }
        }
    }

    public enum FilterArg {
        Female = 1,
        Male = 2,
        Lastname = 3,
        Country,
        Region
    }
}