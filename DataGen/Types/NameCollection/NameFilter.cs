using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DataGen.Types.NameCollection {
    public sealed class NameFilter : Filter<Name, FilterArg> {
        public NameFilter(IEnumerable<Name> enumerable) : base(enumerable) {
        }

        protected override Filter<Name, FilterArg> Where(Func<Name, bool> predicate) {
            var collection = new Collection<Name>();
            var enumerator = GetEnumerator();
            while (enumerator.MoveNext())
                if (predicate(enumerator.Current))
                    collection.Add(enumerator.Current);
            enumerator.Dispose();
            return new NameFilter(collection);
        }

        public override Filter<Name, FilterArg> FilterBy(FilterArg filterArg, params string[] args) {
            switch (filterArg) {
                case FilterArg.Male:
                    return Where(name => name.Type == 1);
                    break;
                case FilterArg.Female:
                    return Where(name => name.Type == 2);
                    break;
                case FilterArg.Lastname:
                    return Where(name => name.Type == 3);
                    break;
                case FilterArg.Country:
                    return Where(name => args.Contains(name.Country));
                    break;
                case FilterArg.Region:
                    return Where(name => args.Contains(name.Region));
                    break;
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