using System.Collections.Generic;
using System.Collections.ObjectModel;
using NodaTime;
using static System.Linq.Enumerable;
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
            => new List<LocalDate>().Sequence(length,
                (localDate, i) => localDate
                    .PlusDays(date.Day * i)
                    .PlusMonths(date.Month * i)
                    .PlusYears(date.Year * i), CurrentLocalDate);


        ///<summary>
        ///  Will add or subtract the date argument from current date.
        /// </summary>
        public static LocalDate CreateDate(LocalDate date, bool past = true)
            => past == true
                ? CurrentLocalDate.Minus(FromYears(date.Year)).Minus(FromMonths(date.Month)).Minus(FromDays(date.Day))
                : CurrentLocalDate.Plus(FromYears(date.Year)).Plus(FromMonths(date.Month)).Plus(FromDays(date.Day));
    }
}