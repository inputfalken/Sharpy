using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NodaTime;
using Sharpy.Enums;
using Sharpy.Properties;
using Sharpy.Types.CountryCode;
using Sharpy.Types.Date;
using Sharpy.Types.Mail;

namespace Sharpy.Types {
    /// <summary>
    ///     Will randomize all data that these methods return.
    /// </summary>
    public sealed class Randomizer {
        public Randomizer() {
            DateGenerator = new DateGenerator(Random);
            MailGenerator = new MailGenerator(new[] {"gmail.com", "hotmail.com", "yahoo.com"}, Random, false);
            PhoneNumberGenerator = new PhoneNumberGenerator(new CountryCode.CountryCode("UnitedStates", "+1"), Random, 7);
        }


        /// <summary>
        ///     Can be used if you have your own collection of items that you would want an random item from.
        /// </summary>
        /// <returns></returns>
        public T CustomCollection<T>(params T[] items) => items[Number(items.Length)];

        /// <summary>
        ///     Can be used if you have your own collection of items that you would want an random item from.
        /// </summary>
        /// <param name="items"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CustomCollection<T>(List<T> items) => items[Number(items.Count)];

        /// <summary>
        ///     Gives a random name, it could be a female first name, male first name and a lastname.
        /// </summary>
        public string Name() => Names.RandomItem(Random).Data;


        /// <summary>
        ///     Gives a random name based on type of argument.
        /// </summary>
        public string Name(NameType nameType) {
            if (!Dictionary.ContainsKey(nameType))
                Dictionary.Add(nameType, new Fetcher<string>(Type(nameType).Select(name => name.Data)));
            return Dictionary[nameType].RandomItem(Random);
        }

        /// <summary>
        ///     Gives a random username from a huge collection.
        /// </summary>
        public string UserName() => UserNames.RandomItem(Random);

        /// <summary>
        ///     Gives a random bool
        /// </summary>
        public bool Bool() => Number(2) != 0;

        /// <summary>
        ///     Gives a random number within below the argument value
        /// </summary>
        public int Number(int maxNum) => Random.Next(maxNum);

        /// <summary>
        ///     Gives a random number within within the two arguments
        /// </summary>
        public int Number(int minNum, int maxNum) => Random.Next(minNum, maxNum);

        /// <summary>
        ///     Gives a date with random month, date and subtract the current the current year by the argument
        /// </summary>
        public LocalDate DateByAge(int age) => DateGenerator.RandomDateByAge(age);

        /// <summary>
        ///     Gives a random month, date and use the argument given as year
        /// </summary>
        public LocalDate DateByYear(int year) => DateGenerator.RandomDateByYear(year);

        /// <summary>
        ///     gives a random phonenumber using a random country code and lets you specify a number to start with as well as the
        ///     length.
        /// </summary>
        public string PhoneNumber(string preNumber = null) =>
            PhoneNumberGenerator.RandomNumber(preNumber);

        /// <summary>
        ///     Gives a mail address by concatining the arguments into a mail address.
        /// </summary>
        public string MailAdress(string name, string secondName = null) => MailGenerator.Mail(name, secondName);

        private Fetcher<Name.Name> _names;
        private Fetcher<string> _userNames;


        private Dictionary<NameType, Fetcher<string>> Dictionary { get; } = new Dictionary<NameType, Fetcher<string>>();

        private static Lazy<Fetcher<Name.Name>> LazyNames { get; } =
            new Lazy<Fetcher<Name.Name>>(
                () => new Fetcher<Name.Name>(JsonConvert.DeserializeObject<IEnumerable<Name.Name>>(
                    Encoding.UTF8.GetString(Resources.NamesByOrigin))));

        internal Fetcher<Name.Name> Names {
            get { return _names ?? LazyNames.Value; }
            set { _names = value; }
        }

        internal Random Random { get; set; } = new Random();
        private DateGenerator DateGenerator { get; }


        internal static Lazy<IEnumerable<CountryCode.CountryCode>> LazyCountryCodes { get; } =
            new Lazy<IEnumerable<CountryCode.CountryCode>>(
                () => JsonConvert.DeserializeObject<IEnumerable<CountryCode.CountryCode>>(
                    Encoding.Default.GetString(Resources.CountryCodes)));

        private static Lazy<Fetcher<string>> LazyUsernames { get; } =
            new Lazy<Fetcher<string>>(() => new Fetcher<string>(Resources.usernames.Split(Convert.ToChar("\n"))));


        internal PhoneNumberGenerator PhoneNumberGenerator { get; set; }


        internal MailGenerator MailGenerator { get; set; }

        internal Fetcher<string> UserNames {
            get { return _userNames ?? LazyUsernames.Value; }
            set { _userNames = value; }
        }


        internal IEnumerable<Name.Name> ByCountry(params Country[] args)
            => new Fetcher<Name.Name>(Names.Where(name => args.Contains(name.Country)));


        internal IEnumerable<Name.Name> ByRegion(params Region[] args)
            => new Fetcher<Name.Name>(Names.Where(name => args.Contains(name.Region)));


        private IEnumerable<Name.Name> Type(NameType nameType) {
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