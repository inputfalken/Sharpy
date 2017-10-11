using System;
using Newtonsoft.Json;
using Sharpy.Builder.Enums;

namespace Sharpy.Builder.Implementation.DataObjects {
    /// <summary>
    ///     Contains Country name a Country Code
    /// </summary>
    internal sealed class CountryCode {
        [JsonConstructor]
        internal CountryCode(string name, string code) {
            Code = code;
            Origin origin;
            if (!Enum.TryParse(name, out origin)) return;
            Name = origin;
            IsParsed = true;
        }

        internal bool IsParsed { get; }

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