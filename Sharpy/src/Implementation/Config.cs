using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Sharpy.Enums;
using Sharpy.Implementation.DataObjects;
using Sharpy.Implementation.Generators;
using Sharpy.Properties;

namespace Sharpy.Implementation {
    /// <summary>
    ///     <para>Is used to configure Randomizer.</para>
    /// </summary>
    public sealed class Config {
        private const string NoSet = "None Set";
        internal NumberGenerator PhoneNumberGenerator { get; }

        private readonly HashSet<Enum> _origins = new HashSet<Enum>();
        private Randomizer<Name> _names;

        private string _seed;

        private Randomizer<string> _userNames;

        internal Config() {
            DateGenerator = new DateGenerator(Random);
            Mailgen = new MailGenerator(new[] {"gmail.com", "hotmail.com", "yahoo.com"}, Random);
            NumberGen = new NumberGenerator(Random);
            SocialSecurityNumberGenerator = new SecurityNumberGen(Random);
            PhoneNumberGenerator = new NumberGenerator(Random);
        }

        internal SecurityNumberGen SocialSecurityNumberGenerator { get; }

        private Lazy<Randomizer<Name>> LazyNames { get; } =
            new Lazy<Randomizer<Name>>(() => new Randomizer<Name>(JsonConvert.DeserializeObject<IEnumerable<Name>>(
                Encoding.UTF8.GetString(Resources.NamesByOrigin))));

        internal Randomizer<Name> Names {
            get { return _names ?? LazyNames.Value; }
            private set { _names = value; }
        }


        internal Random Random { get; private set; } = new Random();
        internal DateGenerator DateGenerator { get; }


        private NumberGenerator NumberGen { get; }


        internal MailGenerator Mailgen { get; }

        private Lazy<Randomizer<string>> LazyUsernames { get; } =
            new Lazy<Randomizer<string>>(
                () => new Randomizer<string>(Resources.usernames.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None)));

        private Randomizer<string> UserNames {
            get { return _userNames ?? LazyUsernames.Value; }
            set { _userNames = value; }
        }

        internal Dictionary<StringType, Randomizer<string>> Dictionary { get; } =
            new Dictionary<StringType, Randomizer<string>>();


        /// <summary>
        /// Executes the predicate on each firstname/lastname.
        /// </summary>
        public Func<string, bool> NamePredicate {
            set { Names = new Randomizer<Name>(Names.Where(name => value(name.Data))); }
        }


        /// <summary>
        ///     Sets Countries which Firstname/lastname are from.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<Country> NameCountries {
            set {
                foreach (var country in value) _origins.Add(country);
                Names = new Randomizer<Name>(Names.Where(name => value.Contains(name.Country)));
            }
        }

        /// <summary>
        ///     Sets Regions which Firstname/lastname are from.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<Region> NameRegion {
            set {
                foreach (var region in value) _origins.Add(region);
                Names = new Randomizer<Name>(Names.Where(name => value.Contains(name.Region)));
            }
        }

        /// <summary>
        /// <para>Sets the mailproviders which will be used for mailgenerator.</para>
        /// </summary>
        //public void MailProviders(params string[] providers) => Mailgen.EmailDomains = providers;
        public IReadOnlyList<string> MailProviders {
            set { Mailgen.EmailDomains = value; }
        }

        /// <summary>
        /// <para>Sets if mailaddresses are gonna be unique.</para>
        /// </summary>
        public bool UniqueMailAddresses {
            set { Mailgen.Unique = value; }
        }


        /// <summary>
        ///     Executes the predicate on each username.
        /// </summary>
        /// <returns></returns>
        public Func<string, bool> UserNamePredicate {
            set { UserNames = new Randomizer<string>(UserNames.Where(value)); }
        }

        /// <summary>
        ///     Will set a seed for the generator to use
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        public Config Seed(int seed) {
            _seed = seed.ToString();
            Random = new Random(seed);
            return this;
        }

        internal IEnumerable<string> StringType(StringType stringType) {
            switch (stringType) {
                case Enums.StringType.FemaleFirstName:
                    return Names.Where(name => name.Type == 1).Select(name => name.Data);
                case Enums.StringType.MaleFirstName:
                    return Names.Where(name => name.Type == 2).Select(name => name.Data);
                case Enums.StringType.LastName:
                    return Names.Where(name => name.Type == 3).Select(name => name.Data);
                case Enums.StringType.FirstName:
                    return Names.Where(name => name.Type == 1 | name.Type == 2).Select(name => name.Data);
                case Enums.StringType.UserName:
                    return UserNames;
                case Enums.StringType.AnyName:
                    return Names.Select(name => name.Data).Concat(UserNames);
                default:
                    throw new ArgumentOutOfRangeException(nameof(stringType), stringType, null);
            }
        }

        /// <inheritdoc />
        public override string ToString() {
            var origins = string.Empty;
            foreach (var origin in _origins)
                if (origin.Equals(_origins.Last())) origins += origin;
                else origins += $"{origin}, ";
            return
                $"Seed: {_seed ?? NoSet}. Using default for System.Random\n" +
                $"Mail: {Mailgen}\n" +
                $"Name: Origins: {(string.IsNullOrEmpty(origins) ? NoSet : origins)}";
        }
    }
}