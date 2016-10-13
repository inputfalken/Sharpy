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
    /// <summary>
    ///     Contains Pre-Configured generators.
    /// </summary>
    public static class Generator {
        private static Generator<string> GenerateNames { get; } = new Generator<string>(
            randomizer => $"{randomizer.Name(NameType.MixedFirstName)} {randomizer.Name(NameType.LastName)}");

        /// <summary>
        ///     Generates a formated string containing First name, space followed by a Last name.
        /// </summary>
        /// <returns></returns>
        public static string Name() => GenerateNames.Generate();
    }

    /// <summary>
    ///     Contains methods for generating the supplied type
    ///     And a config to set settings for how the supplied type should get generated
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Generator<T> {
        private Fetcher<Name> _names;

        private Fetcher<string> _userNames;

        /// <summary>
        ///     Creates a Generator which you can use to create one instance or a collection of the given type
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public Generator(Func<IRandomizer, T> func, IRandomizer randomizer = null) {
            Func = func;
            Randomizer = randomizer ?? new Randomizer<T>(this);
            DateGenerator = new DateGenerator(Random);
            MailGenerator = new MailGenerator(new[] {"gmail.com", "hotmail.com", "yahoo.com"}, Random, false);
            PhoneNumberGenerator = new PhoneNumberGenerator(new CountryCode("UnitedStates", "+1"), Random, 5);
        }

        /// <summary>
        ///     Creates a Generator which you can use to create one instance or a collection of the given type
        ///     The integer included will track iterations.
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public Generator(Func<IRandomizer, int, T> func, IRandomizer randomizer = null) {
            FuncIterator = func;
            Randomizer = randomizer ?? new Randomizer<T>(this);
            DateGenerator = new DateGenerator(Random);
            MailGenerator = new MailGenerator(new[] {"gmail.com", "hotmail.com", "yahoo.com"}, Random, false);
            PhoneNumberGenerator = new PhoneNumberGenerator(new CountryCode("UnitedStates", "+1"), Random, 5);
        }


        private IRandomizer Randomizer { get; }

        private Func<IRandomizer, T> Func { get; }

        private Func<IRandomizer, int, T> FuncIterator { get; }

        private int Iteratation { get; set; }

        private Lazy<Fetcher<Name>> LazyNames { get; } =
            new Lazy<Fetcher<Name>>(
                () => new Fetcher<Name>(JsonConvert.DeserializeObject<IEnumerable<Name>>(
                    Encoding.UTF8.GetString(Resources.NamesByOrigin))));

        internal Fetcher<Name> Names {
            get { return _names ?? LazyNames.Value; }
            private set { _names = value; }
        }


        internal Dictionary<NameType, Fetcher<string>> Dictionary { get; } =
            new Dictionary<NameType, Fetcher<string>>();


        internal Random Random { get; private set; } = new Random();
        internal DateGenerator DateGenerator { get; }


        private Lazy<IEnumerable<CountryCode>> LazyCountryCodes { get; } =
            new Lazy<IEnumerable<CountryCode>>(
                () => JsonConvert.DeserializeObject<IEnumerable<CountryCode>>(
                    Encoding.Default.GetString(Resources.CountryCodes)));

        private Lazy<Fetcher<string>> LazyUsernames { get; } =
            new Lazy<Fetcher<string>>(() => new Fetcher<string>(Resources.usernames.Split(Convert.ToChar("\n"))));


        internal PhoneNumberGenerator PhoneNumberGenerator { get; private set; }


        internal MailGenerator MailGenerator { get; private set; }

        internal Fetcher<string> UserNames {
            get { return _userNames ?? LazyUsernames.Value; }
            private set { _userNames = value; }
        }

        private T Generate(int i) => FuncIterator == null ? Func(Randomizer) : FuncIterator(Randomizer, i);

        /// <summary>
        ///     Will give back one instance of the specified Type
        /// </summary>
        /// <returns></returns>
        public T Generate() => FuncIterator == null ? Func(Randomizer) : FuncIterator(Randomizer, Iteratation++);

        /// <summary>
        ///     Will give back an IEnumerable with the specified type.
        ///     Which contains the ammount of elements.
        /// </summary>
        /// <param name="ammount"></param>
        public IEnumerable<T> GenerateEnumerable(int ammount) {
            for (var i = 0; i < ammount; i++)
                yield return Generate(i);
        }


        /// <summary>
        ///     Executes the predicate on each name.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Generator<T> ConfigName(Func<string, bool> predicate) {
            Names = new Fetcher<Name>(Names.Where(name => predicate(name.Data)));
            return this;
        }

        /// <summary>
        ///     This filters the names by each Country provided
        /// </summary>
        /// <param name="countries"></param>
        /// <returns></returns>
        public Generator<T> ConfigName(params Country[] countries) {
            Names = new Fetcher<Name>(ByCountry(countries));
            return this;
        }


        /// <summary>
        ///     This filters the names by each Region provided
        /// </summary>
        /// <param name="regions"></param>
        /// <returns></returns>
        public Generator<T> ConfigName(params Region[] regions) {
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
        public Generator<T> ConfigMailGen(IEnumerable<string> providers, bool uniqueAddresses = false) {
            MailGenerator = new MailGenerator(providers, Random, uniqueAddresses);
            return this;
        }

        /// <summary>
        ///     Lets you change the settings for the number generator.
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="length"></param>
        /// <param name="uniqueNumbers"></param>
        /// <returns></returns>
        public Generator<T> ConfigPhoneGen(Country countryCode, int length, bool uniqueNumbers = false) {
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
        public Generator<T> ConfigUserName(Func<string, bool> predicate) {
            UserNames = new Fetcher<string>(UserNames.Where(predicate));
            return this;
        }

        /// <summary>
        ///     Will set a seed for the generator to use
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        public Generator<T> Seed(int seed) {
            Random = new Random(seed);
            return this;
        }


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
    }
}