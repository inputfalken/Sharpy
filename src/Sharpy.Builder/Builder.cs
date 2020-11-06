using System;
using System.Collections.Generic;
using Sharpy.Builder.Enums;
using Sharpy.Builder.IProviders;

namespace Sharpy.Builder
{
    /// <summary>
    ///     <para>
    ///         Contains various methods for providing data.
    ///     </para>
    /// </summary>
    public class Builder : IDoubleProvider, IIntegerProvider, ILongProvider, INameProvider, IElementProvider,
        IBoolProvider, IDateProvider, IEmailProvider, ISecurityNumberProvider,
        IPhoneNumberProvider, IUserNameProvider, IArgumentProvider, IGuidProvider, ITimeSpanProvider
    {
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
        private readonly IGuidProvider _guidProvider;
        private readonly ITimeSpanProvider _timeSpanProvider;

        /// <summary>
        ///     <para>
        ///         Creates a <see cref="Builder" /> with <paramref name="configurement" />.
        ///     </para>
        /// </summary>
        /// <param name="configurement">
        ///     The configuration for the <see cref="Builder" />.
        /// </param>
        public Builder(Configurement configurement)
        {
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
            _timeSpanProvider = configurement.TimeSpanProvider ??
                                throw new ArgumentNullException(nameof(configurement.TimeSpanProvider));
        }

        /// <inheritdoc />
        public Builder(int seed) : this(new Configurement(seed))
        {
        }

        /// <inheritdoc />
        public Builder() : this(new Configurement())
        {
        }

        /// <inheritdoc />
        public T Argument<T>(T first, T second, params T[] additional)
        {
            return _argumentProvider.Argument(first, second, additional);
        }

        /// <inheritdoc />
        public bool Bool()
        {
            return _boolProvider.Bool();
        }

        /// <inheritdoc />
        public DateTime DateByAge(int age)
        {
            return _dateProvider.DateByAge(age);
        }

        /// <inheritdoc />
        public DateTime DateByYear(int year)
        {
            return _dateProvider.DateByYear(year);
        }

        /// <inheritdoc />
        public DateTime Date()
        {
            return _dateProvider.Date();
        }

        /// <inheritdoc />
        public DateTime Date(DateTime max)
        {
            return _dateProvider.Date(max);
        }

        public DateTime Date(DateTime min, DateTime max)
        {
            return _dateProvider.Date(min, max);
        }

        /// <inheritdoc />
        public double Double()
        {
            return _doubleProvider.Double();
        }

        /// <inheritdoc />
        public double Double(double max)
        {
            return _doubleProvider.Double(max);
        }

        /// <inheritdoc />
        public double Double(double min, double max)
        {
            return _doubleProvider.Double(min, max);
        }

        /// <inheritdoc />
        public T Element<T>(IReadOnlyList<T> list)
        {
            return _elementProvider.Element(list);
        }

        /// <inheritdoc />
        public string Mail(params string[] names)
        {
            return _emailProvider.Mail(names);
        }

        ///<inheritdoc />
        public string Mail()
        {
            return _emailProvider.Mail();
        }

        /// <inheritdoc />
        public int Integer(int max)
        {
            return _integerProvider.Integer(max);
        }

        /// <inheritdoc />
        public int Integer(int min, int max)
        {
            return _integerProvider.Integer(min, max);
        }

        /// <inheritdoc />
        public int Integer()
        {
            return _integerProvider.Integer();
        }

        /// <inheritdoc />
        public long Long(long min, long max)
        {
            return _longProvider.Long(min, max);
        }

        /// <inheritdoc />
        public long Long(long max)
        {
            return _longProvider.Long(max);
        }

        /// <inheritdoc />
        public long Long()
        {
            return _longProvider.Long();
        }

        /// <inheritdoc />
        public string FirstName()
        {
            return _nameProvider.FirstName();
        }

        /// <inheritdoc />
        public string FirstName(Gender gender)
        {
            return _nameProvider.FirstName(gender);
        }

        /// <inheritdoc />
        public string LastName()
        {
            return _nameProvider.LastName();
        }

        ///<inheritdoc />
        public string PhoneNumber(int length)
        {
            return _phoneNumberProvider.PhoneNumber(length);
        }

        ///<inheritdoc />
        public string PhoneNumber()
        {
            return _phoneNumberProvider.PhoneNumber();
        }

        /// <inheritdoc />
        public string SecurityNumber(DateTime date)
        {
            return _securityNumberProvider.SecurityNumber(date);
        }

        /// <inheritdoc />
        public string SecurityNumber()
        {
            return _securityNumberProvider.SecurityNumber();
        }

        /// <inheritdoc />
        public string UserName()
        {
            return _userNameProvider.UserName();
        }

        /// <inheritdoc />
        public Guid Guid()
        {
            return _guidProvider.Guid();
        }

        public string Guid(GuidFormat format)
        {
            return _guidProvider.Guid(format);
        }

        public TimeSpan TimeSpan()
        {
            return _timeSpanProvider.TimeSpan();
        }

        public TimeSpan TimeSpan(TimeSpan max)
        {
            return _timeSpanProvider.TimeSpan(max);
        }

        public TimeSpan TimeSpan(TimeSpan min, TimeSpan max)
        {
            return _timeSpanProvider.TimeSpan(min, max);
        }
    }
}