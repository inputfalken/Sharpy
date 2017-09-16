using System;

namespace Sharpy.Implementation {
    /// <summary>
    ///     Is used for creating random dates
    /// </summary>
    internal sealed class DateGenerator {
        private readonly Random _random;

        internal DateGenerator(Random random) => _random = random;

        /// <summary>
        ///     Is used for getting the current time.
        /// </summary>
        internal static DateTime CurrentLocalDate => DateTime.Now;

        /// <summary>
        ///     Will give and random date minus the argument in years
        ///     <param name="age">amount of years</param>
        /// </summary>
        internal DateTime RandomDateByAge(int age) {
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
        ///     Will give a random month and date on specific year
        ///     <param name="year">which year to use</param>
        /// </summary>
        internal DateTime RandomDateByYear(int year) {
            if (year < 0)
                throw new ArgumentException($"{nameof(year)} cannot be negative");
            var month = _random.Next(1, CurrentLocalDate.Month);
            return new DateTime(year, month, _random.Next(1, DateTime.DaysInMonth(year, month)));
        }
    }
}