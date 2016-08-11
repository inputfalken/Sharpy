using System;
using System.Collections.Generic;

namespace DataGen.Types.CountryCode {
    public class CountryCodeFilter : Filter<CountryCode, CountryCodeArgs> {
        public CountryCodeFilter(IEnumerable<CountryCode> enumerable) : base(enumerable) {
        }

        public override Filter<CountryCode, CountryCodeArgs> FilterBy(CountryCodeArgs tArg, params string[] args) {
            throw new NotImplementedException();
        }
    }

    public enum CountryCodeArgs {
    }
}