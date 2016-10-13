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
    public sealed class Generator<T> : GeneratorBase<T, IRandomizer> {
        /// <summary>
        ///     Creates a Generator which you can use to create one instance or a collection of the given type
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public Generator(Func<IRandomizer, T> func, IRandomizer randomizer = null)
            : base(func, randomizer) {
            FuncArg = randomizer ?? new Randomizer<T>(this);
            _config = new Config<T>(this);
            DateGenerator = new DateGenerator(Random);
            MailGenerator = new MailGenerator(new[] {"gmail.com", "hotmail.com", "yahoo.com"}, Random, false);
            PhoneNumberGenerator = new PhoneNumberGenerator(new CountryCode("UnitedStates", "+1"), Random, 5);
        }

        /// <summary>
        ///     Creates a Generator which you can use to create one instance or a collection of the given type
        ///     The integer included will track iterations.
        ///     For examples please visit https://github.com/inputfalken/Sharpy
        /// </summary>
        public Generator(Func<IRandomizer, int, T> func, IRandomizer randomizer = null)
            : base(func, randomizer) {
            FuncArg = randomizer ?? new Randomizer<T>(this);
            _config = new Config<T>(this);
            DateGenerator = new DateGenerator(Random);
            MailGenerator = new MailGenerator(new[] {"gmail.com", "hotmail.com", "yahoo.com"}, Random, false);
            PhoneNumberGenerator = new PhoneNumberGenerator(new CountryCode("UnitedStates", "+1"), Random, 5);
        }


        public Config<T> Config {
            get {
                if (FuncArg is Randomizer<T>) return _config;
                throw new Exception("You cannot use this property with a custom randomizer.");
            }
        }

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
        private readonly Config<T> _config;

        internal Fetcher<string> UserNames {
            get { return _userNames ?? LazyUsernames.Value; }
            set { _userNames = value; }
        }
    }
}