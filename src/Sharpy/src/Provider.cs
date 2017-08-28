using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using NodaTime;
using Sharpy.Enums;
using Sharpy.Generator;
using Sharpy.Implementation;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;
using static Sharpy.Generator.Generator;

namespace Sharpy {
    /// <summary>
    ///     <para>
    ///         Contains various methods for providing data by using <see cref="Random" />.
    ///         To get the same result every time you execute the program use the seed overload constructor.
    ///         If want you to add your own methods you can derive from this class.
    ///     </para>
    ///     <para>
    ///         For examples please visit ''.
    ///     </para>
    /// </summary>
    public class Provider : IDoubleProvider, IIntegerProvider, ILongProvider, INameProvider {
        private static readonly Lazy<string[]> LazyUsernames;
        private readonly DateGenerator _dateGenerator;
        private readonly IDoubleProvider _doubleProvider;
        private readonly IIntegerProvider _integerProvider;

        private readonly ILongProvider _longProvider;
        private readonly EmailBuilder _mailbuilder;
        private readonly INameProvider _nameProvider;
        private readonly NumberGenerator _numberGenerator;
        private readonly Random _random;
        private readonly SecurityNumberGen _securityNumberGen;

        private readonly bool _uniqueNumbers;
        private Tuple<int, int> _numberByLengthState = new Tuple<int, int>(0, 0);

        static Provider() {
            LazyUsernames = new Lazy<string[]>(() => {
                var assembly = Assembly.Load("Sharpy");
                var resourceStream = assembly.GetManifestResourceStream("Sharpy.Data.usernames.txt");
                using (var reader = new StreamReader(resourceStream, Encoding.UTF8)) {
                    return reader.ReadToEnd().Split(new[] {"\r\n", "\n"}, StringSplitOptions.None);
                }
            });
        }

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="Provider" /> with <paramref name="configurement" />.
        ///     </para>
        /// </summary>
        /// <param name="configurement">
        ///     The configuration for the <see cref="Provider" />.
        /// </param>
        public Provider(Configurement configurement) {
            _random = configurement.Random;
            _doubleProvider = configurement.DoubleProvider;
            _integerProvider = configurement.IntegerProvider;
            _longProvider = configurement.LongProvider;
            _nameProvider = configurement.NameProvider;
            _dateGenerator = configurement.DateGenerator;
            _mailbuilder = configurement.EmailBuilder;
            _securityNumberGen = configurement.SecurityNumberGen;
            _numberGenerator = configurement.NumberGenerator;
            _uniqueNumbers = configurement.UniqueNumbers;
        }

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="Provider" /> which will randomize the results depending on the <paramref name="seed" />.
        ///     </para>
        /// </summary>
        /// <param name="seed">
        ///     The <paramref name="seed" /> to be used when creating <see cref="Random" /> for randomizing data.
        /// </param>
        public Provider(int seed) : this(new Configurement(seed)) { }

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="Provider" /> which will randomize with the <see cref="Random" /> supplied.
        ///     </para>
        /// </summary>
        /// <param name="random">
        ///     The <see cref="Random" /> to be used when randomizing data.
        /// </param>
        public Provider(Random random) : this(new Configurement(random)) { }

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="Provider" /> which will randomize new results every time program is executed.
        ///     </para>
        /// </summary>
        public Provider() : this(new Configurement()) { }

        internal static IEnumerable<string> UserNames => LazyUsernames.Value;

        /// <inheritdoc cref="IDoubleProvider.Double()" />
        public double Double() => _doubleProvider.Double();

        /// <inheritdoc cref="IDoubleProvider.Double(double)" />
        public double Double(double max) => _doubleProvider.Double(max);

        /// <inheritdoc cref="IDoubleProvider.Double(double, double)" />
        public double Double(double min, double max) => _doubleProvider.Double(min, max);

        /// <inheritdoc cref="IIntegerProvider.Integer(int)" />
        public int Integer(int max) => _integerProvider.Integer(max);

        /// <inheritdoc cref="IIntegerProvider.Integer(int, int)" />
        public int Integer(int min, int max) => _integerProvider.Integer(min, max);

        /// <inheritdoc cref="IIntegerProvider.Integer()" />
        public int Integer() => _integerProvider.Integer();

        /// <inheritdoc cref="ILongProvider.Long(long,long)" />
        public long Long(long min, long max) => _longProvider.Long(min, max);

        /// <inheritdoc cref="ILongProvider.Long(long)" />
        public long Long(long max) => _longProvider.Long(max);

        /// <inheritdoc cref="ILongProvider.Long()" />
        public long Long() => _longProvider.Long();

        /// <inheritdoc cref="INameProvider.FirstName()" />
        public string FirstName() => _nameProvider.FirstName();

        /// <inheritdoc cref="INameProvider.FirstName(Gender)" />
        public string FirstName(Gender gender) => _nameProvider.FirstName(gender);

        /// <inheritdoc cref="INameProvider.LastName()" />
        public string LastName() => _nameProvider.LastName();

        /// <summary>
        ///     <para>
        ///         Randomizes one of the elements from argument <paramref name="items" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="T">The type to randomized from.</typeparam>
        /// <param name="items">The <paramref name="items" /> to randomize from.</param>
        public T Params<T>(params T[] items) => items.RandomItem(_random);

        /// <summary>
        ///     <para>
        ///         Randomizes one of the elements from argument <paramref name="items" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="T">The type to randomized from.</typeparam>
        /// <param name="items">The <paramref name="items" /> to randomize from.</param>
        public T CustomCollection<T>(IReadOnlyList<T> items) => items.RandomItem(_random);

        /// <summary>
        ///     <para>
        ///         Randomizes a <see cref="bool" />.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     A bool whose value is randomized.
        /// </returns>
        public bool Bool() => _random.Next(2) != 0;

        /// <summary>
        ///     <para>
        ///         Randomizes a <see cref="LocalDate" /> based on argument <paramref name="age" />.
        ///     </para>
        /// </summary>
        /// <param name="age">
        ///     The age of the date.
        /// </param>
        /// <returns>
        ///     A <see cref="LocalDate" /> with todays year minus the argument <paramref name="age" />.
        ///     The month and date has been randomized.
        /// </returns>
        public LocalDate DateByAge(int age) => _dateGenerator.RandomDateByAge(age);

        /// <summary>
        ///     <para>
        ///         Randomizes a <see cref="LocalDate" /> based on argument <paramref name="year" />.
        ///     </para>
        /// </summary>
        /// <param name="year">
        ///     The year of the date.
        /// </param>
        /// <returns>
        ///     A <see cref="LocalDate" /> with the argument <paramref name="year" /> as the year.
        ///     The month and date has been randomized.
        /// </returns>
        public LocalDate DateByYear(int year) => _dateGenerator.RandomDateByYear(year);

        /// <summary>
        ///     <para>
        ///         Randomizes a <see cref="string" /> representing a unique social-security number.
        ///     </para>
        /// </summary>
        /// <param name="date">
        ///     The date for the security number
        ///     e.g. new LocalDate(1990, 01, 02) would be converted to 90(year)01(month)02(date)-XXXX(control number)
        /// </param>
        /// <param name="formated">
        ///     If the security number should contain a dash to separate the date number with control number.
        /// </param>
        /// <returns>
        ///     A <see cref="string" /> representing a unique social security number.
        /// </returns>
        public string SocialSecurityNumber(LocalDate date, bool formated = true) {
            var result = _securityNumberGen.SecurityNumber(_random.Next(10000),
                FormatDigit(date.Year % 100).Append(FormatDigit(date.Month), FormatDigit(date.Day)));
            if (result == -1)
                throw new Exception("You have reached the maximum possible combinations for a control number");
            var securityNumber = result.ToString();
            if (securityNumber.Length != 10)
                securityNumber = Prefix(result, 10 - securityNumber.Length);
            return formated ? securityNumber.Insert(6, "-") : securityNumber;
        }

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="string" /> representing a unique mail address.
        ///     </para>
        /// </summary>
        /// <param name="name">The mail name of the mail address</param>
        /// <param name="secondName">A second name using a random separator</param>
        /// <remarks>
        ///     If a mail address would be duplicated ; a random number would be appended to the address.
        /// </remarks>
        /// <returns>
        ///     A string representing a email address.
        /// </returns>
        public string MailAddress(string name, string secondName = null) => _mailbuilder.Mail(name, secondName);

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="string" /> with its length equal to the number given to argument
        ///         <paramref name="length" />.
        ///     </para>
        /// </summary>
        /// <param name="length">The length of the <see cref="string" /> returned.</param>
        /// <returns>
        ///     A <see cref="string" /> with numbers with its length equal to the argument <paramref name="length" />.
        /// </returns>
        /// <exception cref="Exception">Reached maximum amount of combinations for the argument <paramref name="length" />.</exception>
        public string NumberByLength(int length) {
            //If _numberByLenghtState has changed
            if (_numberByLengthState.Item1 != length)
                _numberByLengthState = new Tuple<int, int>(length, (int) Math.Pow(10, length) - 1);
            var res = _numberGenerator.RandomNumber(0, _numberByLengthState.Item2, _uniqueNumbers);
            if (res == -1)
                throw new Exception($"Reached maximum amount of combinations for the argument '{nameof(length)}'.");

            var number = res.ToString();
            return number.Length != length
                ? Prefix(number, length - number.Length)
                : number;
        }

        /// <summary>
        ///     <para>
        ///         Returns a random <see cref="string" /> representing a user name.
        ///     </para>
        /// </summary>
        /// <returns>
        ///     A string representing a user name.
        /// </returns>
        public string UserName() => LazyUsernames.Value.RandomItem(_random);

        private static string Prefix<T>(T item, int ammount) => new string('0', ammount).Append(item);

        private static string FormatDigit(int i) => i < 10 ? Prefix(i, 1) : i.ToString();

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> with <see cref="Provider" /> as its generic type.
        ///     </para>
        /// </summary>
        /// <param name="provider">
        ///     The instance of <see cref="Provider" /> you want to be used as a generator.
        /// </param>
        /// <typeparam name="TProvider">
        ///     The <see cref="Provider" /> you want to have. This could be a descendant of <see cref="Provider" />.
        /// </typeparam>
        /// <returns>
        ///     <para>
        ///         A <see cref="IGenerator{T}" />.
        ///     </para>
        /// </returns>
        public static IGenerator<TProvider> AsGenerator<TProvider>(TProvider provider) where TProvider : Provider =>
            Create(provider);
    }
}