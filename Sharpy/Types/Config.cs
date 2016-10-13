using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Sharpy.Enums;
using Sharpy.Properties;
using Sharpy.Types.CountryCode;
using Sharpy.Types.Date;
using Sharpy.Types.Mail;

namespace Sharpy.Types {
    public class Config<T> {
        private Generator<T> Generator { get; }

        public Config(Generator<T> generator) {
            Generator = generator;

            DateGenerator = new DateGenerator(Random);
            MailGeneratorP = new MailGenerator(new[] {"gmail.com", "hotmail.com", "yahoo.com"}, Random, false);
            PhoneNumberGenerator = new PhoneNumberGenerator(new CountryCode.CountryCode("UnitedStates", "+1"), Random, 5);
        }


        /// <summary>
        ///     Executes the predicate on each name.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Config<T> Name(Func<string, bool> predicate) {
            Names = new Fetcher<Name.Name>(Names.Where(name => predicate(name.Data)));
            return this;
        }

        /// <summary>
        ///     This filters the names by each Country provided
        /// </summary>
        /// <param name="countries"></param>
        /// <returns></returns>
        public Config<T> Name(params Country[] countries) {
            Names = new Fetcher<Name.Name>(ByCountry(countries));
            return this;
        }


        /// <summary>
        ///     This filters the names by each Region provided
        /// </summary>
        /// <param name="regions"></param>
        /// <returns></returns>
        public Config<T> Name(params Region[] regions) {
            Names = new Fetcher<Name.Name>(ByRegion(regions));
            return this;
        }

        /// <summary>
        ///     Lets you set the providers for the mail addresses.
        ///     You can also a set a bool for wether the addreses will be unique.
        ///     If set to unique numbers will be appended in case of replicate mail address.
        /// </summary>
        /// <param name="providers"></param>
        /// <param name="uniqueAddresses">For Unique Addresses</param>
        /// <returns></returns>
        public Config<T> MailGenerator(IEnumerable<string> providers, bool uniqueAddresses = false) {
            MailGeneratorP = new MailGenerator(providers, Random, uniqueAddresses);
            return this;
        }

        /// <summary>
        ///     Lets you change the settings for the number generator.
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="length"></param>
        /// <param name="uniqueNumbers"></param>
        /// <returns></returns>
        public Config<T> PhoneGenerator(Country countryCode, int length, bool uniqueNumbers = false) {
            PhoneNumberGenerator =
                new PhoneNumberGenerator(LazyCountryCodes.Value.Single(number => number.Name == countryCode),
                    Random,
                    length, uniqueNumbers);
            return this;
        }


        /// <summary>
        ///     Executes the predicate on each username.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Config<T> UserName(Func<string, bool> predicate) {
            UserNames = new Fetcher<string>(UserNames.Where(predicate));
            return this;
        }

        /// <summary>
        ///     Will set a seed for the generator to use
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        public Config<T> Seed(int seed) {
            Random = new Random(seed);
            return this;
        }


        private Lazy<Fetcher<Name.Name>> LazyNames { get; } =
            new Lazy<Fetcher<Name.Name>>(
                () => new Fetcher<Name.Name>(JsonConvert.DeserializeObject<IEnumerable<Name.Name>>(
                    Encoding.UTF8.GetString(Resources.NamesByOrigin))));

        private Fetcher<Name.Name> _names;


        internal Fetcher<Name.Name> Names {
            get { return _names ?? LazyNames.Value; }
            set { _names = value; }
        }


        internal Random Random { get; set; } = new Random();
        internal DateGenerator DateGenerator { get; }


        private Lazy<IEnumerable<CountryCode.CountryCode>> LazyCountryCodes { get; } =
            new Lazy<IEnumerable<CountryCode.CountryCode>>(
                () => JsonConvert.DeserializeObject<IEnumerable<CountryCode.CountryCode>>(
                    Encoding.Default.GetString(Resources.CountryCodes)));

        private Lazy<Fetcher<string>> LazyUsernames { get; } =
            new Lazy<Fetcher<string>>(() => new Fetcher<string>(Resources.usernames.Split(Convert.ToChar("\n"))));


        internal PhoneNumberGenerator PhoneNumberGenerator { get; set; }


        internal MailGenerator MailGeneratorP { get; set; }

        private Fetcher<string> _userNames;

        internal Fetcher<string> UserNames {
            get { return _userNames ?? LazyUsernames.Value; }
            set { _userNames = value; }
        }

        private IEnumerable<Name.Name> ByCountry(params Country[] args)
            => new Fetcher<Name.Name>(Names.Where(name => args.Contains(name.Country)));


        private IEnumerable<Name.Name> ByRegion(params Region[] args)
            => new Fetcher<Name.Name>(Names.Where(name => args.Contains(name.Region)));

        internal Dictionary<NameType, Fetcher<string>> Dictionary { get; } =
            new Dictionary<NameType, Fetcher<string>>();

        internal IEnumerable<Name.Name> Type(NameType nameType) {
            switch (nameType) {
                case NameType.FemaleFirstName:
                    return Generator.Config.Names.Where(name => name.Type == 1);
                case NameType.MaleFirstName:
                    return Generator.Config.Names.Where(name => name.Type == 2);
                case NameType.LastName:
                    return Generator.Config.Names.Where(name => name.Type == 3);
                case NameType.MixedFirstName:
                    return Generator.Config.Names.Where(name => name.Type == 1 | name.Type == 2);
                default:
                    throw new ArgumentOutOfRangeException(nameof(nameType), nameType, null);
            }
        }
    }
}