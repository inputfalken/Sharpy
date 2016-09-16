using System;
using Sharpy.Types.Enums;

namespace Sharpy.Types.CountryCode {
    /// <summary>
    ///     Contains Country name a Country Code
    /// </summary>
    public class CountryCode {
        internal bool IsParsed { get; }

        /// <summary>
        ///     Name of the Country
        /// </summary>
        public Country Name { get; }
        /// <summary>
        ///     The country code for the Country
        /// </summary>
        public string Code { get; }

        /// <summary>
        ///     gets populated by the JSON deserializer
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        public CountryCode(string name, string code) {
            Code = code;
            Country country;
            if (!Enum.TryParse(name, out country)) return;
            Name = country;
            IsParsed = true;
        }
    }
}