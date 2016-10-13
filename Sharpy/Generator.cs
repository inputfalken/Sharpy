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
    ///    Supply the class you want to be generated.
    ///    Use the randomizer to give you data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Generator<T> {
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
            PhoneNumberGenerator = new PhoneNumberGenerator(new CountryCode("UnitedStates", "+1"), Random, 5);
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

        private Fetcher<Name> _names;


        internal Fetcher<Name> Names {
            get { return _names ?? LazyNames.Value; }
            set { _names = value; }
        }


        internal Random Random { get; set; } = new Random();
        internal DateGenerator DateGenerator { get; }


        internal Lazy<IEnumerable<CountryCode>> LazyCountryCodes { get; } =
            new Lazy<IEnumerable<CountryCode>>(
                () => JsonConvert.DeserializeObject<IEnumerable<CountryCode>>(
                    Encoding.Default.GetString(Resources.CountryCodes)));

        private Lazy<Fetcher<string>> LazyUsernames { get; } =
            new Lazy<Fetcher<string>>(() => new Fetcher<string>(Resources.usernames.Split(Convert.ToChar("\n"))));


        internal PhoneNumberGenerator PhoneNumberGenerator { get; set; }


        internal MailGenerator MailGenerator { get; set; }

        private Fetcher<string> _userNames;

        internal Fetcher<string> UserNames {
            get { return _userNames ?? LazyUsernames.Value; }
            set { _userNames = value; }
        }

        private T Generate(int i) => FuncIterator == null ? Func(Randomizer) : FuncIterator(Randomizer, i);

        public IConfig<T> Config { get; }

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