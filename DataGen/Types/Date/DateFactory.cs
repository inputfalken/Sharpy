using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NodaTime;
using static NodaTime.Period;

namespace DataGen.Types.Date {
    public static class DateFactory {
        private static LocalDate CurrentLocalDate
            => SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;


        /// <summary>
        ///     Will create a sequence of the values given
        /// </summary>
        /// <param name="length">Length of sequence</param>
        /// <param name="date">Date to be sequenced</param>
        /// <returns></returns>
        public static IEnumerable<LocalDate> CreateSequence(int length, LocalDate date) {
            var localDates = new Collection<LocalDate>();
            for (var i = 0; i < length; i++)
                localDates.Add(
                    CurrentLocalDate
                        .Plus(FromYears(date.Year * i))
                        .Plus(FromMonths(date.Month * i))
                        .Plus(FromDays(date.Day * i)));
            return localDates;
        }


        ///<summary>
        ///  Will add or subtract the date argument from current date.
        /// </summary>
        public static LocalDate CreateDate(LocalDate date, bool past = true)
            => past == true
                ? CurrentLocalDate.Minus(FromYears(date.Year)).Minus(FromMonths(date.Month)).Minus(FromDays(date.Day))
                : CurrentLocalDate.Plus(FromYears(date.Year)).Plus(FromMonths(date.Month)).Plus(FromDays(date.Day));
    }
}