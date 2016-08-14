using System;
using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types.String {
    public sealed class StringFilter : Filter<string> {
        public StringFilter(IEnumerable<string> enumerable) : base(enumerable) {
        }


        public StringFilter StartsWith(params string[] args) => new StringFilter(args.SelectMany(s
            => this.Where(username => username.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) == 0)));

        public StringFilter Contains(params string[] args) => new StringFilter(args.SelectMany(s
            => this.Where(username => username.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) != -1)));
    }
}