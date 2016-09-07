using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataGen.Types.CountryCode;
using DataGen.Types.Enums;
using DataGen.Types.Mail;
using DataGen.Types.Name;
using DataGen.Types.String;
using Newtonsoft.Json;
using static DataGen.Types.HelperClass;

namespace DataGen {
    //TODO make each Config contain an instance of Random which gets passed arround everywhere
    public class Config {
        private static Lazy<CountryCodeFilter> LazyCountryCodes { get; } =
            new Lazy<CountryCodeFilter>(
                () => new CountryCodeFilter(JsonConvert.DeserializeObject<IEnumerable<PhoneNumberGenerator>>(
                    Encoding.Default.GetString(Properties.Resources.CountryCodes))));

        private static CountryCodeFilter GetCountryCodes()
            => CountryCodes ?? LazyCountryCodes.Value;

        internal PhoneNumberGenerator PhoneNumberGenerator { get; private set; }
        private static CountryCodeFilter CountryCodes { get; set; }

        internal MailGenerator MailGenerator { get; private set; } =
            new MailGenerator(new[] { "gmail.com", "hotmail.com", "yahoo.com" }, false);


        private Lazy<StringFilter> LazyUsernames { get; } =
            new Lazy<StringFilter>(() => new StringFilter(Properties.Resources.usernames.Split(Convert.ToChar("\n"))));

        private StringFilter UserNames { get; set; }

        internal StringFilter GetUserNames() => UserNames ?? LazyUsernames.Value;

        private Lazy<NameFilter> LazyNameFilter { get; } =
            new Lazy<NameFilter>(() => new NameFilter(JsonConvert.DeserializeObject<IEnumerable<Name>>(
                Encoding.UTF8.GetString(Properties.Resources.NamesByOrigin))));

        private NameFilter NameFilter { get; set; }

        internal NameFilter GetNames() => NameFilter ?? LazyNameFilter.Value;

        public Config NameOrigin(params Country[] countries) {
            NameFilter = GetNames().ByCountry(countries);
            return this;
        }

        public Config NameOrigin(params Region[] regions) {
            NameFilter = NameFilter.ByRegion(regions);
            return this;
        }

        public Config Name(Func<IStringFilter<NameFilter>, NameFilter> func) {
            NameFilter = func(GetNames());
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
            MailGenerator = new MailGenerator(providers, uniqueAddresses);
            return this;
        }

        /// <summary>
        ///     Lets you change the country code
        /// </summary>
        /// <param name="country"></param>
        /// <param name="uniqueNumbers"></param>
        /// <returns></returns>
        public Config CountryCode(Country country, bool uniqueNumbers = false) {
            PhoneNumberGenerator = GetCountryCodes().First(generator => generator.Name.Equals(country));
            PhoneNumberGenerator.Unique = uniqueNumbers;
            return this;
        }


        /// <summary>
        ///     Will let you filter the usernames result for the randomizer
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public Config UserName(Func<StringFilter, StringFilter> func) {
            UserNames = func(GetUserNames());
            return this;
        }

        /// <summary>
        ///     NOTE: If you use this method it will set a seed for everything including future generators.
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        public Config Seed(int seed) {
            SetRandomizer(new Random(seed));
            return this;
        }
    }
}