using System;
using System.Collections.Generic;
using System.Linq;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    /// <summary>
    /// Randomizes Swedish postal codes.
    /// </summary>
    public class SwePostalCodeRandomizer : IPostalCodeProvider {
        private readonly Random _rnd;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rnd"></param>
        public SwePostalCodeRandomizer(Random rnd) => _rnd = rnd;

        private readonly Dictionary<string, string[]> _dictionary = new Dictionary<string, string[]>();

        /// <inheritdoc />
        public string PostalCode(string county) {
            if (string.IsNullOrEmpty(county))
                throw new ArgumentNullException($"{nameof(county)} can not be null or empty.");
            if (_dictionary.ContainsKey(county)) return _dictionary[county].RandomItem(_rnd);
            var strings = Data.GetPostalcodes
                .Where(p => p.County.Equals(county, StringComparison.CurrentCultureIgnoreCase))
                .Select(p => p.Postalcode)
                .ToArray();
            if (strings.Any()) _dictionary.Add(county, strings);
            else throw new ArgumentException($"Could not find any postal code with argument '{county}'.");
            return _dictionary[county].RandomItem(_rnd);
        }

        /// <inheritdoc />
        public string PostalCode() => Data.GetPostalcodes.RandomItem(_rnd).Postalcode;
    }
}