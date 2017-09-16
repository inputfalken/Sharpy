using System;
using Sharpy.IProviders;

namespace Sharpy.Implementation {
    /// <summary>
    ///     Is used for creating random dates
    /// </summary>
    public sealed class DateRandomizer : IDateProvider {
        private readonly Random _random;

        internal DateRandomizer(Random random) => _random = random;

        /// <summary>
        ///     Is used for getting the current time.
        /// </summary>
        internal static DateTime CurrentLocalDate => DateTime.Now;

        /// <summary>
        ///     <para>
        ///         Randomizes a <see cref="DateTime" /> based on argument <paramref name="age" />.
        ///     </para>
        /// </summary>
        /// <param name="age">
        ///     The age of the date.
        /// </param>
        /// <returns>
        ///     A <see cref="DateTime" /> with todays year minus the argument <paramref name="age" />.
        ///     The month and date has been randomized.
        /// </returns>
        public DateTime DateByAge(int age) {
            if (age < 0)
                throw new ArgumentException($"{nameof(age)} cannot be negative");
            var date = CurrentLocalDate.AddYears(-age);
            var month = _random.Next(1, date.Month);

            var day = month == date.Month
                ? _random.Next(1, date.Day)
                : _random.Next(1, DateTime.DaysInMonth(date.Year, month));
            return new DateTime(date.Year, month, day);
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
            if (year < 0)
                throw new ArgumentException($"{nameof(year)} cannot be negative");
            var month = _random.Next(1, CurrentLocalDate.Month);
            return new DateTime(year, month, _random.Next(1, DateTime.DaysInMonth(year, month)));
        }
    }
}