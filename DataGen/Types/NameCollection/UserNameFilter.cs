using System;
using System.Collections.Generic;

namespace DataGen.Types.NameCollection {
    public class UserNameFilter : Filter<string, UserNameFilter> {
        public UserNameFilter(IEnumerable<string> enumerable) : base(enumerable) {
        }

        protected override Filter<string, UserNameFilter> Where(Func<string, bool> predicate) {
            throw new NotImplementedException();
        }

        public override Filter<string, UserNameFilter> FilterBy(UserNameFilter tenum, params string[] args) {
            throw new NotImplementedException();
        }
    }

    internal enum UsernameFilter {
    }
}