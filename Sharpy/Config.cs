using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Sharpy.Types.CountryCode;
using Sharpy.Types.Date;
using Sharpy.Types.Enums;
using Sharpy.Types.Mail;
using Sharpy.Types.Name;
using Sharpy.Types.String;

namespace Sharpy {
    public class Config {
        internal Random Random { get; private set; } = new Random();
        internal DateGenerator DateGenerator { get; }

        public Config() {
            DateGenerator = new DateGenerator(Random);
            MailGenerator = new MailGenerator(new[] { "gmail.com", "hotmail.com", "yahoo.com" }, Random, false);
            PhoneNumberGenerator = new PhoneNumberGenerator("UnitedStates", "+1", Random);
        }

        private static Lazy<CountryCodeFilter> LazyCountryCodes { get; } =
            new Lazy<CountryCodeFilter>(
                () => new CountryCodeFilter(JsonConvert.DeserializeObject<IEnumerable<PhoneNumberGenerator>>(
                    Encoding.Default.GetString(Properties.Resources.CountryCodes))));

        private static Lazy<StringFilter> LazyUsernames { get; } =
            new Lazy<StringFilter>(() => new StringFilter(Properties.Resources.usernames.Split(Convert.ToChar("\n"))));


        private static Lazy<NameFilter> LazyNameFilter { get; } =
            new Lazy<NameFilter>(() => new NameFilter(JsonConvert.DeserializeObject<IEnumerable<Name>>(
                Encoding.UTF8.GetString(Properties.Resources.NamesByOrigin))));

        internal PhoneNumberGenerator PhoneNumberGenerator { get; private set; }


        internal MailGenerator MailGenerator { get; private set; }

        private StringFilter _userNamesField;

        internal StringFilter UserNames {
            get { return _userNamesField ?? LazyUsernames.Value; }
            private set { _userNamesField = value; }
        }


        private NameFilter _nameFilter;

        internal NameFilter NameFilter {
            get { return _nameFilter ?? LazyNameFilter.Value; }
            private set { _nameFilter = value; }
        }


        public Config NameOrigin(params Country[] countries) {
            NameFilter = NameFilter.ByCountry(countries);
            return this;
        }

        public Config NameOrigin(params Region[] regions) {
            NameFilter = NameFilter.ByRegion(regions);
            return this;
        }

        public Config Name(Func<IStringFilter<NameFilter>, NameFilter> func) {
            NameFilter = func(NameFilter);
            return this;
        }


        /// <summary>
        ///     Lets you change the providers for the mail addresses.
        ///     You can also a set a bool for wether the addreses will be unique.
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
            var phoneNumberGenerator = LazyCountryCodes.Value.Single(generator => generator.Name.Equals(country));
            PhoneNumberGenerator = new PhoneNumberGenerator(phoneNumberGenerator.Name.ToString(),
                phoneNumberGenerator.Code, Random)
            { Unique = uniqueNumbers };
            return this;
        }


        /// <summary>
        ///     Will let you filter the usernames result for the randomizer
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public Config UserName(Func<StringFilter, StringFilter> func) {
            UserNames = func(UserNames);
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