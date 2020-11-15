using System;
using Sharpy.Builder.Implementation.ExtensionMethods;
using Sharpy.Builder.Providers;
using static System.DateTime;

namespace Sharpy.Builder.Implementation
{
    /// <summary>
    ///     Randomizes <see cref="System.DateTime" /> elements by using <see cref="Random" />.
    /// </summary>
    public sealed class DateTimeRandomizer : IDateTimeProvider
    {
        private readonly Random _random;

        /// <summary>
        ///     Creates <see cref="DateTimeRandomizer" />.
        /// </summary>
        /// <param name="random"></param>
        public DateTimeRandomizer(Random random) => _random = random;

        /// <inheritdoc />
        public DateTime DateTimeByAge(int age)
        {
            if (age < 0)
                throw new ArgumentException($"{nameof(age)} cannot be negative");

            var date = Now
                .AddYears(-age)
                .AddMonths(-_random.Next(1, 12) + 1);

            return date
                .AddDays(-_random.Day(date.Year, date.Month))
                .AddHours(_random.Hour())
                .AddMinutes(_random.Minute())
                .AddSeconds(_random.Second())
                .AddMilliseconds(_random.MilliSecond());
        }

        /// <inheritdoc />
        public DateTime DateTimeByYear(int year)
        {
            if (year <= MinValue.Year) throw new ArgumentException($"{nameof(year)} cannot be negative");

            return DateTime(
                new DateTime(year, 1, 1),
                new DateTime(year, 12, DaysInMonth(year, 12))
            );
        }


        /// <inheritdoc />
        public DateTime DateTime()
        {
            return DateTime(MinValue, MaxValue);
        }

        /// <inheritdoc />
        public DateTime DateTime(DateTime max)
        {
            return DateTime(MinValue, max);
        }

        /// <inheritdoc />
        public DateTime DateTime(DateTime min, DateTime max)
        {
            return _random.DateTime(min, max);
        }
    }
}