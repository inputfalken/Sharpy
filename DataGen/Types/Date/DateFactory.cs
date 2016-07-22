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
        /// <param name="month"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static LocalDate SubtractionToCurrentDate(int years = 0, int month = 0, int days = 0) {
            if (years < 0 || month < 0 || days < 0)
                throw new ArgumentException("Arguments cannot be negative");
            return
                CurrentLocalDate
                    .Minus(FromYears(years))
                    .Minus(FromMonths(month))
                    .Minus(FromDays(days));
        }


        /// <summary>
        /// Will Return a random date at year of the argument
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        //TODO add some testing
        public static LocalDate RandomDateSpecifikYear(int year) {
            var month = HelperClass.Randomizer(1, 12);
            var daysInMonth = CalendarSystem.Iso.GetDaysInMonth(year, month);
            var day = HelperClass.Randomizer(daysInMonth);
            return new LocalDate(year, month, day);
        }
    }
}