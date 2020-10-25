using System;
using System.Collections.Generic;
using Sharpy.Builder.Enums;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder {
    /// <summary>
    ///     <para>
    ///         Contains various methods for providing data.
    ///     </para>
    /// </summary>
    public class Builder : IDoubleProvider, IIntegerProvider, ILongProvider, INameProvider, IElementProvider,
        IBoolProvider, IDateProvider, IEmailProvider, ISecurityNumberProvider,
        IPhoneNumberProvider, IUserNameProvider, IArgumentProvider, IGuidProvider {
        private readonly IArgumentProvider _argumentProvider;
        private readonly IBoolProvider _boolProvider;
        private readonly IDateProvider _dateProvider;
        private readonly IDoubleProvider _doubleProvider;
        private readonly IElementProvider _elementProvider;
        private readonly IEmailProvider _emailProvider;
        private readonly IIntegerProvider _integerProvider;
        private readonly ILongProvider _longProvider;
        private readonly INameProvider _nameProvider;
        private readonly IPhoneNumberProvider _phoneNumberProvider;
        private readonly ISecurityNumberProvider _securityNumberProvider;
        private readonly IUserNameProvider _userNameProvider;
        private IGuidProvider _guidProvider;

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
            _dateProvider = configurement.DateProvider ??
                            throw new ArgumentNullException(nameof(configurement.DateProvider));
            _emailProvider = configurement.MailProvider ??
                             throw new ArgumentNullException(nameof(configurement.MailProvider));
            _securityNumberProvider = configurement.SecurityNumberProvider ??
                                      throw new ArgumentNullException(nameof(configurement.SecurityNumberProvider));
            _elementProvider = configurement.ListElementPicker ??
                               throw new ArgumentNullException(nameof(configurement.ListElementPicker));
            _boolProvider = configurement.BoolProvider ??
                            throw new ArgumentNullException(nameof(configurement.BoolProvider));
            _phoneNumberProvider = configurement.PhoneNumberProvider ??
                                   throw new ArgumentNullException(nameof(configurement.PhoneNumberProvider));
            _userNameProvider = configurement.UserNameProvider ??
                                throw new ArgumentNullException(nameof(configurement.UserNameProvider));
            _argumentProvider = configurement.ArgumentProvider ??
                                throw new ArgumentNullException(nameof(configurement.ArgumentProvider));

            _guidProvider = configurement.GuidProvider ??
                                          throw new ArgumentNullException(nameof(configurement.GuidProvider));
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
        public DateTime DateByAge(int age) => _dateProvider.DateByAge(age);

        /// <inheritdoc />
        public DateTime DateByYear(int year) => _dateProvider.DateByYear(year);

        /// <inheritdoc />
        public DateTime Date() => _dateProvider.Date();

        /// <inheritdoc />
        public DateTime Date(DateTime max) => _dateProvider.Date(max);

        public DateTime Date(DateTime min, DateTime max) => _dateProvider.Date(min, max);

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
        public string SecurityNumber(DateTime date) =>
            _securityNumberProvider.SecurityNumber(date);

        /// <inheritdoc />
        public string SecurityNumber() => _securityNumberProvider.SecurityNumber();

        /// <inheritdoc />
        public string UserName() => _userNameProvider.UserName();

        /// <inheritdoc />
        public Guid Guid() => _guidProvider.Guid();

        public string Guid(GuidFormat format) => _guidProvider.Guid(format);
    }

}