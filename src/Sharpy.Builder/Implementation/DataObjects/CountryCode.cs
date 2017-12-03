using System;
using Newtonsoft.Json;
using Sharpy.Builder.Enums;
using static System.Enum;

namespace Sharpy.Builder.Implementation.DataObjects {
    /// <summary>
    ///     Contains Country name a Country Code
    /// </summary>
    internal sealed class CountryCode {
        [JsonConstructor]
        internal CountryCode(string name, string code) {
            Code = code;
            if (TryParse(name, out Origin origin)) throw new ArgumentNullException(nameof(name));
            Name = origin;
        }

        /// <summary>
        ///     Name of the Country
        /// </summary>
        public Origin Name { get; }

        /// <summary>
        ///     The country code for the Country
        /// </summary>
        public string Code { get; }
    }
}