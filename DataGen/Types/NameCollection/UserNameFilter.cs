﻿using System;
using System.Collections.Generic;

namespace DataGen.Types.NameCollection {
    public sealed class UserNameFilter : Filter<string, UserNameFilter> {
        public UserNameFilter(IEnumerable<string> enumerable) : base(enumerable) {
        }


        public override Filter<string, UserNameFilter> FilterBy(UserNameFilter tenum, params string[] args) {
            throw new NotImplementedException();
        }
    }

    internal enum UsernameFilter {
    }
}