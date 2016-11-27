using System;
using System.Collections.Generic;
using System.Linq;
using NodaTime;
using Sharpy.Enums;
using Sharpy.Implementation;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;
using Sharpy.Properties;

namespace Sharpy {
    /// <summary>
    ///     <para>
    ///         Contains various methods for Generating data and utility methods which you can combine with the generation
    ///         methods.
    ///         If you would want the same result every time you invoke these methods you can set the seed for the random
    ///         required
    ///         by the constructor.
    ///     </para>
    ///     <para>If you want to map this to instantiate another class you can call Generate/GenerateSequence.</para>
    ///     <para>For examples please visit https://github.com/inputfalken/Sharpy </para>
    /// </summary>
    public class Generator : IDoubleProvider, IIntegerProvider, ILongProvider, INameProvider {
        private readonly Random _random;
        private readonly INameProvider _nameProvider;
        private readonly IDoubleProvider _doubleProvider;
        private readonly IIntegerProvider _integerProvider;
        private readonly ILongProvider _longProvider;
        private Tuple<int, int> _phoneState = new Tuple<int, int>(0, 0);

        /// <summary>
        ///     <para>Instantiates a new generator which will generate based on the random supplied</para>
        /// </summary>
        public Generator(Random random) : this(new Configurement {
            Random = random,
            IntegerProvider = new IntRandomizer(random),
            DoubleProvider = new DoubleRandomizer(random),
            LongProvider = new LongRandomizer(random),
            NameProvider = new NameByOrigin(random),
            UniqueMails = false,
            UniqueNumbers = false
        }) {}

        private Generator(Configurement conf) {
            _random = conf.Random;
            _doubleProvider = conf.DoubleProvider;
            _integerProvider = conf.IntegerProvider;
            _longProvider = conf.LongProvider;
            DateGenerator = new DateGenerator(conf.Random);
            Mailgen = new MailGenerator(conf.MailProviders, conf.Random);
            SocialSecurityNumberGenerator = new SecurityNumberGen(conf.Random);
            PhoneNumberGenerator = new NumberGenerator(conf.Random);
            UniqueNumbers = conf.UniqueNumbers;
            Mailgen.Unique = conf.UniqueMails;
            _nameProvider = conf.NameProvider;
        }

        private NumberGenerator PhoneNumberGenerator { get; }

        private SecurityNumberGen SocialSecurityNumberGenerator { get; }

        private DateGenerator DateGenerator { get; }

        private MailGenerator Mailgen { get; }

        private bool UniqueNumbers { get; }

        private Lazy<string[]> LazyUsernames { get; } =
            new Lazy<string[]>(() => Resources.usernames.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None));


        /// <summary>
        ///     <para>Returns a random first name.</para>
        /// </summary>
        /// <returns></returns>
        public string FirstName() => _nameProvider.FirstName();

        /// <summary>
        ///     <para>Returns a first name based on Gender.</para>
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        public string FirstName(Gender gender) => _nameProvider.FirstName(gender);

        /// <summary>
        ///     <para>Returns a random lastname.</para>
        /// </summary>
        /// <returns></returns>
        public string LastName() => _nameProvider.LastName();

        /// <summary>
        ///     <para>Randomizes one of the arguments.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public T Params<T>(params T[] items) => items.RandomItem(_random);

        /// <summary>
        ///     <para>Randomizes one of the elements in the IReadOnlyList.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public T CustomCollection<T>(IReadOnlyList<T> items) => items.RandomItem(_random);

        /// <summary>
        ///     <para>Randomizes a bool.</para>
        /// </summary>
        /// <returns></returns>
        public bool Bool() => _random.Next(2) != 0;

        /// <summary>
        ///     <para>Randomizes a date based on age.</para>
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public LocalDate DateByAge(int age) => DateGenerator.RandomDateByAge(age);

        /// <summary>
        ///     <para>Randomizes a date based on year.</para>
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public LocalDate DateByYear(int year) => DateGenerator.RandomDateByYear(year);

        /// <summary>
        ///     <para>Randomizes a unique SocialSecurity Number.</para>
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
        ///     <para>Returns a string representing a mailaddress.</para>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="secondName"></param>
        /// <returns></returns>
        public string MailAddress(string name, string secondName = null)
            => Mailgen.Mail(name, secondName);

        /// <summary>
        ///     <para>Returns a number with the length of the argument.</para>
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
        ///     <para>Returns a username.</para>
        /// </summary>
        /// <returns></returns>
        public string UserName() => LazyUsernames.Value.RandomItem(_random);

        /// <summary>
        ///     <para>Generates a Double.</para>
        /// </summary>
        /// <returns></returns>
        public double Double() => _doubleProvider.Double();

        /// <summary>
        ///     <para>Generates a double within max value.</para>
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public double Double(double max) => _doubleProvider.Double(max);

        /// <summary>
        ///     <para>Generates a within min and max.</para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public double Double(double min, double max) => _doubleProvider.Double(min, max);

        /// <summary>
        ///     <para>Generates a Integer.</para>
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public int Integer(int max) => _integerProvider.Integer(max);

        /// <summary>
        ///     <para>Generates a integer within min and max.</para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int Integer(int min, int max) => _integerProvider.Integer(min, max);

        /// <summary>
        ///     <para>Generates a integer.</para>
        /// </summary>
        /// <returns></returns>
        public int Integer() => _integerProvider.Integer();

        /// <summary>
        ///     <para>Generates a long within min and max.</para>
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public long Long(long min, long max) => _longProvider.Long(min, max);

        /// <summary>
        ///     <para>Generates a long within max.</para>
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public long Long(long max) => _longProvider.Long(max);

        /// <summary>
        ///     Generates a long.
        /// </summary>
        /// <returns></returns>
        public long Long() => _longProvider.Long();

        private static string Prefix<T>(T item, int ammount) => new string('0', ammount).Append(item);

        private static string FormatDigit(int i) => i < 10 ? Prefix(i, 1) : i.ToString();

        /// <summary>
        ///     <para>
        ///         Use this class if you want to configure your Generator. then call CreateGenerator to get the generator.
        ///     </para>
        /// </summary>
        public class Configurement {
            /// <summary>
            ///     <para>Gets and Sets the implementation which Generator's FirstName, LastName methods use.</para>
            ///     <para>By default the names loaded from an internal file supplied by this library.</para>
            /// </summary>
            public INameProvider NameProvider { get; set; }

            /// <summary>
            ///     <para>Gets and Sets the implementation which Generator's Double methods use.</para>
            ///     <para>By Default the doubles are randomized</para>
            /// </summary>
            public IDoubleProvider DoubleProvider { get; set; }

            /// <summary>
            ///     <para>Gets and Sets the implementation which Generator's Integer methods use.</para>
            ///     <para>By Default the ints are randomized</para>
            /// </summary>
            public IIntegerProvider IntegerProvider { get; set; }

            /// <summary>
            ///     <para>Gets and Sets the implementation which Generator's Long methods use.</para>
            ///     <para>By Default the longs are randomized</para>
            /// </summary>
            public ILongProvider LongProvider { get; set; }

            /// <summary>
            ///     <para>Gets and Sets the Random which the Generator will use.</para>
            /// </summary>
            public Random Random { get; set; } = new Random();

            /// <summary>
            ///     <para>Gets and Sets the mailproviders which will be used for generating MailAddresses.</para>
            ///     <para>This affects Generator's MailAddress method.</para>
            ///     <para>Set to gmail.com, hotmail.com and yahoo.com by default.</para>
            /// </summary>
            public IReadOnlyList<string> MailProviders { get; set; } = new[] {"gmail.com", "hotmail.com", "yahoo.com"};

            /// <summary>
            ///     <para>Gets and Sets if the generator's MailAddress are return unique mail addresses.</para>
            ///     <para>This affects Generator's MailAddress method.</para>
            ///     <para>
            ///         Set to false by Default
            ///         NOTE:
            ///         If this is set to true the following will happen.
            ///         MailAddress will now append numbers to the end of the mailaddress if it  allready exist.
            ///     </para>
            /// </summary>
            public bool UniqueMails { get; set; }

            /// <summary>
            ///     <para>Gets and Sets if Generator's NumberByLength returns unique numbers.</para>
            ///     <para>Set to false by Default</para>
            ///     <para>
            ///         NOTE:
            ///         If this is set to true the following will happen.
            ///         Generator's NumberByLength method will throw an exception if called more than Length^10
            ///     </para>
            /// </summary>
            public bool UniqueNumbers { get; set; }

            /// <summary>
            ///     Creates a Generator with your configurement.
            /// </summary>
            /// <returns></returns>
            public Generator CreateGenerator() {
                if (LongProvider == null) LongProvider = new LongRandomizer(Random);
                if (IntegerProvider == null) IntegerProvider = new IntRandomizer(Random);
                if (DoubleProvider == null) DoubleProvider = new DoubleRandomizer(Random);
                if (NameProvider == null) NameProvider = new NameByOrigin(Random);
                return new Generator(this);
            }
        }
    }
}