using System;
using Sharpy.IProviders;
using static System.DateTime;

namespace Sharpy.Implementation {
    /// <summary>
    ///     Randomizes <see cref="DateTime" /> elements by using <see cref="Random" />.
    /// </summary>
    public sealed class DateRandomizer : IDateProvider {
        private readonly Random _random;

        /// <summary>
        ///     Creates <see cref="DateRandomizer" />.
        /// </summary>
        /// <param name="random"></param>
        public DateRandomizer(Random random) => _random = random;

        /// <summary>
        ///     <para>
        ///         Randomizes a <see cref="DateTime" /> based on argument <paramref name="age" />.
        ///     </para>
        /// </summary>
        /// <param name="age">
        ///     The age of the date.
        /// </param>
        /// <returns>
        ///     A randomized <see cref="DateTime" /> based on <paramref name="age" />.
        /// </returns>
        public DateTime DateByAge(int age) {
            if (age < 0)
                throw new ArgumentException($"{nameof(age)} cannot be negative");
            var date = Now
                .AddYears(-age)
                .AddMonths(-_random.Next(1, 12));

            return date
                .AddDays(-_random.Next(1, DaysInMonth(date.Year, date.Month) + 1))
                .AddHours(-_random.Next(1, 24))
                .AddMinutes(-_random.Next(-1, 60))
                .AddSeconds(-_random.Next(1, 60));
        }

        /// <summary>
        ///     <para>
        ///         Randomizes a <see cref="DateTime" /> based on argument <paramref name="year" />.
        ///     </para>
        /// </summary>
        /// <param name="year">
        ///     The year of the date.
        /// </param>
        /// <returns>
        ///     A <see cref="DateTime" /> with the argument <paramref name="year" /> as the year.
        ///     The month and date has been randomized.
        /// </returns>
        public DateTime DateByYear(int year) {
            if (year <= MinValue.Year) throw new ArgumentException($"{nameof(year)} cannot be negative");
            var month = _random.Next(1, 13);
            return new DateTime(
                year,
                month,
                DaysInMonth(year, month),
                _random.Next(1, 24),
                _random.Next(1, 60),
                _random.Next(1, 60)
            );
        }
    }
}