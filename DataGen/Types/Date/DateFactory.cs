using System;
using NodaTime;
using static NodaTime.Period;

namespace DataGen.Types.Date {
    public static class DateFactory {
        private static LocalDate CurrentLocalDate
            => SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;

        /// <summary>
        /// Will Return the current days plus the arguments
        /// </summary>
        /// <param name="years"></param>
        /// <param name="month"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static LocalDate AdditionToCurrentDate(int years = 0, int month = 0, int days = 0) {
            if (years < 0 || month < 0 || days < 0)
                throw new ArgumentException("Arguments cannot be negative");
            return CurrentLocalDate
                .PlusYears(years)
                .PlusMonths(month)
                .PlusDays(days);
        }

        /// <summary>
        ///     Will Return the current days minus the arguments
        /// </summary>
        /// <param name="years"></param>
        /// <param name="months"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static LocalDate SubtractionFromCurrentDate(int years = 0, int months = 0, int days = 0) {
            if (years < 0 || months < 0 || days < 0)
                throw new ArgumentException("Arguments cannot be negative");
            return
                CurrentLocalDate
                    .Minus(FromYears(years))
                    .Minus(FromMonths(months))
                    .Minus(FromDays(days));
        }
    }
}