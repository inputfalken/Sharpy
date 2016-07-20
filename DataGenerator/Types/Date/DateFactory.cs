using System;
using NodaTime;
using NodaTime.Calendars;
using NodaTime.TimeZones;

namespace DataGenerator.Types.Date {
    public static class DateFactory {
        /// <summary>
        ///  Returns a LocalDate with a random month and the current year minus age arg
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public static LocalDate RandomBirthDate(int age) {
            if (age < 0)
                throw new ArgumentException("Argument cannot be negative");
            var currentTime = CurrentTime();
            int month;
            int year;
            if (age == 0) {
                month = HelperClass.Randomizer(1, currentTime.Month);
                year = currentTime.Year;
            }
            else {
                year = currentTime.Minus(Period.FromYears(age)).Year;
                month = RandomizeMonth();
            }
            return new LocalDate(year, month, RandomizeDate(CalendarSystem.Iso.GetDaysInMonth(year, month)));
        }


        /// <summary>
        /// Returns a LocalDate with a random month and date and a year within min and max age
        /// </summary>
        /// <param name="minAge"></param>
        /// <param name="maxAge"></param>
        /// <returns></returns>
        public static LocalDate RandomBirthDate(int minAge, int maxAge) {
            if (minAge < 0 || maxAge < 0)
                throw new ArgumentException("Arguments cannot be negative");
            var month = RandomizeMonth();
            var year = RandomizeBirthYear(minAge, maxAge);
            return new LocalDate(year, month, RandomizeDate(CalendarSystem.Iso.GetDaysInMonth(year, month)));
        }

        public static LocalDate RandomFutureDate(int years) {
            if (years < 0)
                throw new ArgumentException("Argument cannot be negative");
            var month = RandomizeMonth();
            var year = CurrentTime().Year + years;
            return new LocalDate(year, month, RandomizeDate(CalendarSystem.Iso.GetDaysInMonth(year, month)));
        }

        /// <summary>
        /// Returns the current year minus age
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        private static LocalDate CurrentTime()
            => SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault())
                .Date;

        /// <summary>
        /// Returns the current year minus a value between minAge and maxAge
        /// </summary>
        /// <param name="minAge"></param>
        /// <param name="maxAge"></param>
        /// <returns></returns>
        private static int RandomizeBirthYear(int minAge, int maxAge)
            => SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault())
                .Date.Minus(Period.FromYears(HelperClass.Randomizer(minAge, maxAge))).Year;

        /// <summary>
        /// Returns a random int representing a month
        /// </summary>
        /// <returns></returns>
        private static int RandomizeMonth()
            => HelperClass.Randomizer(1, 12);

        /// <summary>
        /// Returns a date within a month
        /// </summary>
        /// <param name="maxDays"></param>
        /// <returns></returns>
        private static int RandomizeDate(int maxDays)
            => HelperClass.Randomizer(1, maxDays);
    }
}