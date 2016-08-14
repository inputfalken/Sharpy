using System;
using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types.String {
    public sealed class StringFilter : Filter<string> {
        public StringFilter(IEnumerable<string> enumerable) : base(enumerable) {
        }


        public StringFilter StartsWith(params string[] args) => new StringFilter(args.SelectMany(s
            => this.Where(username => username.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) == 0)));

        public StringFilter DoesNotStartWith(params string[] args) => new StringFilter(args.SelectMany(s
            => this.Where(username => username.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) != 0)));

        //Todo Refactor
        public StringFilter Contains(params string[] args) {
            if (args.Length == 1)
                return new StringFilter(this.Where(username =>
                        username.IndexOf(args[0], StringComparison.CurrentCultureIgnoreCase) >= 0));
            return new StringFilter(args.SelectMany(s
                    => this.Where(username => username.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) >= 0)))
                .RemoveRepeatedData()
                .ToStringFilter();
        }

        public StringFilter DoesNotContain(params string[] args) => new StringFilter(args.SelectMany(s
            => this.Where(username => username.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) < 0)));

        public StringFilter ByLength(int length) => new StringFilter(this.Where(s => s.Length == length));
    }
}