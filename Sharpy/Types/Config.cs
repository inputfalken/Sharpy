using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Sharpy.Enums;
using Sharpy.Types.CountryCode;
using Sharpy.Types.Date;
using Sharpy.Types.Mail;
using Sharpy.Types.Name;
using Sharpy.Types.String;

namespace Sharpy.Types {
    /// <summary>
    ///     This class is used for configure each Generator created 
    /// </summary>
    public class Config {
        internal Random Random { get; private set; } = new Random();
        internal DateGenerator DateGenerator { get; }

        /// <summary>
        /// 
        /// </summary>
        public NameConfig Name { get; } = new NameConfig();

        internal Config() {
            DateGenerator = new DateGenerator(Random);
            MailGenerator = new MailGenerator(new[] { "gmail.com", "hotmail.com", "yahoo.com" }, Random, false);
            PhoneNumberGenerator = new PhoneNumberGenerator(new CountryCode.CountryCode("UnitedStates", "+1"), Random);
        }

        private static Lazy<IEnumerable<CountryCode.CountryCode>> LazyCountryCodes { get; } =
            new Lazy<IEnumerable<CountryCode.CountryCode>>(
                () => JsonConvert.DeserializeObject<IEnumerable<CountryCode.CountryCode>>(
                    Encoding.Default.GetString(Properties.Resources.CountryCodes)));

        private static Lazy<StringFilter> LazyUsernames { get; } =
            new Lazy<StringFilter>(() => new StringFilter(Properties.Resources.usernames.Split(Convert.ToChar("\n"))));


        internal PhoneNumberGenerator PhoneNumberGenerator { get; private set; }


        internal MailGenerator MailGenerator { get; private set; }

        private StringFilter _userNamesField;

        internal StringFilter UserNames {
            get { return _userNamesField ?? LazyUsernames.Value; }
            private set { _userNamesField = value; }
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
            PhoneNumberGenerator =
                new PhoneNumberGenerator(LazyCountryCodes.Value.Single(number => number.Name == country), Random,
                    uniqueNumbers);
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

    /// <summary>
    /// 
    /// </summary>
    public class NameConfig : IStringFilter<NameConfig> {
        private static Lazy<NameFilter> LazyNameFilter { get; } =
            new Lazy<NameFilter>(() => new NameFilter(JsonConvert.DeserializeObject<IEnumerable<Name.Name>>(
                Encoding.UTF8.GetString(Properties.Resources.NamesByOrigin))));

        private NameFilter _nameFilter;

        internal NameFilter NameFilter {
            get { return _nameFilter ?? LazyNameFilter.Value; }
            private set { _nameFilter = value; }
        }

        /// <summary>
        ///     This filters the names by each Country provided
        /// </summary>
        /// <param name="countries"></param>
        /// <returns></returns>
        public NameConfig NameOrigin(params Country[] countries) {
            NameFilter = NameFilter.ByCountry(countries);
            return this;
        }

        /// <summary>
        ///     This filters the names by each Region provided
        /// </summary>
        /// <param name="regions"></param>
        /// <returns></returns>
        public NameConfig NameOrigin(params Region[] regions) {
            NameFilter = NameFilter.ByRegion(regions);
            return this;
        }

        private static int IndexOf(string str, string substring)
            => str.IndexOf(substring, StringComparison.CurrentCultureIgnoreCase);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public NameConfig DoesNotStartWith(string arg) {
            NameFilter = new NameFilter(NameFilter.Where(s => IndexOf(s.Data, arg) != 0));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public NameConfig DoesNotContain(string arg) {
            NameFilter = new NameFilter(NameFilter.Where(s => IndexOf(s.Data, arg) < 0));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public NameConfig StartsWith(params string[] args) {
            NameFilter = args.Length == 1
                ? new NameFilter(NameFilter.Where(s => IndexOf(s.Data, args[0]) == 0))
                : new NameFilter(NameFilter.Where(s => args.Any(arg => IndexOf(s.Data, arg) == 0)));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public NameConfig Contains(params string[] args) {
            NameFilter = args.Length == 1
                ? new NameFilter(NameFilter.Where(s => s.Data.IndexOf(args[0], StringComparison.OrdinalIgnoreCase) >= 0))
                : new NameFilter(NameFilter.Where(s => args.Any(s.Data.Contains)));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public NameConfig ByLength(int length) {
            if (length < 1) throw new ArgumentOutOfRangeException($"{nameof(length)} is below 1");
            NameFilter = new NameFilter(NameFilter.Where(s => s.Data.Length == length));
            return this;
        }
    }
}