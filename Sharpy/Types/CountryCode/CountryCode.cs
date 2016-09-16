using System;
using Newtonsoft.Json;
using Sharpy.Types.Enums;

namespace Sharpy.Types.CountryCode {
    public class CountryCode {
        public bool IsParsed { get; }

        public Country Name { get; }
        public string Code { get; }

        [JsonConstructor]
        internal CountryCode(string name, string code) {
            Code = code;
            Country country;
            if (!Enum.TryParse(name, out country)) return;
            Name = country;
            IsParsed = true;
        }
    }
}