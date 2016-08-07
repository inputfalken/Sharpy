using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DataGen.Types.NameCollection {
    public sealed class NameFilter : Filter<Name, NameArg> {
        public NameFilter(IEnumerable<Name> enumerable) : base(enumerable) {
        }


        public override Filter<Name, NameArg> FilterBy(NameArg nameArg, params string[] args) {
            switch (nameArg) {
                case NameArg.FemaleFirstName:
                    return new NameFilter(this.Where(name => name.Type == 1));
                case NameArg.MaleFirstName:
                    return new NameFilter(this.Where(name => name.Type == 2));
                case NameArg.Lastname:
                    return new NameFilter(this.Where(name => name.Type == 3));
                case NameArg.Country:
                    return new NameFilter(this.Where(name => args.Contains(name.Country)));
                case NameArg.Region:
                    return new NameFilter(this.Where(name => args.Contains(name.Region)));
                case NameArg.MixedFirstNames:
                    return new NameFilter(this.Where(name => name.Type == 1 || name.Type == 2));
                default:
                    throw new ArgumentOutOfRangeException(nameof(nameArg), nameArg, null);
            }
        }
    }

    public enum NameArg {
        FemaleFirstName = 1,
        MaleFirstName = 2,
        Lastname = 3,
        MixedFirstNames,
        Country,
        Region
    }
}