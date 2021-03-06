using System;
using System.Collections.Generic;
using Sharpy.Builder.Enums;
using Sharpy.Builder.Providers;

namespace Sharpy.Builder
{
    /// <summary>
    ///     <para>
    ///         Contains various methods for providing data.
    ///     </para>
    /// </summary>
    public class Builder :
        IDoubleProvider,
        IIntProvider,
        ILongProvider,
        INameProvider,
        IElementProvider,
        IBoolProvider,
        IDateTimeProvider,
        IEmailProvider,
        IUserNameProvider,
        IGuidProvider,
        ITimeSpanProvider,
        IDecimalProvider,
        IDateTimeOffsetProvider,
        IFloatProvider,
        ICharProvider
    {
        private readonly IBoolProvider _boolProvider;
        private readonly ICharProvider _charProvider;
        private readonly IDateTimeOffsetProvider _dateTimeOffsetProvider;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IDecimalProvider _decimalProvider;
        private readonly IDoubleProvider _doubleProvider;
        private readonly IElementProvider _elementProvider;
        private readonly IEmailProvider _emailProvider;
        private readonly IFloatProvider _floatProvider;
        private readonly IGuidProvider _guidProvider;
        private readonly IIntProvider _intProvider;
        private readonly ILongProvider _longProvider;
        private readonly INameProvider _nameProvider;
        private readonly ITimeSpanProvider _timeSpanProvider;
        private readonly IUserNameProvider _userNameProvider;

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
            _doubleProvider = configurement.DoubleProvider;
            _intProvider = configurement.IntProvider;
            _longProvider = configurement.LongProvider;
            _nameProvider = configurement.NameProvider;
            _dateTimeProvider = configurement.DateTimeProvider;
            _emailProvider = configurement.MailProvider;
            _elementProvider = configurement.ListElementPicker;
            _boolProvider = configurement.BoolProvider;
            _userNameProvider = configurement.UserNameProvider;
            _guidProvider = configurement.GuidProvider;
            _timeSpanProvider = configurement.TimeSpanProvider;
            _decimalProvider = configurement.DecimalProvider;
            _dateTimeOffsetProvider = configurement.DateTimeOffSetProvider;
            _floatProvider = configurement.FloatProvider;
            _charProvider = configurement.CharProvider;
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
        public bool Bool()
        {
            return _boolProvider.Bool();
        }

        /// <inheritdoc />
        public char Char()
        {
            return _charProvider.Char();
        }

        /// <inheritdoc />
        public char Char(in char max)
        {
            return _charProvider.Char(max);
        }

        /// <inheritdoc />
        public char Char(in char min, in char max)
        {
            return _charProvider.Char(min, max);
        }

        /// <inheritdoc />
        public DateTimeOffset DateTimeOffset()
        {
            return _dateTimeOffsetProvider.DateTimeOffset();
        }

        /// <inheritdoc />
        public DateTimeOffset DateTimeOffset(in DateTimeOffset max)
        {
            return _dateTimeOffsetProvider.DateTimeOffset(max);
        }

        /// <inheritdoc />
        public DateTimeOffset DateTimeOffset(in DateTimeOffset min, in DateTimeOffset max)
        {
            return _dateTimeOffsetProvider.DateTimeOffset(min, max);
        }

        /// <inheritdoc />
        public DateTime DateTimeFromAge(in int age)
        {
            return _dateTimeProvider.DateTimeFromAge(age);
        }

        /// <inheritdoc />
        public DateTime DateTimeFromYear(in int year)
        {
            return _dateTimeProvider.DateTimeFromYear(year);
        }

        /// <inheritdoc />
        public DateTime DateTime()
        {
            return _dateTimeProvider.DateTime();
        }

        /// <inheritdoc />
        public DateTime DateTime(in DateTime max)
        {
            return _dateTimeProvider.DateTime(max);
        }

        /// <inheritdoc />
        public DateTime DateTime(in DateTime min, in DateTime max)
        {
            return _dateTimeProvider.DateTime(min, max);
        }

        /// <inheritdoc />
        public decimal Decimal(in decimal max)
        {
            return _decimalProvider.Decimal(max);
        }

        /// <inheritdoc />
        public decimal Decimal(in decimal min, in decimal max)
        {
            return _decimalProvider.Decimal(min, max);
        }

        /// <inheritdoc />
        public decimal Decimal()
        {
            return _decimalProvider.Decimal();
        }

        /// <inheritdoc />
        public double Double()
        {
            return _doubleProvider.Double();
        }

        /// <inheritdoc />
        public double Double(in double max)
        {
            return _doubleProvider.Double(max);
        }

        /// <inheritdoc />
        public double Double(in double min, in double max)
        {
            return _doubleProvider.Double(min, max);
        }

        /// <inheritdoc />
        public T FromArgument<T>(in T first, in T second)
        {
            return _elementProvider.FromArgument(first, second);
        }

        /// <inheritdoc />
        public T FromArgument<T>(in T first, in T second, in T third)
        {
            return _elementProvider.FromArgument(first, second, third);
        }

        /// <inheritdoc />
        public T FromArgument<T>(in T first, in T second, in T third, in T fourth)
        {
            return _elementProvider.FromArgument(first, second, third, fourth);
        }

        /// <inheritdoc />
        public T FromArgument<T>(in T first, in T second, in T third, in T fourth, in T fifth)
        {
            return _elementProvider.FromArgument(first, second, third, fourth, fifth);
        }

        /// <inheritdoc />
        public T FromArgument<T>(in T first, in T second, in T third, in T fourth, in T fifth, params T[] additional)
        {
            return _elementProvider.FromArgument(first, second, third, fourth, fifth, additional);
        }

        /// <inheritdoc />
        public T FromList<T>(in IReadOnlyList<T> list)
        {
            return _elementProvider.FromList(list);
        }

        /// <inheritdoc />
        public T FromSpan<T>(in ReadOnlySpan<T> span)
        {
            return _elementProvider.FromSpan(span);
        }

        /// <inheritdoc />
        public T FromSpan<T>(in Span<T> span)
        {
            return _elementProvider.FromSpan(span);
        }

        /// <inheritdoc />
        public string Mail(in string[] names)
        {
            return _emailProvider.Mail(names);
        }

        ///<inheritdoc />
        public string Mail(in string name)
        {
            return _emailProvider.Mail(name);
        }

        ///<inheritdoc />
        public string Mail(in string firstName, in string secondName)
        {
            return _emailProvider.Mail(firstName, secondName);
        }

        ///<inheritdoc />
        public string Mail(in string firstName, in string secondName, in string thirdName)
        {
            return _emailProvider.Mail(firstName, secondName, thirdName);
        }

        ///<inheritdoc />
        public string Mail(in string firstName, in string secondName, in string thirdName, in string fourthName)
        {
            return _emailProvider.Mail(firstName, secondName, thirdName, fourthName);
        }

        ///<inheritdoc />
        public string Mail(in string firstName, in string secondName, in string thirdName, in string fourthName, in string fifthName)
        {
            return _emailProvider.Mail(firstName, secondName, thirdName, fourthName, fifthName);
        }

        ///<inheritdoc />
        public string Mail(
            in string firstName,
            in string secondName,
            in string thirdName,
            in string fourthName,
            in string fifthName,
            params string[] additional
        )
        {
            return _emailProvider.Mail(firstName, secondName, thirdName, fourthName, fifthName, additional);
        }


        /// <inheritdoc />
        public float Float(in float min, in float max)
        {
            return _floatProvider.Float(min, max);
        }

        /// <inheritdoc />
        public float Float(in float max)
        {
            return _floatProvider.Float(max);
        }

        /// <inheritdoc />
        public float Float()
        {
            return _floatProvider.Float();
        }

        /// <inheritdoc />
        public Guid Guid()
        {
            return _guidProvider.Guid();
        }

        /// <inheritdoc />
        public string Guid(in GuidFormat format)
        {
            return _guidProvider.Guid(format);
        }

        /// <inheritdoc />
        public int Int(in int max)
        {
            return _intProvider.Int(max);
        }

        /// <inheritdoc />
        public int Int(in int min, in int max)
        {
            return _intProvider.Int(min, max);
        }

        /// <inheritdoc />
        public int Int()
        {
            return _intProvider.Int();
        }

        /// <inheritdoc />
        public long Long(in long min, in long max)
        {
            return _longProvider.Long(min, max);
        }

        /// <inheritdoc />
        public long Long(in long max)
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
        public string FirstName(in Gender gender)
        {
            return _nameProvider.FirstName(gender);
        }

        /// <inheritdoc />
        public string LastName()
        {
            return _nameProvider.LastName();
        }

        /// <inheritdoc />
        public TimeSpan TimeSpan()
        {
            return _timeSpanProvider.TimeSpan();
        }

        /// <inheritdoc />
        public TimeSpan TimeSpan(in TimeSpan max)
        {
            return _timeSpanProvider.TimeSpan(max);
        }

        /// <inheritdoc />
        public TimeSpan TimeSpan(in TimeSpan min, in TimeSpan max)
        {
            return _timeSpanProvider.TimeSpan(min, max);
        }

        /// <inheritdoc />
        public string UserName()
        {
            return _userNameProvider.UserName();
        }
    }
}