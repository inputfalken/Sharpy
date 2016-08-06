﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataGen.Types;

namespace DataGen.Types.NameCollection {
    public sealed class UserNameFilter : Filter<string, FilterArgs> {
        public UserNameFilter(IEnumerable<string> enumerable) : base(enumerable) {
        }

        public override Filter<string, FilterArgs> FilterBy(FilterArgs filterArgs, params string[] args) {
            switch (filterArgs) {
                case FilterArgs.StartsWith:
                    return new UserNameFilter(args.SelectMany(s
                        => this.Where(username => username.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) == 0)));
                case FilterArgs.Contains:
                    return new UserNameFilter(args.SelectMany(s
                        => this.Where(username => username.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) != -1)));
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterArgs), filterArgs, null);
            }
        }
    }

    public enum FilterArgs {
        StartsWith,
        Contains,
    }
}