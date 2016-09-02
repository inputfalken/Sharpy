using System;
using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types.String {
    public sealed class StringFilter : Filter<string>, IStringFilter<StringFilter> {
        public StringFilter(IEnumerable<string> enumerable) : base(enumerable) {
        }

        public StringFilter DoesNotStartWith(string arg) => new StringFilter(this.Where(s => IndexOf(s, arg) != 0));
        public StringFilter DoesNotContain(string arg) => new StringFilter(this.Where(s => !s.Contains(arg)));

        public StringFilter StartsWith(params string[] args)
            => args.Length == 1
                ? new StringFilter(this.Where(s => IndexOf(s, args[0]) == 0))
                : new StringFilter(this.Where(s => args.Any(arg => IndexOf(s, arg) == 0)));

        public StringFilter Contains(params string[] args)
            => args.Length == 1
                ? new StringFilter(this.Where(s => s.Contains(args[0])))
                : new StringFilter(this.Where(s => args.Any(s.Contains)));


        public StringFilter ByLength(int length) {
            if (length < 1) throw new ArgumentOutOfRangeException($"{nameof(length)} is below 1");
            return new StringFilter(this.Where(s => s.Length == length));
        }

    }
}