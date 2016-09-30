using System;
using Newtonsoft.Json;
using Sharpy.Enums;

namespace Sharpy.Types.CountryCode {
    /// <summary>
    ///     Contains Country name a Country Code
    /// </summary>
    internal sealed class CountryCode {
        [JsonConstructor]
        internal CountryCode(string name, string code) {
            Code = code;
            Country country;
            if (!Enum.TryParse(name, out country)) return;
            Name = country;
            IsParsed = true;
        }

        internal bool IsParsed { get; }

        /// <summary>
        ///     Name of the Country
        /// </summary>
        public Country Name { get; }

        /// <summary>
        ///     The country code for the Country
        /// </summary>
        public string Code { get; }
    }
}