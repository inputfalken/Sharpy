using System;
using System.Collections.Generic;
using System.Linq;
using Sharpy.Builder.Enums;
using Sharpy.Builder.Implementation.DataObjects;
using Sharpy.Builder.Implementation.ExtensionMethods;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder.Implementation {
    /// <summary>
    ///     <para>
    ///         Randomizes <see cref="string" /> elements representing names by using <see cref="Random" />.
    ///     </para>
    /// </summary>
    public sealed class NameByOrigin : INameProvider {
        private static readonly ISet<Origin> Regions = new HashSet<Origin> {
            Enums.Origin.Europe,
            Enums.Origin.NorthAmerica,
            Enums.Origin.CentralAmerica,
            Enums.Origin.SouthAmerica
        };

        private readonly IDictionary<NameType, IReadOnlyList<string>> _dictionary;

        private readonly ISet<Origin> _origins;
        private readonly Random _random;
        private readonly ISet<Origin> _selectedCountries = new HashSet<Origin>();
        private readonly ISet<Origin> _selectedRegions = new HashSet<Origin>();

        private NameByOrigin(Random random) {
            _dictionary = new Dictionary<NameType, IReadOnlyList<string>>();
            _random = random;
        }

        /// <summary>
        ///     <para>
        ///         Randomizes common names by <see cref="Enums.Origin" /> using argument <paramref name="random" />.
        ///     </para>
        /// </summary>
        public NameByOrigin(Random random, params Origin[] origins) : this(random) {
            _origins = new HashSet<Origin>(origins);
            foreach (var origin in _origins)
                if (Regions.Contains(origin)) _selectedRegions.Add(origin);
                else _selectedCountries.Add(origin);
        }

        /// <inheritdoc />
        /// <summary>
        ///     <para>
        ///         Randomizes common names by <see cref="T:Sharpy.Builder.Enums.Origin" /> using <see cref="T:System.Random" />.
        ///     </para>
        /// </summary>
        /// <param name="origins">
        ///     The name origins.
        /// </param>
        public NameByOrigin(params Origin[] origins) : this(new Random(), origins) { }

        private static IEnumerable<NameModel> Names => Data.GetNames;

        /// <inheritdoc />
        public string FirstName(Gender gender) => Name(
            gender == Gender.Male ? NameType.MaleFirst : NameType.FemaleFirst);

        /// <inheritdoc />
        public string FirstName() => Name(_random.Next(2) == 0 ? NameType.FemaleFirst : NameType.MaleFirst);

        /// <inheritdoc />
        public string LastName() => Name(NameType.Last);

        /// <summary>
        ///     <para>
        ///         Returns the collection used for randomizing names.
        ///         No argument will get every name.
        ///         With argument/arguments filters will be used for the <see cref="Enums.Origin" />.
        ///     </para>
        /// </summary>
        /// <param name="origins">
        ///     The name origins.
        /// </param>
        /// <returns>
        ///     A <see cref="IEnumerable{T}" /> with names from the <paramref name="origins" /> used.
        /// </returns>
        public static IEnumerable<string> GetCollection(params Origin[] origins) {
            return origins.Any()
                ? Names
                    .Where(n => origins.Contains(n.Country) || origins.Contains(n.Region))
                    .Select(n => n.Name)
                : Names.Select(n => n.Name);
        }

        /// <summary>
        ///     <para>Returns a name based on <see cref="NameType" />.</para>
        /// </summary>
        /// <param name="arg"></param>
        private string Name(NameType arg)
        {
            if (_dictionary.TryGetValue(arg, out var names)) return names.RandomItem(_random);
            var strings = Origin(Names.Where(n => n.Type == arg)).Select(n => n.Name).ToList();
            if (strings.Count > 0) _dictionary.Add(arg, strings);
            else throw new Exception("Can't obtain strings with this configuration");
            return _dictionary[arg].RandomItem(_random);
        }

        private IEnumerable<NameModel> Origin(IEnumerable<NameModel> names) {
            return _origins != null && _origins.Any()
                ? names.Where(
                    name => _selectedCountries.Contains(name.Country) | _selectedRegions.Contains(name.Region))
                : names;
        }
    }
}