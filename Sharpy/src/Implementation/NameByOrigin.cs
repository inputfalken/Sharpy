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
    public class NameByOrigin : INameProvider<NameType> {
        private readonly Origin[] _origins;
        private readonly Random _random;

        internal NameByOrigin(Random random) {
            _random = random;
        }

        /// <summary>
        ///     Randomizes names with supplied random based on Origin.
        /// </summary>
        /// <param name="random"></param>
        /// <param name="origins"></param>
        public NameByOrigin(Random random, params Origin[] origins) {
            _random = random;
            _origins = origins;
        }

        /// <summary>
        ///     Randomizes names based on Origin.
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
        ///     <para>Returns a name based on nametype.</para>
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public string Name(NameType arg) {
            if (Dictionary.ContainsKey(arg)) return Dictionary[arg].RandomItem(_random);
            var strings = StringType(arg).ToArray();
            if (strings.Any()) Dictionary.Add(arg, strings);
            else throw new Exception("Can't obtain strings with this configuration");
            return Dictionary[arg].RandomItem(_random);
        }

        private IEnumerable<string> StringType(NameType nameType) {
            switch (nameType) {
                case NameType.FemaleFirstName:
                    return Origin(Names.Where(n => n.Type == 1)).Select(n => n.Data);
                case NameType.MaleFirstName:
                    return Origin(Names.Where(n => n.Type == 2)).Select(n => n.Data);
                case NameType.LastName:
                    return Origin(Names.Where(n => n.Type == 3)).Select(n => n.Data);
                case NameType.FirstName:
                    return Origin(Names.Where(n => (n.Type == 1) | (n.Type == 2))).Select(n => n.Data);
                case NameType.Any:
                    return Origin(Names).Select(n => n.Data);
                default:
                    throw new ArgumentOutOfRangeException(nameof(nameType), nameType, null);
            }
        }

        private IEnumerable<Name> Origin(IEnumerable<Name> names) => (_origins != null) && _origins.Any()
            ? names.Where(name => _origins.Contains(name.Country) | _origins.Contains(name.Region))
            : names;
    }
}