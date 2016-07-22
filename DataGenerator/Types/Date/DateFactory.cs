using System;
using NodaTime;
using NodaTime.Calendars;
using NodaTime.TimeZones;

namespace DataGenerator.Types.Date {
    public static class DateFactory {
        private static LocalDate CurrentLocalDate
            => SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;

        public static LocalDate RandomFutureDate(int years) {
            if (years < 0)
                throw new ArgumentException("Years cannot be negative");


            return years == 0
                ? FutureDateWithinCurrentYear()
                : RandomLocalDateByYear(CurrentLocalDate.Year + years);
        }

        private static LocalDate FutureDateWithinCurrentYear() {
            var month = HelperClass.Randomizer(CurrentLocalDate.Month, 12);
            var daysInMonth = CalendarSystem.Iso.GetDaysInMonth(CurrentLocalDate.Year, month);
            var date = month == CurrentLocalDate.Month
                ? HelperClass.Randomizer(CurrentLocalDate.Day + 1, daysInMonth)
                : RandomizeDate(daysInMonth);
            return new LocalDate(CurrentLocalDate.Year, month, date);
        }

        /// <summary>
        ///  Returns a LocalDate with a random month and the current year minus years arg
        /// </summary>
        /// <param name="years"></param>
        /// <returns></returns>
        public static LocalDate RandomPreviousDate(int years) {
            if (years < 0)
                throw new ArgumentException("Argument cannot be negative");
            return years == 0
                ? LocalDateWithinCurrentYear()
                : RandomLocalDateByYear(CurrentLocalDate.Year - years);
        }

        /// <summary>
        /// Returns a LocalDate with a random month and date and a year within min and max years
        /// </summary>
        /// <param name="minAge"></param>
        /// <param name="maxAge"></param>
        /// <returns></returns>
        public static LocalDate RandomPreviousDate(int minAge, int maxAge) {
            if (minAge < 0 || maxAge < 0)
                throw new ArgumentException("Arguments cannot be negative");
            if (minAge > maxAge)
                throw new ArgumentException("maxAge cannot be larger than minAge");
            var randomizedYear = CurrentLocalDate.Year - HelperClass.Randomizer(minAge, maxAge);

            //Will be true if randomized year would be randomed to 0
            return CurrentLocalDate.Year == randomizedYear
                ? LocalDateWithinCurrentYear()
                : RandomLocalDateByYear(randomizedYear);
        }

        /// <summary>
        /// Makes sure the local date returned cannot be a date in the future
        /// </summary>
        /// <returns></returns>
        private static LocalDate LocalDateWithinCurrentYear() {
            var month = HelperClass.Randomizer(1, CurrentLocalDate.Month);
            var daysInMonth = CalendarSystem.Iso.GetDaysInMonth(CurrentLocalDate.Year, month);
            var date = month == CurrentLocalDate.Month
                ? HelperClass.Randomizer(1, CurrentLocalDate.Day)
                : RandomizeDate(daysInMonth);
            return new LocalDate(CurrentLocalDate.Year, month, date);
        }

        private static LocalDate RandomLocalDateByYear(int years) {
            var month = HelperClass.Randomizer(1, 12);
            var date = RandomizeDate(CalendarSystem.Iso.GetDaysInMonth(years, month));
            return new LocalDate(years, month, date);
        }


        /// <summary>
        /// Returns a date within a month
        /// </summary>
        /// <param name="maxDays"></param>
        /// <returns></returns>
        private static int RandomizeDate(int maxDays)
            => HelperClass.Randomizer(1, maxDays);
    }
}