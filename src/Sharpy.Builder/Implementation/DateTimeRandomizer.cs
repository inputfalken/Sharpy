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
        public DateTimeRandomizer(Random random)
        {
            _random = random;
        }

        /// <inheritdoc />
        public DateTime DateTimeFromAge(in int age)
        {
            if (age < 0)
                throw new ArgumentException($"{nameof(age)} cannot be negative");

            var max = Now.AddYears(-age);

            return DateTime(new DateTime(max.Year, 1, 1), max);
        }

        /// <inheritdoc />
        public DateTime DateTimeFromYear(in int year)
        {
            if (year <= 0)
                throw new ArgumentException($"{nameof(year)} cannot be negative");

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
        public DateTime DateTime(in DateTime max)
        {
            return DateTime(MinValue, max);
        }

        /// <inheritdoc />
        public DateTime DateTime(in DateTime min, in DateTime max)
        {
            return _random.DateTime(min, max);
        }
    }
}