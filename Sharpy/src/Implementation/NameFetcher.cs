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
    internal class NameFetcher : INameProvider {
        private readonly Random _random;

        private IEnumerable<Name> _names;
        private IEnumerable<string> _userNames;

        public NameFetcher(Random random) {
            _random = random;
        }

        private Lazy<IEnumerable<string>> LazyUsernames { get; } =
            new Lazy<IEnumerable<string>>(() => Resources.usernames.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None))
            ;

        private IEnumerable<Name> Names {
            get { return _names ?? LazyNames.Value; }
            set { _names = value; }
        }

        private IEnumerable<string> UserNames {
            get { return _userNames ?? LazyUsernames.Value; }
            set { _userNames = value; }
        }


        /// <summary>
        ///     <para>Sets the predicate which will be executed on each Firstname and Lastname.</para>
        ///     <para>This affects IGenerator's String method when you pass FirstName and Lastname as argument.</para>
        /// </summary>
        public Func<string, bool> NamePredicate {
            set { Names = Names.Where(name => value(name.Data)); }
        }

        /// <summary>
        ///     <para>Sets the predicate which will be executed on each UserName.</para>
        ///     <para>This affects IGenerator's String method when you use UserName as argument.</para>
        /// </summary>
        public Func<string, bool> UserNamePredicate {
            set { UserNames = UserNames.Where(value); }
        }


        private Lazy<IEnumerable<Name>> LazyNames { get; } =
            new Lazy<IEnumerable<Name>>(() => JsonConvert.DeserializeObject<IEnumerable<Name>>(
                Encoding.UTF8.GetString(Resources.NamesByOrigin)));

        /// <summary>
        ///     <para>Gets and Sets Origins to filer First and Last names with.</para>
        ///     <para>This affects IGenerator's String method when you pass FirstName and Lastname as argument.</para>
        ///     <para>Set to all countries by default.</para>
        /// </summary>
        public IEnumerable<Origin> Origins { get; set; }

        private Dictionary<NameType, IReadOnlyList<string>> Dictionary { get; } =
            new Dictionary<NameType, IReadOnlyList<string>>();

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
                    return Origin(Names.Where(name => name.Type == 1)).Select(name => name.Data);
                case NameType.MaleFirstName:
                    return Origin(Names.Where(name => name.Type == 2)).Select(name => name.Data);
                case NameType.LastName:
                    return Origin(Names.Where(name => name.Type == 3)).Select(name => name.Data);
                case NameType.FirstName:
                    return Origin(Names.Where(name => (name.Type == 1) | (name.Type == 2))).Select(name => name.Data);
                case NameType.UserName:
                    return UserNames;
                case NameType.Any:
                    return Names.Select(name => name.Data).Concat(UserNames);
                default:
                    throw new ArgumentOutOfRangeException(nameof(nameType), nameType, null);
            }
        }

        private IEnumerable<Name> Origin(IEnumerable<Name> names) => (Origins != null) && Origins.Any()
            ? names.Where(name => Origins.Contains(name.Country) | Origins.Contains(name.Region))
            : names;
    }
}