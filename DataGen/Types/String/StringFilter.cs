using System;
using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types.String {
    public sealed class StringFilter : Filter<string> {
        public StringFilter(IEnumerable<string> enumerable) : base(enumerable) {
        }


        public StringFilter StartsWith(params string[] args)
            => new StringFilter(args.SelectMany(arg => this.Where(s => IndexOf(s, arg) == 0)));

        public StringFilter DoesNotStartWith(params string[] args)
            => new StringFilter(args.SelectMany(arg => this.Where(s => IndexOf(s, arg) != 0)));

        public StringFilter Contains(params string[] args)
            => args.Length == 1
                ? new StringFilter(this.Where(s => IndexOf(s, args[0]) >= 0))
                : new StringFilter(RemoveRepeatedData(args.SelectMany(arg => this.Where(s => IndexOf(s, arg) >= 0))));

        public StringFilter DoesNotContain(params string[] args)
            => new StringFilter(args.SelectMany(arg => this.Where(s => IndexOf(s, arg) < 0)));

        public StringFilter ByLength(int length) => new StringFilter(this.Where(s => s.Length == length));

        private static int IndexOf(string str, string substring)
            => str.IndexOf(substring, StringComparison.CurrentCultureIgnoreCase);
    }
}