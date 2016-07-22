using NodaTime;
using static NodaTime.Period;

namespace DataGen.Types.Date {
    public static class DateFactory {
        private static LocalDate CurrentLocalDate
            => SystemClock.Instance.Now.InZone(DateTimeZoneProviders.Bcl.GetSystemDefault()).Date;

        /// <summary>
        ///     Will add days, months, years to a date
        /// </summary>
        /// <param name="localDate">Will be defaulted to today if not specified</param>
        /// <param name="years">Years to be added</param>
        /// <param name="month">Months to be added</param>
        /// <param name="days">Days to be added </param>
        /// <returns></returns>
        public static LocalDate Addition(LocalDate localDate = default(LocalDate), uint years = 0, uint month = 0,
            uint days = 0) {
            var date = localDate == default(LocalDate) ? CurrentLocalDate : localDate;
            return date
                .Plus(FromYears(years))
                .Plus(FromMonths(month))
                .Plus(FromDays(days));
        }

        /// <summary>
        ///     Will subtract from current date
        /// </summary>
        /// <param name="localDate"></param>
        /// <param name="years"></param>
        /// <param name="months"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static LocalDate Subtraction(LocalDate localDate = default(LocalDate), uint years = 0, uint months = 0,
            uint days = 0) {
            var date = localDate == default(LocalDate) ? CurrentLocalDate : localDate;
            return date
                .Minus(FromYears(years))
                .Minus(FromMonths(months))
                .Minus(FromDays(days));
        }
    }
}