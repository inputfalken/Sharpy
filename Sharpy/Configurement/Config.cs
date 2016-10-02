using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Sharpy.Enums;
using Sharpy.Properties;
using Sharpy.Types;
using Sharpy.Types.CountryCode;
using Sharpy.Types.Date;
using Sharpy.Types.Mail;
using Sharpy.Types.Name;

namespace Sharpy.Configurement {
    /// <summary>
    ///     Configures the result from the generator instance.
    /// </summary>
    public sealed class Config {
        private Fetcher<Name> _names;
        private Fetcher<string> _userNamesField;
        private Dictionary<NameType, Fetcher<string>> Dictionary { get; } = new Dictionary<NameType, Fetcher<string>>();

        internal Config() {
            DateGenerator = new DateGenerator(Random);
            MailGenerator = new MailGenerator(new[] {"gmail.com", "hotmail.com", "yahoo.com"}, Random, false);
            PhoneNumberGenerator = new PhoneNumberGenerator(new CountryCode("UnitedStates", "+1"), Random);
        }

        private static Lazy<Fetcher<Name>> LazyNames { get; } =
            new Lazy<Fetcher<Name>>(
                () => new Fetcher<Name>(JsonConvert.DeserializeObject<IEnumerable<Name>>(
                    Encoding.UTF8.GetString(Resources.NamesByOrigin))));

        internal Fetcher<Name> Names {
            get { return _names ?? LazyNames.Value; }
            private set { _names = value; }
        }

        internal Random Random { get; private set; } = new Random();
        internal DateGenerator DateGenerator { get; }


        private static Lazy<IEnumerable<CountryCode>> LazyCountryCodes { get; } =
            new Lazy<IEnumerable<CountryCode>>(
                () => JsonConvert.DeserializeObject<IEnumerable<CountryCode>>(
                    Encoding.Default.GetString(Resources.CountryCodes)));

        private static Lazy<Fetcher<string>> LazyUsernames { get; } =
            new Lazy<Fetcher<string>>(() => new Fetcher<string>(Resources.usernames.Split(Convert.ToChar("\n"))));


        internal PhoneNumberGenerator PhoneNumberGenerator { get; private set; }


        internal MailGenerator MailGenerator { get; private set; }

        internal Fetcher<string> UserNames {
            get { return _userNamesField ?? LazyUsernames.Value; }
            private set { _userNamesField = value; }
        }

        internal string Name() => Names.RandomItem(Random).Data;

        internal string Name(NameType nameType) {
            if (!Dictionary.ContainsKey(nameType))
                Dictionary.Add(nameType, new Fetcher<string>(Type(nameType).Select(name => name.Data)));
            return Dictionary[nameType].RandomItem(Random);
        }

        /// <summary>
        ///     Executes the predicate on each name.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Config Name(Func<string, bool> predicate) {
            Names = new Fetcher<Name>(Names.Where(name => predicate(name.Data)));
            return this;
        }

        /// <summary>
        ///     This filters the names by each Country provided
        /// </summary>
        /// <param name="countries"></param>
        /// <returns></returns>
        public Config Name(params Country[] countries) {
            Names = new Fetcher<Name>(ByCountry(countries));
            return this;
        }


        /// <summary>
        ///     This filters the names by each Region provided
        /// </summary>
        /// <param name="regions"></param>
        /// <returns></returns>
        public Config Name(params Region[] regions) {
            Names = new Fetcher<Name>(ByRegion(regions));
            return this;
        }

        private static int IndexOf(string str, string substring)
            => str.IndexOf(substring, StringComparison.CurrentCultureIgnoreCase);

        private IEnumerable<Name> ByCountry(params Country[] args)
            => new Fetcher<Name>(Names.Where(name => args.Contains(name.Country)));


        private IEnumerable<Name> ByRegion(params Region[] args)
            => new Fetcher<Name>(Names.Where(name => args.Contains(name.Region)));


        internal IEnumerable<Name> Type(NameType nameType) {
            switch (nameType) {
                case NameType.FemaleFirstName:
                    return Names.Where(name => name.Type == 1);
                case NameType.MaleFirstName:
                    return Names.Where(name => name.Type == 2);
                case NameType.LastName:
                    return Names.Where(name => name.Type == 3);
                case NameType.MixedFirstName:
                    return Names.Where(name => name.Type == 1 | name.Type == 2);
                default:
                    throw new ArgumentOutOfRangeException(nameof(nameType), nameType, null);
            }
        }


        /// <summary>
        ///     Lets you set the providers for the mail addresses.
        ///     You can also a set a bool for wether the addreses will be unique.
        ///     If set to unique numbers will be appended in case of replicate mail address.
        /// </summary>
        /// <param name="providers"></param>
        /// <param name="uniqueAddresses">For Unique Addresses</param>
        /// <returns></returns>
        public Config Mail(IEnumerable<string> providers, bool uniqueAddresses = false) {
            MailGenerator = new MailGenerator(providers, Random, uniqueAddresses);
            return this;
        }

        /// <summary>
        ///     Lets you change the country code
        /// </summary>
        /// <param name="country"></param>
        /// <param name="uniqueNumbers"></param>
        /// <returns></returns>
        public Config CountryCode(Country country, bool uniqueNumbers = false) {
            PhoneNumberGenerator =
                new PhoneNumberGenerator(LazyCountryCodes.Value.Single(number => number.Name == country), Random,
                    uniqueNumbers);
            return this;
        }


        /// <summary>
        ///     Executes the predicate on each username.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Config UserName(Func<string, bool> predicate) {
            UserNames = new Fetcher<string>(UserNames.Where(predicate));
            return this;
        }

        /// <summary>
        ///     Will set a seed for the generator to use
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        public Config Seed(int seed) {
            Random = new Random(seed);
            return this;
        }
    }
}