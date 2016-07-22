using System;
using NodaTime;

namespace DataGen.Types.Date {
    public static class DateFactory {
        private static LocalDate CurrentLocalDate
            => SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;

        //Instead of method overloading use default values? User could then decide which data should be randomed by using named arguments
        public static LocalDate RandomFutureDate(int years) {
            if (years < 0)
                throw new ArgumentException("Years cannot be negative");
            return years == 0
                ? FutureDateWithinCurrentYear()
                : RandomLocalDate(CurrentLocalDate.Year + years);
        }

        private static LocalDate FutureDateWithinCurrentYear() {
            //Random a value between current month and last
            var month = HelperClass.Randomizer(CurrentLocalDate.Month, 12);
            var daysInMonth = CalendarSystem.Iso.GetDaysInMonth(CurrentLocalDate.Year, month);
            //Is true if month gets randomized to current month
            var date = month == CurrentLocalDate.Month
                //Makes sure that that you cannot get a date whos date isn't passed
                ? HelperClass.Randomizer(CurrentLocalDate.Day + 1, daysInMonth)
                : RandomizeDate(daysInMonth);
            return new LocalDate(CurrentLocalDate.Year, month, date);
        }

        /// <summary>
        ///     Returns a LocalDate with a random month and the current year minus years arg
        /// </summary>
        /// <param name="years"></param>
        /// <returns></returns>
        public static LocalDate RandomPreviousDate(int years) {
            if (years < 0)
                throw new ArgumentException("Argument cannot be negative");
            return years == 0
                ? LocalDateWithinCurrentYear()
                : RandomLocalDate(CurrentLocalDate.Year - years);
        }


        /// <summary>
        ///     Makes sure the local date returned cannot be a date in the future
        /// </summary>
        /// <returns></returns>
        private static LocalDate LocalDateWithinCurrentYear() {
            var month = HelperClass.Randomizer(1, CurrentLocalDate.Month);
            var daysInMonth = CalendarSystem.Iso.GetDaysInMonth(CurrentLocalDate.Year, month);
            //Is true if month gets randomized to current month
            var date = month == CurrentLocalDate.Month
                //Makes sure that you cannot get the current date or a date which hasn't yet passed 
                ? HelperClass.Randomizer(1, CurrentLocalDate.Day - 1)
                : RandomizeDate(daysInMonth);
            return new LocalDate(CurrentLocalDate.Year, month, date);
        }

        private static LocalDate RandomLocalDate(int years) {
            var month = HelperClass.Randomizer(1, 12);
            var date = RandomizeDate(CalendarSystem.Iso.GetDaysInMonth(years, month));
            return new LocalDate(years, month, date);
        }


        /// <summary>
        ///     Returns a date within a month
        /// </summary>
        /// <param name="maxDays"></param>
        /// <returns></returns>
        private static int RandomizeDate(int maxDays)
            => HelperClass.Randomizer(1, maxDays);
    }
}