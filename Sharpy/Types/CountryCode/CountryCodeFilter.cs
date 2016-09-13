using System.Collections.Generic;
using System.Linq;
using Sharpy.Types.Enums;

namespace Sharpy.Types.CountryCode {
    /// <summary>
    ///     This class is used for parsing country codes from a json file
    /// </summary>
    public class CountryCodeFilter : Filter<PhoneNumberGenerator> {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumerable"></param>
        public CountryCodeFilter(IEnumerable<PhoneNumberGenerator> enumerable) : base(enumerable) {
        }

        /// <summary>
        ///     This lets you select a country code based on country title
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public CountryCodeFilter ByCountry(params Country[] args)
            => new CountryCodeFilter(this.Where(c => args.Contains(c.Name)));
    }
}