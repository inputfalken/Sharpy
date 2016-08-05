using System;
using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types.NameCollection {
    public class NameFilter : Filter<Name> {
        public NameFilter(IEnumerable<Name> result) : base(result) {
        }

        public NameFilter FilterBy(FilterArg filterArg, params string[] args) {
            switch (filterArg) {
                case FilterArg.Male:
                    Predicate(name => name.Type == 1);
                    break;
                case FilterArg.Female:
                    Predicate(name => name.Type == 2);
                    break;
                case FilterArg.Lastname:
                    Predicate(name => name.Type == 3);
                    break;
                case FilterArg.Country:
                    Predicate(name => args.Contains(name.Country));
                    break;
                case FilterArg.Region:
                    Predicate(name => args.Contains(name.Region));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterArg), filterArg, null);
            }
            return new NameFilter(Names);
        }

        private List<Name> Names { get; } = new List<Name>();

        private void Predicate(Func<Name, bool> predicate) {
            var enumerator = GetEnumerator();
            if (predicate(enumerator.Current)) {
                while (enumerator.MoveNext())
                    Names.Add(enumerator.Current);
                enumerator.Dispose();
            }
        }
    }
}