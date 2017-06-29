using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Sharpy.Enums;
using Sharpy.Implementation.DataObjects;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;
using Sharpy.Properties;

namespace Sharpy.Implementation {
    /// <summary>
    ///     <para>Randomizes common names by origin.</para>
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
        ///     <para>Randomizes common names by origin using random/></para>
        /// </summary>
        public NameByOrigin(Random random, params Origin[] origins) : this(random) {
            _origins = new HashSet<Origin>(origins);
            foreach (var origin in _origins)
                if (Regions.Contains(origin)) _selectedRegions.Add(origin);
                else _selectedCountries.Add(origin);
        }

        /// <summary>
        ///     <para>Randomizes names based on origin.</para>
        /// </summary>
        /// <param name="origins"></param>
        public NameByOrigin(params Origin[] origins) : this(new Random(), origins) { }


        private static IEnumerable<Name> Names {
            get { return LazyNames.Value; }
        }


        private static Lazy<IEnumerable<Name>> LazyNames { get; } =
            new Lazy<IEnumerable<Name>>(() => JsonConvert.DeserializeObject<IEnumerable<Name>>(
                Encoding.UTF8.GetString(Resources.NamesByOrigin)));


        private Dictionary<NameType, IReadOnlyList<string>> Dictionary { get; } =
            new Dictionary<NameType, IReadOnlyList<string>>();

        /// <summary>
        ///     <para>Returns a randomized first name based on gender.</para>
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        public string FirstName(Gender gender) {
            return Name(
                gender == Gender.Male ? NameType.MaleFirst : NameType.FemaleFirst);
        }

        /// <summary>
        ///     <para>Returns a randomized First name</para>
        /// </summary>
        /// <returns></returns>
        public string FirstName() {
            return Name(_random.Next(2) == 0 ? NameType.FemaleFirst : NameType.MaleFirst);
        }

        /// <summary>
        ///     <para>Returns a randomized last name.</para>
        /// </summary>
        /// <returns></returns>
        public string LastName() {
            return Name(NameType.Last);
        }

        /// <summary>
        ///     <para>
        ///         Returns the collection used for randomizing names.
        ///         No argument will get every name.
        ///         With argument/arguments filters will be used for the origin.
        ///     </para>
        /// </summary>
        /// <param name="origins"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetCollection(params Origin[] origins) {
            return origins.Any()
                ? Names
                    .Where(n => origins.Contains(n.Country) || origins.Contains(n.Region))
                    .Select(n => n.Data)
                : Names.Select(n => n.Data);
        }

        /// <summary>
        ///     <para>Returns a name based on nametype.</para>
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
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