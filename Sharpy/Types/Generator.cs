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
        private Fetcher<Name.Name> _names;

        private Fetcher<string> _userNames;

        /// <summary>
        ///     Creates a Generator which you can use to create one instance or a collection of the given type
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public Generator(Func<IRandomizer, T> func, IRandomizer randomizer = null, IConfig<T> config = null) {
            Func = func;
            Config = config ?? new Config<T>(this);
            Randomizer = randomizer ?? new Randomizer<T>(this);
            DateGenerator = new DateGenerator(Random);
            MailGenerator = new MailGenerator(new[] {"gmail.com", "hotmail.com", "yahoo.com"}, Random, false);
            PhoneNumberGenerator = new PhoneNumberGenerator(new CountryCode.CountryCode("UnitedStates", "+1"), Random, 5);
        }

        /// <summary>
        ///     Creates a Generator which you can use to create one instance or a collection of the given type
        ///     The integer included will track iterations.
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public Generator(Func<IRandomizer, int, T> func, IRandomizer randomizer = null, IConfig<T> config = null) {
            FuncIterator = func;
            Config = config ?? new Config<T>(this);
            Randomizer = randomizer ?? new Randomizer<T>(this);
            DateGenerator = new DateGenerator(Random);
            MailGenerator = new MailGenerator(new[] {"gmail.com", "hotmail.com", "yahoo.com"}, Random, false);
            PhoneNumberGenerator = new PhoneNumberGenerator(new CountryCode.CountryCode("UnitedStates", "+1"), Random, 5);
        }

        internal IEnumerable<Name.Name> Type(NameType nameType) {
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

        private IRandomizer Randomizer { get; }

        private Func<IRandomizer, T> Func { get; }

        private Func<IRandomizer, int, T> FuncIterator { get; }
        public IConfig<T> Config { get; }

        private int Iteratation { get; set; }

        private Lazy<Fetcher<Name.Name>> LazyNames { get; } =
            new Lazy<Fetcher<Name.Name>>(
                () => new Fetcher<Name.Name>(JsonConvert.DeserializeObject<IEnumerable<Name.Name>>(
                    Encoding.UTF8.GetString(Resources.NamesByOrigin))));

        internal Fetcher<Name.Name> Names {
            get { return _names ?? LazyNames.Value; }
            set { _names = value; }
        }


        internal Dictionary<NameType, Fetcher<string>> Dictionary { get; } =
            new Dictionary<NameType, Fetcher<string>>();


        internal Random Random { get; set; } = new Random();
        internal DateGenerator DateGenerator { get; }


        internal Lazy<IEnumerable<CountryCode.CountryCode>> LazyCountryCodes { get; } =
            new Lazy<IEnumerable<CountryCode.CountryCode>>(
                () => JsonConvert.DeserializeObject<IEnumerable<CountryCode.CountryCode>>(
                    Encoding.Default.GetString(Resources.CountryCodes)));

        private Lazy<Fetcher<string>> LazyUsernames { get; } =
            new Lazy<Fetcher<string>>(() => new Fetcher<string>(Resources.usernames.Split(Convert.ToChar("\n"))));


        internal PhoneNumberGenerator PhoneNumberGenerator { get; set; }


        internal MailGenerator MailGenerator { get; set; }

        internal Fetcher<string> UserNames {
            get { return _userNames ?? LazyUsernames.Value; }
            set { _userNames = value; }
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
    }
}