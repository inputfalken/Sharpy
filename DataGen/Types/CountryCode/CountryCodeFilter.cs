using System;
using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types.CountryCode {
    public class CountryCodeFilter : Filter<CountryCode, CountryCodeArgs> {
        public CountryCodeFilter(IEnumerable<CountryCode> enumerable) : base(enumerable) {
        }

        public override Filter<CountryCode, CountryCodeArgs> FilterBy(CountryCodeArgs tArg, params string[] args) {
            switch (tArg) {
                case CountryCodeArgs.ByCountry:
                    return new CountryCodeFilter(this.Where(c => args.Contains(c.Name)));
                default:
                    throw new ArgumentOutOfRangeException(nameof(tArg), tArg, null);
            }
        }
    }

    public enum CountryCodeArgs {
        ByCountry
    }
}