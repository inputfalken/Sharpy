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
                case FilterArg.MaleFirstName:
                    return new NameFilter(this.Where(name => name.Type == 2));
                case FilterArg.FemaleFirstName:
                    return new NameFilter(this.Where(name => name.Type == 1));
                case FilterArg.Lastname:
                    return new NameFilter(this.Where(name => name.Type == 3));
                case FilterArg.Country:
                    return new NameFilter(this.Where(name => args.Contains(name.Country)));
                case FilterArg.Region:
                    return new NameFilter(this.Where(name => args.Contains(name.Region)));
                case FilterArg.MixedFirstNames:
                    return new NameFilter(this.Where(name => name.Type == 1 || name.Type == 2));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterArg), filterArg, null);
            }
        }
    }

    public enum FilterArg {
        FemaleFirstName = 1,
        MaleFirstName = 2,
        Lastname = 3,
        MixedFirstNames,
        Country,
        Region
    }
}