using System;
using System.Collections.Generic;
using System.Linq;
using DataGen.Types;

namespace DataGen.Types.NameCollection {
    public sealed class StringFilter : Filter<string, StringArg> {
        public StringFilter(IEnumerable<string> enumerable) : base(enumerable) {
        }

        public override Filter<string, StringArg> FilterBy(StringArg stringArg, params string[] args) {
            switch (stringArg) {
                case StringArg.StartsWith:
                    return new StringFilter(args.SelectMany(s
                        => this.Where(username => username.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) == 0)));
                case StringArg.Contains:
                    return new StringFilter(args.SelectMany(s
                        => this.Where(username => username.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) != -1)));
                default:
                    throw new ArgumentOutOfRangeException(nameof(stringArg), stringArg, null);
            }
        }
    }

    public enum StringArg {
        StartsWith,
        Contains,
    }
}