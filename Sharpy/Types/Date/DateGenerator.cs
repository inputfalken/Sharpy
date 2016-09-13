using System;
using System.Collections.Generic;
using System.Linq;
using NodaTime;

namespace Sharpy.Types.Date {
    public class DateGenerator {
        private readonly Random _random;

        public static LocalDate CurrentLocalDate
            => SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;

        public Random Random { get; set; }
        public DateGenerator(Random random) {
            _random = random;
        }

        /// <summary>
        ///     Will create a sequence of the values given from current date
        /// </summary>
        /// <param name="length">Length of sequence</param>
        /// <param name="sequenceDate">Date to be sequenced</param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public static IEnumerable<LocalDate> Sequence(int length, LocalDate sequenceDate,
            LocalDate startDate = default(LocalDate)) {
            var localDate = startDate == default(LocalDate) ? CurrentLocalDate : startDate;
            return Enumerable.Range(1, length)
                .Select(i => localDate
                    .PlusYears(sequenceDate.Year * i)
                    .PlusMonths(sequenceDate.Month * i)
                    .PlusDays(sequenceDate.Day * i));
        }


        /// <summary>
        ///     Will add or subtract the date argument from current date.
        ///     <param name="date">The date to subtract/add to/from current date</param>
        ///     <param name="subtract">if set to false it will add the values to the current date rather than subtracting</param>
        /// </summary>
        public static LocalDate Date(LocalDate date, bool subtract = true) => subtract
            ? CurrentLocalDate.Minus(Period.FromYears(date.Year)).Minus(Period.FromMonths(date.Month)).Minus(Period.FromDays(date.Day))
            : CurrentLocalDate.Plus(Period.FromYears(date.Year)).Plus(Period.FromMonths(date.Month)).Plus(Period.FromDays(date.Day));

        /// <summary>
        ///     This overload will do the same but with ints
        ///     <param name="subtract">if set to false it will add the values to the current date rather than subtracting</param>
        /// </summary>
        public static LocalDate Date(int year, int month, int day, bool subtract = true) {
            if (year < 0 || month < 0 || day < 0)
                throw new ArgumentException("Year/Month/Day cannot be negative");
            return subtract
                ? CurrentLocalDate.Minus(Period.FromYears(year)).Minus(Period.FromMonths(month)).Minus(Period.FromDays(day))
                : CurrentLocalDate.Plus(Period.FromYears(year)).Plus(Period.FromMonths(month)).Plus(Period.FromDays(day));
        }

        /// <summary>
        ///     Will give and random date minus the argument in years
        ///     <param name="age">ammount of years</param>
        /// </summary>
        public LocalDate RandomDateByAge(int age) {
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
        ///     Will give a random month and date on specifk year
        ///     <param name="year">which year to use</param>
        /// </summary>
        public LocalDate RandomDateByYear(int year) {
            var month = _random.Next(1, CurrentLocalDate.Month);
            return new LocalDate(year, month, _random.Next(1, DateTime.DaysInMonth(year, month)));
        }
    }
}