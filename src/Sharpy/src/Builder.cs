using System;
using System.Collections.Generic;
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
    public class Builder : IDoubleProvider, IIntegerProvider, ILongProvider, INameProvider, IReadListElementProvider,
        IBoolProvider, IDateProvider, IEmailProvider, IPostalCodeProvider, ISecurityNumberProvider {
        private readonly IBoolProvider _boolProvider;
        private readonly IDateProvider _dateprovider;
        private readonly IDoubleProvider _doubleProvider;
        private readonly IEmailProvider _emailProvider;
        private readonly IIntegerProvider _integerProvider;

        private readonly ILongProvider _longProvider;
        private readonly INameProvider _nameProvider;
        private readonly IPostalCodeProvider _postalCodeProvider;
        private readonly IReadListElementProvider _readListElementProvider;
        private readonly ISecurityNumberProvider _securityNumberProvider;
        private readonly UniqueIntegerRandomizer _uniqueIntegerRandomizer;

        private readonly bool _uniqueNumbers;
        private (int, int) _numberByLengthState = (0, 0);

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="Builder" /> with <paramref name="configurement" />.
        ///     </para>
        /// </summary>
        /// <param name="configurement">
        ///     The configuration for the <see cref="Builder" />.
        /// </param>
        public Builder(Configurement configurement) {
            _doubleProvider = configurement.DoubleProvider ??
                              throw new ArgumentNullException(nameof(configurement.DoubleProvider));
            _integerProvider = configurement.IntegerProvider ??
                               throw new ArgumentNullException(nameof(configurement.IntegerProvider));
            _longProvider = configurement.LongProvider ??
                            throw new ArgumentNullException(nameof(configurement.LongProvider));
            _nameProvider = configurement.NameProvider ??
                            throw new ArgumentNullException(nameof(configurement.NameProvider));
            _dateprovider = configurement.DateProvider ??
                            throw new ArgumentNullException(nameof(configurement.DateProvider));
            _emailProvider = configurement.MailProvider ??
                             throw new ArgumentNullException(nameof(configurement.MailProvider));
            _securityNumberProvider = configurement.SecurityNumberProvider ??
                                      throw new ArgumentNullException(nameof(configurement.SecurityNumberProvider));
            _uniqueIntegerRandomizer = configurement.UniqueIntegerRandomizer ??
                                       throw new ArgumentNullException(nameof(configurement.UniqueIntegerRandomizer));
            _readListElementProvider = configurement.ListElementPicker ??
                                       throw new ArgumentNullException(nameof(configurement.ListElementPicker));
            _boolProvider = configurement.BoolProvider ??
                            throw new ArgumentNullException(nameof(configurement.BoolProvider));
            _postalCodeProvider = configurement.PostalCodeProvider ??
                                  throw new ArgumentNullException(nameof(configurement.PostalCodeProvider));
            _uniqueNumbers = configurement.UniqueNumbers;
        }

        /// <inheritdoc />
        public Builder(int seed) : this(new Configurement(seed)) { }

        /// <inheritdoc />
        public Builder() : this(new Configurement()) { }

        /// <inheritdoc cref="IBoolProvider.Bool" />
        public bool Bool() => _boolProvider.Bool();

        /// <inheritdoc />
        public DateTime DateByAge(int age) => _dateprovider.DateByAge(age);

        /// <inheritdoc />
        public DateTime DateByYear(int year) => _dateprovider.DateByYear(year);

        /// <inheritdoc cref="IDoubleProvider.Double()" />
        public double Double() => _doubleProvider.Double();

        /// <inheritdoc cref="IDoubleProvider.Double(double)" />
        public double Double(double max) => _doubleProvider.Double(max);

        /// <inheritdoc cref="IDoubleProvider.Double(double, double)" />
        public double Double(double min, double max) => _doubleProvider.Double(min, max);

        /// <summary>
        ///     <para>
        ///         Returns a <see cref="string" /> representing a unique mail address.
        ///     </para>
        /// </summary>
        /// <param name="names">The mail names of the mail address</param>
        /// <remarks>
        ///     If a mail address would be duplicated ; a random number would be appended to the address.
        /// </remarks>
        /// <returns>
        ///     A string representing a email address.
        /// </returns>
        public string Mail(params string[] names) => _emailProvider.Mail(names);

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
        public string PostalCode() => _postalCodeProvider.PostalCode();

        /// <inheritdoc />
        public string PostalCode(string county) => _postalCodeProvider.PostalCode(county);

        /// <inheritdoc cref="IReadListElementProvider.Element{T}" />
        public T Element<T>(IReadOnlyList<T> list) => _readListElementProvider.Element(list);

        /// <inheritdoc />
        public T Argument<T>(T first, T second, params T[] additional) =>
            _readListElementProvider.Argument(first, second, additional);

        /// <summary>
        ///     <para>
        ///         Randomizes a <see cref="string" /> representing a unique social-security number.
        ///     </para>
        /// </summary>
        /// <param name="date">
        ///     The date for the security number
        ///     e.g. new LocalDate(1990, 01, 02) would be converted to 90(year)01(month)02(date)-XXXX(control number)
        /// </param>
        /// ///
        /// <returns>
        ///     A <see cref="string" /> representing a unique social security number.
        /// </returns>
        public string SecurityNumber(DateTime date) =>
            _securityNumberProvider.SecurityNumber(date);

        public string SecurityNumber() => _securityNumberProvider.SecurityNumber();

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
            var res = _uniqueIntegerRandomizer.RandomNumber(0, _numberByLengthState.Item2, _uniqueNumbers);
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
        public string UserName() => Element(Data.GetUserNames);

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> by using <see cref="Builder" /> as input argument for argument
        ///         <see cref="selector" />.
        ///     </para>
        /// </summary>
        /// <param name="builder">
        ///     The instance of <see cref="Builder" /> you want to be used as a generator.
        /// </param>
        /// <param name="selector">A transform function to apply to each generation.</param>
        /// <typeparam name="TBuilder">
        ///     The <see cref="Builder" /> you want to have. This could be a descendant of <see cref="Builder" />.
        /// </typeparam>
        /// <typeparam name="TResult">The type of the result from the selector function.</typeparam>
        /// <returns>
        ///     <para>
        ///         A <see cref="IGenerator{T}" /> whose type is based on the return type of argument <see cref="selector" />.
        ///     </para>
        /// </returns>
        public static IGenerator<TResult> AsGenerator<TBuilder, TResult>(TBuilder builder,
            Func<TBuilder, TResult> selector) where TBuilder : Builder => builder != null
            ? selector != null
                ? Create(builder).Select(selector)
                : throw new ArgumentNullException(nameof(selector))
            : throw new ArgumentNullException(nameof(builder));

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> by using <see cref="Builder" /> as input argument for argument
        ///         <see cref="selector" />.
        ///     </para>
        /// </summary>
        /// <param name="selector">A transform function to apply to each generation.</param>
        /// <typeparam name="TResult">The type of the result from the selector function.</typeparam>
        /// <returns>
        ///     <para>
        ///         A <see cref="IGenerator{T}" /> whose type is based on the return type of argument <see cref="selector" />.
        ///     </para>
        /// </returns>
        public static IGenerator<TResult> AsGenerator<TResult>(
            Func<Builder, TResult> selector) => Create(new Builder())
            .Select(selector ?? throw new ArgumentNullException(nameof(selector)));
    }
}