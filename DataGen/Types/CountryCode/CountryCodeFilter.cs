using System;
using System.Collections.Generic;
using System.Linq;

namespace DataGen.Types.CountryCode {
    public class CountryCodeFilter : Filter<PhoneNumberGenerator> {
        public CountryCodeFilter(IEnumerable<PhoneNumberGenerator> enumerable) : base(enumerable) {
        }

        public CountryCodeFilter ByCountry(params string[] args)
            => new CountryCodeFilter(this.Where(c => args.Contains(c.Name)));

    }
}