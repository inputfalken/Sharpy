using System;
using System.Collections.Generic;
using Sharpy.Core;
using Sharpy.Core.Linq;
using Sharpy.Enums;
using Sharpy.IProviders;
using static Sharpy.Core.Generator;

namespace Sharpy {
    /// <summary>
    ///     <para>
    ///         Contains various methods for providing data.
    ///     </para>
    /// </summary>
    public class Builder : IDoubleProvider, IIntegerProvider, ILongProvider, INameProvider, IElementProvider,
        IBoolProvider, IDateProvider, IEmailProvider, IPostalCodeProvider, ISecurityNumberProvider,
        IPhoneNumberProvider, IUserNameProvider, IArgumentProvider {
        private readonly IArgumentProvider _argumentProvider;
        private readonly IBoolProvider _boolProvider;
        private readonly IDateProvider _dateprovider;
        private readonly IDoubleProvider _doubleProvider;
        private readonly IElementProvider _elementProvider;
        private readonly IEmailProvider _emailProvider;
        private readonly IIntegerProvider _integerProvider;

        private readonly ILongProvider _longProvider;
        private readonly INameProvider _nameProvider;
        private readonly IPhoneNumberProvider _phoneNumberProvider;
        private readonly IPostalCodeProvider _postalCodeProvider;
        private readonly ISecurityNumberProvider _securityNumberProvider;
        private readonly IUserNameProvider _userNameProvider;

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
            _elementProvider = configurement.ListElementPicker ??
                               throw new ArgumentNullException(nameof(configurement.ListElementPicker));
            _boolProvider = configurement.BoolProvider ??
                            throw new ArgumentNullException(nameof(configurement.BoolProvider));
            _postalCodeProvider = configurement.PostalCodeProvider ??
                                  throw new ArgumentNullException(nameof(configurement.PostalCodeProvider));
            _phoneNumberProvider = configurement.PhoneNumberProvider ??
                                   throw new ArgumentNullException(nameof(configurement.PhoneNumberProvider));
            _userNameProvider = configurement.UserNameProvider ??
                                throw new ArgumentNullException(nameof(configurement.UserNameProvider));
            _argumentProvider = configurement.ArgumentProvider ??
                                throw new ArgumentNullException(nameof(configurement.ArgumentProvider));
        }

        /// <inheritdoc />
        public Builder(int seed) : this(new Configurement(seed)) { }

        /// <inheritdoc />
        public Builder() : this(new Configurement()) { }

        /// <inheritdoc />
        public T Argument<T>(T first, T second, params T[] additional) =>
            _argumentProvider.Argument(first, second, additional);

        /// <inheritdoc />
        public bool Bool() => _boolProvider.Bool();

        /// <inheritdoc />
        public DateTime DateByAge(int age) => _dateprovider.DateByAge(age);

        /// <inheritdoc />
        public DateTime DateByYear(int year) => _dateprovider.DateByYear(year);

        /// <inheritdoc />
        public DateTime Date() => _dateprovider.Date();

        /// <inheritdoc />
        public double Double() => _doubleProvider.Double();

        /// <inheritdoc />
        public double Double(double max) => _doubleProvider.Double(max);

        /// <inheritdoc />
        public double Double(double min, double max) => _doubleProvider.Double(min, max);

        /// <inheritdoc />
        public T Element<T>(IReadOnlyList<T> list) => _elementProvider.Element(list);

        /// <inheritdoc />
        public string Mail(params string[] names) => _emailProvider.Mail(names);

        ///<inheritdoc />
        public string Mail() => _emailProvider.Mail();

        /// <inheritdoc />
        public int Integer(int max) => _integerProvider.Integer(max);

        /// <inheritdoc />
        public int Integer(int min, int max) => _integerProvider.Integer(min, max);

        /// <inheritdoc />
        public int Integer() => _integerProvider.Integer();

        /// <inheritdoc />
        public long Long(long min, long max) => _longProvider.Long(min, max);

        /// <inheritdoc />
        public long Long(long max) => _longProvider.Long(max);

        /// <inheritdoc />
        public long Long() => _longProvider.Long();

        /// <inheritdoc />
        public string FirstName() => _nameProvider.FirstName();

        /// <inheritdoc />
        public string FirstName(Gender gender) => _nameProvider.FirstName(gender);

        /// <inheritdoc />
        public string LastName() => _nameProvider.LastName();

        ///<inheritdoc />
        public string PhoneNumber(int length) => _phoneNumberProvider.PhoneNumber(length);

        ///<inheritdoc />
        public string PhoneNumber() => _phoneNumberProvider.PhoneNumber();

        /// <inheritdoc />
        public string PostalCode() => _postalCodeProvider.PostalCode();

        /// <inheritdoc />
        public string PostalCode(string county) => _postalCodeProvider.PostalCode(county);

        /// <inheritdoc />
        public string SecurityNumber(DateTime date) =>
            _securityNumberProvider.SecurityNumber(date);

        /// <inheritdoc />
        public string SecurityNumber() => _securityNumberProvider.SecurityNumber();

        /// <inheritdoc />
        public string UserName() => _userNameProvider.UserName();

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="IGenerator{T}" /> by using <see cref="Builder" /> as input argument for argument
        ///         <see paramref="selector" />.
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
        ///         A <see cref="IGenerator{T}" /> whose type is based on the return type of argument <see paramref="selector" />.
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
        ///     </para>
        /// </summary>
        /// <param name="selector">A transform function to apply to each generation.</param>
        /// <typeparam name="TResult">The type of the result from the selector function.</typeparam>
        /// <returns>
        ///     <para>
        ///         A <see cref="IGenerator{T}" /> whose type is based on the return type of argument <see paramref="selector" />.
        ///     </para>
        /// </returns>
        public static IGenerator<TResult> AsGenerator<TResult>(
            Func<Builder, TResult> selector) => Create(new Builder())
            .Select(selector ?? throw new ArgumentNullException(nameof(selector)));
    }

    /// <summary>
    ///     Provides a set of static methods for <see cref="Builder" />.
    /// </summary>
    public static class BuilderExtensions {
        /// <summary>
        ///     <para>
        ///         Turns the <see cref="Builder" /> into a <see cref="IGenerator{T}" />.
        ///     </para>
        ///     <para>
        ///         This also works for descenders of <see cref="Builder" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TBuilder">
        ///     The type of the <see cref="Builder"/> or a descendant.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The result from the parameter <paramref name="selector" />.
        /// </typeparam>
        /// <param name="source">
        ///     The <see cref="Builder"/> or a descendant.
        /// </param>
        /// <param name="selector">
        ///     The function whose result will be the type of the <see cref="IGenerator{T}"/>.
        /// </param>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose type is the result from the function argument <paramref name="selector" />.
        /// </returns>
        public static IGenerator<TResult> ToGenerator<TBuilder, TResult>(this TBuilder source,
            Func<TBuilder, TResult> selector) where TBuilder : Builder {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            return Create(source).Select(selector);
        }

        /// <summary>
        ///     <para>
        ///         Turns the <see cref="Builder" /> into a <see cref="IGenerator{T}" />.
        ///     </para>
        ///     <para>
        ///         This also works for descenders of <see cref="Builder" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TBuilder">
        ///     The type of the <see cref="Builder"/> or a descendant.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The result from the parameter <paramref name="selector" />.
        /// </typeparam>
        /// <param name="source">
        ///     The <see cref="Builder"/> or a descendant.
        /// </param>
        /// <param name="selector">
        ///     The function whose result will be the type of the <see cref="IGenerator{T}"/>.
        /// </param>
        /// <returns>
        ///     A <see cref="IGenerator{T}" /> whose type is the result from the function argument <paramref name="selector" />.
        /// </returns>
        public static IGenerator<TResult> ToGenerator<TBuilder, TResult>(this TBuilder source,
            Func<TBuilder, int, TResult> selector) where TBuilder : Builder {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            return Create(source).Select(selector);
        }

        /// <summary>
        ///     <para>
        ///         Turns the <see cref="Builder" /> into a <see cref="IEnumerable{T}" />.
        ///     </para>
        ///     <para>
        ///         This also works for descenders of <see cref="Builder" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TBuilder">
        ///     The type of the <see cref="Builder"/> or a descendant.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The result from the parameter <paramref name="selector" />.
        /// </typeparam>
        /// <param name="source">
        ///     The builder.
        /// </param>
        /// <param name="selector">
        ///     The function whose result will be the type of the <see cref="IEnumerable{T}"/>.
        /// </param>
        /// <param name="count">
        ///     The <paramref name="count" /> of the returned <see cref="IEnumerable{T}" />.
        /// </param>
        /// <returns>
        ///     A <see cref="IEnumerable{T}" /> whose type is the result from the function argument <paramref name="selector" />.
        /// </returns>
        public static IEnumerable<TResult> ToEnumerable<TBuilder, TResult>(this TBuilder source,
            Func<TBuilder, TResult> selector, int count) where TBuilder : Builder {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            for (var i = 0; i < count; i++) yield return selector(source);
        }

        /// <summary>
        ///     <para>
        ///         Turns the <see cref="Builder" /> into a <see cref="IEnumerable{T}" />.
        ///     </para>
        ///     <para>
        ///         This also works for descenders of <see cref="Builder" />.
        ///     </para>
        /// </summary>
        /// <typeparam name="TBuilder">
        ///     The type of the <see cref="Builder"/> or a descendant.
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     The result from the parameter <paramref name="selector" />.
        /// </typeparam>
        /// <param name="source">
        ///     The builder.
        /// </param>
        /// <param name="selector">
        ///     The function whose result will be the type of the <see cref="IEnumerable{T}"/>.
        /// </param>
        /// <param name="count">
        ///     The <paramref name="count" /> of the returned <see cref="IEnumerable{T}" />.
        /// </param>
        /// <returns>
        ///     A <see cref="IEnumerable{T}" /> whose type is the result from the function argument <paramref name="selector" />.
        /// </returns>
        public static IEnumerable<TResult> ToEnumerable<TBuilder, TResult>(this TBuilder source,
            Func<TBuilder, int, TResult> selector, int count) where TBuilder : Builder {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            for (var i = 0; i < count; i++) yield return selector(source, i);
        }
    }
}