using System;
using System.Collections.Generic;
using System.Linq;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    public class SwePostalCodeRandomizer : IPostalCodeProvider {
        public readonly Random Rnd;

        public SwePostalCodeRandomizer(Random rnd) => Rnd = rnd;

        private readonly Dictionary<string, string[]> _dictionary = new Dictionary<string, string[]>();

        public string PostalCode(string county) {
            if (string.IsNullOrEmpty(county))
                throw new ArgumentNullException($"{nameof(county)} can not be null or empty.");
            if (_dictionary.ContainsKey(county)) return _dictionary[county].RandomItem(Rnd);
            var strings = Data.GetPostalcodes
                .Where(p => p.Country == county)
                .Select(p => p.Postalcode)
                .ToArray();
            if (strings.Any()) _dictionary.Add(county, strings);
            else throw new ArgumentException($"Could not find any postal code with argument '{county}'.");
            return _dictionary[county].RandomItem(Rnd);
        }

        public string PostalCode() => Data.GetPostalcodes.RandomItem(Rnd).Postalcode;
    }
}