using System;
using System.Collections.Generic;
using System.Linq;
using NodaTime;
using Sharpy.Core;
using Sharpy.Core.Linq;
using Sharpy.Enums;
using Sharpy.Implementation;
using Sharpy.Implementation.ExtensionMethods;
using Sharpy.IProviders;
using static Sharpy.Core.Generator;

namespace Sharpy {
    /// <summary>
    ///     <para>
    ///         Contains various methods for providing data by using <see cref="IIntegerProvider" />.
    ///         To get the same result every time you execute the program use the seed overload constructor.
    ///         If want you to add your own methods you can derive from this class.
    ///     </para>
    /// </summary>
    public class Builder : IDoubleProvider, IIntegerProvider, ILongProvider, INameProvider, IListElementPicker,
        IBoolProvider {
        private readonly DateGenerator _dateGenerator;
        private readonly IDoubleProvider _doubleProvider;
        private readonly IIntegerProvider _integerProvider;

        private readonly ILongProvider _longProvider;
        private readonly EmailBuilder _mailbuilder;
        private readonly INameProvider _nameProvider;
        private readonly NumberGenerator _numberGenerator;
        private readonly SecurityNumberGen _securityNumberGen;
        private readonly IBoolProvider _boolProvider;

        private readonly bool _uniqueNumbers;
        private (int, int) _numberByLengthState = (0, 0);
        private readonly IListElementPicker _listElementPicker;

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="Builder" /> with <paramref name="configurement" />.
        ///     </para>
        /// </summary>
        /// <param name="configurement">
        ///     The configuration for the <see cref="Builder" />.
        /// </param>
        public Builder(Configurement configurement) {
            _doubleProvider = configurement.DoubleProvider;
            _integerProvider = configurement.IntegerProvider;
            _longProvider = configurement.LongProvider;
            _nameProvider = configurement.NameProvider;
            _dateGenerator = configurement.DateGenerator;
            _mailbuilder = configurement.EmailBuilder;
            _securityNumberGen = configurement.SecurityNumberGen;
            _numberGenerator = configurement.NumberGenerator;
            _uniqueNumbers = configurement.UniqueNumbers;
            _listElementPicker = configurement.ListElementPicker;
            _boolProvider = configurement.BoolProvider;
        }

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="Builder" /> which will randomize the results depending on the <paramref name="seed" />.
        ///     </para>
        /// </summary>
        /// <param name="seed">
        ///     The <paramref name="seed" /> to be used when creating <see cref="Random" /> for randomizing data.
        /// </param>
        public Builder(int seed) : this(new Configurement(seed)) { }

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="Builder" /> which will randomize new results every time program is executed.
        ///     </para>
        /// </summary>
        public Builder() : this(new Configurement()) { }

        private static string[] UserNames => Data.GetUserNames;

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

        /// <inheritdoc />
        public bool Bool() => _boolProvider.Bool();

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
            var result = _securityNumberGen.SecurityNumber(FormatDigit(date.Year % 100)
                .Append(FormatDigit(date.Month), FormatDigit(date.Day)));
            if (result == -1)
                throw new Exception("You have reached the maximum possible combinations for a control number");
            var securityNumber = result.ToString();
            if (securityNumber.Length != 10)
                securityNumber = result.Prefix(10 - securityNumber.Length);
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
                _numberByLengthState = (length, (int) Math.Pow(10, length) - 1);
            var res = _numberGenerator.RandomNumber(0, _numberByLengthState.Item2, _uniqueNumbers);
            if (res == -1)
                throw new Exception($"Reached maximum amount of combinations for the argument '{nameof(length)}'.");

            var number = res.ToString();
            return number.Length != length
                ? number.Prefix(length - number.Length)
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
        public string UserName() => TakeElement(UserNames);

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> with <see cref="Builder" /> as its generic type.
        ///     </para>
        /// </summary>
        /// <param name="provider">
        ///     The instance of <see cref="Builder" /> you want to be used as a generator.
        /// </param>
        /// <param name="selector">A transform function to apply to each generation.</param>
        /// <typeparam name="TProvider">
        ///     The <see cref="Builder" /> you want to have. This could be a descendant of <see cref="Builder" />.
        /// </typeparam>
        /// <typeparam name="TResult">The type of the result from the selector function.</typeparam>
        /// <returns>
        ///     <para>
        ///         A <see cref="IGenerator{T}" />.
        ///     </para>
        /// </returns>
        public static IGenerator<TResult> AsGenerator<TProvider, TResult>(TProvider provider,
            Func<TProvider, TResult> selector) where TProvider : Builder => provider != null
            ? selector != null
                ? Create(provider).Select(selector)
                : throw new ArgumentNullException(nameof(selector))
            : throw new ArgumentNullException(nameof(provider));

        private static string FormatDigit(int i) => i < 10 ? i.Prefix(1) : i.ToString();

        /// <inheritdoc />
        public T TakeElement<T>(IReadOnlyList<T> list) => _listElementPicker.TakeElement(list);

        public T TakeArgument<T>(params T[] list) => _listElementPicker.TakeArgument(list);
    }
}