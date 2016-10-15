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

namespace Sharpy {
    public class Config {
        public Config() {
            DateGenerator = new DateGenerator(Random);
            MailGeneratorP = new MailGenerator(new[] {"gmail.com", "hotmail.com", "yahoo.com"}, Random, false);
            PhoneNumberGenerator = new PhoneNumberGenerator(Random, 5, "070");
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

        /// <summary>
        ///     Lets you set the providers for the mail addresses.
        ///     You can also a set a bool for wether the addreses will be unique.
        ///     If set to unique numbers will be appended in case of replicate mail address.
        /// </summary>
        /// <param name="providers"></param>
        /// <param name="uniqueAddresses">For Unique Addresses</param>
        /// <returns></returns>
        public Config MailGenerator(IEnumerable<string> providers, bool uniqueAddresses = false) {
            MailGeneratorP = new MailGenerator(providers, Random, uniqueAddresses);
            return this;
        }

        /// <summary>
        ///     Lets you change the settings for the number generator.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="uniqueNumbers"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public Config PhoneGenerator(int length, bool uniqueNumbers = false, string prefix = null) {
            PhoneNumberGenerator =
                new PhoneNumberGenerator(
                    Random,
                    length,
                    prefix,
                    uniqueNumbers
                );
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


        private Lazy<Fetcher<Name>> LazyNames { get; } =
            new Lazy<Fetcher<Name>>(
                () => new Fetcher<Name>(JsonConvert.DeserializeObject<IEnumerable<Name>>(
                    Encoding.UTF8.GetString(Resources.NamesByOrigin))));

        private Fetcher<Name> _names;


        internal Fetcher<Name> Names {
            get { return _names ?? LazyNames.Value; }
            private set { _names = value; }
        }


        internal Random Random { get; private set; } = new Random();
        internal DateGenerator DateGenerator { get; }


        private Lazy<IEnumerable<CountryCode>> LazyCountryCodes { get; } =
            new Lazy<IEnumerable<CountryCode>>(
                () => JsonConvert.DeserializeObject<IEnumerable<CountryCode>>(
                    Encoding.Default.GetString(Resources.CountryCodes)));

        private Lazy<Fetcher<string>> LazyUsernames { get; } =
            new Lazy<Fetcher<string>>(() => new Fetcher<string>(Resources.usernames.Split(Convert.ToChar("\n"))));


        internal PhoneNumberGenerator PhoneNumberGenerator { get; set; }


        internal MailGenerator MailGeneratorP { get; set; }

        private Fetcher<string> _userNames;

        internal Fetcher<string> UserNames {
            get { return _userNames ?? LazyUsernames.Value; }
            private set { _userNames = value; }
        }

        private IEnumerable<Name> ByCountry(params Country[] args)
            => new Fetcher<Name>(Names.Where(name => args.Contains(name.Country)));


        private IEnumerable<Name> ByRegion(params Region[] args)
            => new Fetcher<Name>(Names.Where(name => args.Contains(name.Region)));

        internal Dictionary<StringType, Fetcher<string>> Dictionary { get; } =
            new Dictionary<StringType, Fetcher<string>>();

        internal IEnumerable<string> StringType(StringType stringType) {
            switch (stringType) {
                case Enums.StringType.FemaleFirstName:
                    return Names.Where(name => name.Type == 1).Select(name => name.Data);
                case Enums.StringType.MaleFirstName:
                    return Names.Where(name => name.Type == 2).Select(name => name.Data);
                case Enums.StringType.LastName:
                    return Names.Where(name => name.Type == 3).Select(name => name.Data);
                case Enums.StringType.MixedFirstName:
                    return Names.Where(name => name.Type == 1 | name.Type == 2).Select(name => name.Data);
                case Enums.StringType.UserName:
                    return UserNames;
                case Enums.StringType.Random:
                    return Random.Next(2) != 0 ? UserNames : Names.Select(name => name.Data);
                case Enums.StringType.AnyName:
                    return Names.Select(name => name.Data);
                default:
                    throw new ArgumentOutOfRangeException(nameof(stringType), stringType, null);
            }
        }
    }
}