using System;
using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types.Name {
    public class NameFilter : Filter<Name> {
        public NameFilter(IEnumerable<Name> result) : base(result) {
        }

        public NameFilter FilterBy(Types types, params string[] args) {
            switch (types) {
                case Types.Country:
                    return new NameFilter(args.SelectMany(country => Result.Where(name => name.Country.Equals(country))));
                case Types.Region:
                    return new NameFilter(args.SelectMany(region => Result.Where(name => name.Region.Equals(region))));
                case Types.Female:
                    return new NameFilter(Result.Where(name => name.NameType == NameType.Female));
                case Types.Lastname:
                    return new NameFilter(Result.Where(name => name.NameType == NameType.LastName));
                case Types.Male:
                    return new NameFilter(Result.Where(name => name.NameType == NameType.Male));
                default:
                    throw new ArgumentOutOfRangeException(nameof(types), types, null);
            }
        }


        public enum Types {
            Country,
            Region,
            Female,
            Lastname,
            Male
        }
    }
}