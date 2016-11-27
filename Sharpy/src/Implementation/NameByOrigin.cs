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
    ///     <para>Randomizes Common names by Origin.</para>
    /// </summary>
    public class NameByOrigin : INameProvider {
        private readonly Origin[] _origins;
        private readonly Random _random;

        internal NameByOrigin(Random random) {
            _random = random;
        }

        /// <summary>
        ///     <para>Randomizes names with supplied random based on Origin.</para>
        /// </summary>
        /// <param name="random"></param>
        /// <param name="origins"></param>
        public NameByOrigin(Random random, params Origin[] origins) {
            _random = random;
            _origins = origins;
        }

        /// <summary>
        ///     <para>Randomizes names based on Origin.</para>
        /// </summary>
        /// <param name="origins"></param>
        public NameByOrigin(params Origin[] origins) {
            _random = new Random();
            _origins = origins;
        }


        private IEnumerable<Name> Names => LazyNames.Value;


        private Lazy<IEnumerable<Name>> LazyNames { get; } =
            new Lazy<IEnumerable<Name>>(() => JsonConvert.DeserializeObject<IEnumerable<Name>>(
                Encoding.UTF8.GetString(Resources.NamesByOrigin)));


        private Dictionary<NameType, IReadOnlyList<string>> Dictionary { get; } =
            new Dictionary<NameType, IReadOnlyList<string>>();

        /// <summary>
        ///     <para>Returns a randomized first name based on gender.</para>
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        public string FirstName(Gender gender)
            => Name(gender == Gender.Male ? NameType.MaleFirst : NameType.FemaleFirst);

        /// <summary>
        ///     <para>Returns a randomized First name</para>
        /// </summary>
        /// <returns></returns>
        public string FirstName() => Name(_random.Next(2) == 0 ? NameType.FemaleFirst : NameType.MaleFirst);

        /// <summary>
        ///     <para>Returns a randomized last name.</para>
        /// </summary>
        /// <returns></returns>
        public string LastName() => Name(NameType.Last);

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

        private IEnumerable<Name> Origin(IEnumerable<Name> names) => (_origins != null) && _origins.Any()
            ? names.Where(name => _origins.Contains(name.Country) | _origins.Contains(name.Region))
            : names;
    }
}