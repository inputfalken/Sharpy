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
        ///     will create a sequence of the values given
        /// </summary>
        /// <param name="length">Length of sequence</param>
        /// <param name="date">date to be sequenced</param>
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


        //Can add an bool arg and use ternary operator to make this into one method
        public static LocalDate CreatePastDate(LocalDate date)
            => CurrentLocalDate.Minus(FromYears(date.Year)).Minus(FromMonths(date.Month)).Minus(FromDays(date.Day));

        public static LocalDate CreateFutureDate(LocalDate date)
            => CurrentLocalDate.Plus(FromYears(date.Year)).Plus(FromMonths(date.Month)).Plus(FromDays(date.Day));
    }
}