using System;
using System.Collections.Generic;
using System.Linq;
using NodaTime;
using static NodaTime.Period;

namespace DataGen.Types.Date {
    public static class DateFactory {
        private static LocalDate CurrentLocalDate
            => SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;


        /// <summary>
        ///     Will create a sequence of the values given from current date
        /// </summary>
        /// <param name="length">Length of sequence</param>
        /// <param name="date">Date to be sequenced</param>
        /// <returns></returns>
        public static IEnumerable<LocalDate> CreateSequence(int length, LocalDate date)
            => Enumerable.Range(1, length)
                .Select(i => CurrentLocalDate.PlusYears(date.Year * i).PlusMonths(date.Month * i).PlusDays(date.Day * i));


        /// <summary>
        ///     Will add or subtract the date argument from current date.
        /// <param name="date">The date to subtract/add to/from current date</param>
        /// <param name="subtract">if set to false it will perform addition</param>
        /// </summary>
        public static LocalDate Date(LocalDate date, bool subtract = true)
            => subtract
                ? CurrentLocalDate.Minus(FromYears(date.Year)).Minus(FromMonths(date.Month)).Minus(FromDays(date.Day))
                : CurrentLocalDate.Plus(FromYears(date.Year)).Plus(FromMonths(date.Month)).Plus(FromDays(date.Day));
    }
}