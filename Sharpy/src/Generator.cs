using System;
using System.Collections.Generic;
using System.Linq;
using NodaTime;
using Sharpy.Enums;
using Sharpy.Implementation;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;

namespace Sharpy {
    /// <summary>
    /// </summary>
    /// <remarks>
    ///     <para>If you want to generate data just call Generate/GenerateMany.</para>
    ///     <para>For examples please visit https://github.com/inputfalken/Sharpy </para>
    /// </remarks>
    public sealed class Generator : GeneratorBase {
        private readonly Random _random;
        private Tuple<int, int> _phoneState = new Tuple<int, int>(0, 0);
        private readonly INameProvider _nameProvider;
        private readonly IStringProvider _stringProvider;

        /// <summary>
        ///     <para>Instantiates a new generator which will generate based on the random supplied</para>
        /// </summary>
        public Generator(Random random)
            : base(new DoubleRandomizer(random), new IntRandomizer(random), new LongRandomizer(random)) {
            _random = random;
            DateGenerator = new DateGenerator(_random);
            Mailgen = new MailGenerator(new[] {"gmail.com", "hotmail.com", "yahoo.com"}, _random);
            SocialSecurityNumberGenerator = new SecurityNumberGen(_random);
            PhoneNumberGenerator = new NumberGenerator(_random);
            _nameProvider = new NameByOrigin(random);
            _stringProvider = new UserNameRandomizer(random);
        }

        private Generator(Configurement config)
            : base(
                new DoubleRandomizer(config.Random), new IntRandomizer(config.Random), new LongRandomizer(config.Random)
            ) {
            _random = config.Random;
            DateGenerator = new DateGenerator(config.Random);
            Mailgen = new MailGenerator(config.MailProviders, config.Random);
            SocialSecurityNumberGenerator = new SecurityNumberGen(config.Random);
            PhoneNumberGenerator = new NumberGenerator(config.Random);
            UniqueNumbers = config.UniqueNumbers;
            _nameProvider = config.Origins == null
                ? new NameByOrigin(config.Random)
                : new NameByOrigin(config.Random, config.Origins.ToArray());
            _stringProvider = new UserNameRandomizer(config.Random);
        }

        private NumberGenerator PhoneNumberGenerator { get; }

        private SecurityNumberGen SocialSecurityNumberGenerator { get; }

        private DateGenerator DateGenerator { get; }

        private MailGenerator Mailgen { get; }

        private bool UniqueNumbers { get; } = true;

        /// <summary>
        /// <para>Randomizes one of the arguments.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public T Params<T>(params T[] items) => items.RandomItem(_random);

        /// <summary>
        /// <para>Randomizes one of the elements in the IReadOnlyList.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public T CustomCollection<T>(IReadOnlyList<T> items) => items.RandomItem(_random);

        /// <summary>
        /// <para>Randomizes a bool.</para>
        /// </summary>
        /// <returns></returns>
        public bool Bool() => _random.Next(2) != 0;

        /// <summary>
        /// <para>Randomizes a date based on age.</para>
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public LocalDate DateByAge(int age) => DateGenerator.RandomDateByAge(age);

        /// <summary>
        /// <para>Randomizes a date based on year.</para>
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public LocalDate DateByYear(int year) => DateGenerator.RandomDateByYear(year);

        /// <summary>
        /// <para>Randomizes a unique SocialSecurity Number.</para>
        /// </summary>
        /// <param name="date"></param>
        /// <param name="formated"></param>
        /// <returns></returns>
        public string SocialSecurityNumber(LocalDate date, bool formated = true) {
            var result = SocialSecurityNumberGenerator.SecurityNumber(_random.Next(10000),
                FormatDigit(date.YearOfCentury).Append(FormatDigit(date.Month), FormatDigit(date.Day)));
            if (result == -1)
                throw new Exception("You have reached the maxium possible combinations for a controlnumber");
            var securityNumber = result.ToString();
            if (securityNumber.Length != 10)
                securityNumber = Prefix(result, 10 - securityNumber.Length);
            return formated ? securityNumber.Insert(6, "-") : securityNumber;
        }

        /// <summary>
        /// <para>Returns a string representing a mailaddress.</para>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="secondName"></param>
        /// <returns></returns>
        public string MailAddress(string name, string secondName = null)
            => Mailgen.Mail(name, secondName);

        /// <summary>
        /// <para>Returns a number with the length of the argument.</para>
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string NumberByLength(int length) {
            //If phonestate has changed
            if (_phoneState.Item1 != length)
                _phoneState = new Tuple<int, int>(length, (int) Math.Pow(10, length) - 1);
            var res = PhoneNumberGenerator.RandomNumber(0, _phoneState.Item2, UniqueNumbers);
            if (res == -1) throw new Exception("You reached maxium Ammount of combinations for the Length used");

            var phoneNumber = res.ToString();
            return phoneNumber.Length != length
                ? Prefix(phoneNumber, length - phoneNumber.Length)
                : phoneNumber;
        }

        /// <summary>
        /// <para>Returns common names based on argument.</para>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string Name(NameType type) => _nameProvider.Name(type);

        public string UserName() => _stringProvider.String();

        private static string Prefix<T>(T item, int ammount) => new string('0', ammount).Append(item);

        private static string FormatDigit(int i) => i < 10 ? Prefix(i, 1) : i.ToString();

        /// <summary>
        /// <para>Use this class if you want to configure your Generator. then call CreateGenerator method to get the generator.</para>
        /// </summary>
        public class Configurement {
            /// <summary>
            /// <para>Gets and Sets the Random which the Generator will use.</para>
            /// </summary>
            public Random Random { get; set; }

            /// <summary>
            ///     <para>Gets and Sets the mailproviders which will be used for generating MailAddresses.</para>
            ///     <para>This affects Generator's MailAddress method.</para>
            ///     <para>Set to gmail.com, hotmail.com and yahoo.com by default.</para>
            /// </summary>
            public IReadOnlyList<string> MailProviders { get; set; }

            /// <summary>
            ///     <para>Gets and Sets if mailaddresses are gonna be unique.</para>
            ///     <para>This affects Generator's MailAddress method.</para>
            ///     <para>Set to false by Default</para>
            /// </summary>
            public bool UniqueMails { get; set; }

            /// <summary>
            /// <para>Gets and Sets the origins of names returned the Generator's Name method.</para>
            /// <para>Set to nothing by default.</para>
            /// </summary>
            public IEnumerable<Origin> Origins { get; set; }

            /// <summary>
            ///     <para>Gets and Sets if Generator's NumberByLength returns unique numbers.</para>
            ///     <para>Set to false by Default</para>
            ///     <para>
            ///         NOTE:
            ///         If this is true the following will happen.
            ///         IGenerator's NumberByLength method will throw an exception if called more than Length^10
            ///     </para>
            /// </summary>
            public bool UniqueNumbers { get; set; }

            /// <summary>
            /// Creates a Generator with your configurement.
            /// </summary>
            /// <returns></returns>
            public Generator CreateGenerator() {
                if (Random == null) Random = new Random();
                if (MailProviders == null) MailProviders = new[] {"gmail.com", "hotmail.com", "yahoo.com"};
                return new Generator(this);
            }
        }
    }
}