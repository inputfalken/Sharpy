using System;
using NodaTime;

namespace Sharpy.Implementation {
    /// <summary>
    ///     Is used for creating random dates
    /// </summary>
    internal sealed class DateGenerator {
        private readonly Random _random;

        internal DateGenerator(Random random) {
            _random = random;
        }

        /// <summary>
        ///     Is used for getting the current time.
        /// </summary>
        internal static LocalDate CurrentLocalDate => SystemClock
            .Instance
            .GetCurrentInstant()
            .InZone(DateTimeZoneProviders.Tzdb.GetSystemDefault()).Date;

        /// <summary>
        ///     Will give and random date minus the argument in years
        ///     <param name="age">amount of years</param>
        /// </summary>
        internal LocalDate RandomDateByAge(int age) {
            if (age < 0)
                throw new ArgumentException($"{nameof(age)} cannot be negative");
            var month = _random.Next(1, CurrentLocalDate.Month);
            var date = CurrentLocalDate.Minus(Period.FromYears(age));
            var day = month == CurrentLocalDate.Month
                ? _random.Next(1, date.Day)
                : _random.Next(1, DateTime.DaysInMonth(date.Year, month));
            return new LocalDate(date.Year, month, day);
        }

        /// <summary>
        ///     Will give a random month and date on specific year
        ///     <param name="year">which year to use</param>
        /// </summary>
        internal LocalDate RandomDateByYear(int year) {
            if (year < 0)
                throw new ArgumentException($"{nameof(year)} cannot be negative");
            var month = _random.Next(1, CurrentLocalDate.Month);
            return new LocalDate(year, month, _random.Next(1, DateTime.DaysInMonth(year, month)));
        }
    }
}