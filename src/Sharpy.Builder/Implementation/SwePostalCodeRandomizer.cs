using System;
using System.Collections.Generic;
using System.Linq;
using Sharpy.Builder.Implementation.ExtensionMethods;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder.Implementation {
    /// <summary>
    ///     Randomizes <see cref="string" /> elements as Swedish postal codes.
    /// </summary>
    public sealed class SwePostalCodeRandomizer : IPostalCodeProvider {
        private readonly Dictionary<string, string[]> _dictionary = new Dictionary<string, string[]>();
        private readonly Random _rnd;

        /// <summary>
        ///     <para>
        ///         The <see cref="Random" /> for randomizing.
        ///     </para>
        /// </summary>
        public SwePostalCodeRandomizer(Random rnd) => _rnd = rnd;

        /// <inheritdoc />
        public string PostalCode(string county) {
            if (string.IsNullOrEmpty(county))
                throw new ArgumentNullException($"{nameof(county)} can not be null or empty.");
            if (_dictionary.ContainsKey(county)) return _dictionary[county].RandomItem(_rnd);
            var strings = Data.GetPostalCodes
                .Where(p => p.County.Equals(county, StringComparison.CurrentCultureIgnoreCase))
                .Select(p => p.Postalcode)
                .ToArray();
            if (strings.Any()) _dictionary.Add(county, strings);
            else throw new ArgumentException($"Could not find any postal code with argument '{county}'.");
            return _dictionary[county].RandomItem(_rnd);
        }

        /// <inheritdoc />
        public string PostalCode() => Data.GetPostalCodes.RandomItem(_rnd).Postalcode;
    }
}