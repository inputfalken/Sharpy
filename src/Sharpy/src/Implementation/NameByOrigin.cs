﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Sharpy.Enums;
using Sharpy.Implementation.DataObjects;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    /// <summary>
    ///     <para>
    ///         Randomizes common names by <see cref="Enums.Origin" />.
    ///     </para>
    /// </summary>
    public class NameByOrigin : INameProvider {
        private static readonly ISet<Origin> Regions = new HashSet<Origin> {
            Enums.Origin.Europe,
            Enums.Origin.NorthAmerica,
            Enums.Origin.CentralAmerica,
            Enums.Origin.SouthAmerica
        };

        private readonly ISet<Origin> _origins;
        private readonly Random _random;
        private readonly ISet<Origin> _selectedCountries = new HashSet<Origin>();
        private readonly ISet<Origin> _selectedRegions = new HashSet<Origin>();

        private NameByOrigin(Random random) {
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
        ///         Randomizes common names by <see cref="T:Sharpy.Enums.Origin" /> using <see cref="T:System.Random" />.
        ///     </para>
        /// </summary>
        /// <param name="origins">
        ///     The name origins.
        /// </param>
        public NameByOrigin(params Origin[] origins) : this(new Random(), origins) { }

        internal static IEnumerable<Name> Names => LazyNames.Value;

        // This code block is probably ran everytime Names is requested!
        private static Lazy<IEnumerable<Name>> LazyNames {
            get {
                return new Lazy<IEnumerable<Name>>(() => {
                    var assembly = Assembly.Load("Sharpy");
                    var resourceStream = assembly.GetManifestResourceStream("Sharpy.Data.NamesByOrigin.json");
                    using (var reader = new StreamReader(resourceStream, Encoding.UTF8)) {
                        return JsonConvert.DeserializeObject<IEnumerable<Name>>(reader.ReadToEnd());
                    }
                });
            }
        }

        private Dictionary<NameType, IReadOnlyList<string>> Dictionary { get; } =
            new Dictionary<NameType, IReadOnlyList<string>>();

        /// <inheritdoc cref="INameProvider.FirstName(Gender)" />
        public string FirstName(Gender gender) {
            return Name(
                gender == Gender.Male ? NameType.MaleFirst : NameType.FemaleFirst);
        }

        /// <inheritdoc cref="INameProvider.FirstName()" />
        public string FirstName() {
            return Name(_random.Next(2) == 0 ? NameType.FemaleFirst : NameType.MaleFirst);
        }

        /// <inheritdoc cref="INameProvider.LastName()" />
        public string LastName() {
            return Name(NameType.Last);
        }

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
                    .Select(n => n.Data)
                : Names.Select(n => n.Data);
        }

        /// <summary>
        ///     <para>Returns a name based on <see cref="NameType" />.</para>
        /// </summary>
        /// <param name="arg"></param>
        private string Name(NameType arg) {
            if (Dictionary.ContainsKey(arg)) return Dictionary[arg].RandomItem(_random);
            var strings = Origin(Names.Where(n => n.Type == arg)).Select(n => n.Data).ToArray();
            if (strings.Any()) Dictionary.Add(arg, strings);
            else throw new Exception("Can't obtain strings with this configuration");
            return Dictionary[arg].RandomItem(_random);
        }

        private IEnumerable<Name> Origin(IEnumerable<Name> names) {
            return _origins != null && _origins.Any()
                ? names.Where(
                    name => _selectedCountries.Contains(name.Country) | _selectedRegions.Contains(name.Region))
                : names;
        }
    }
}